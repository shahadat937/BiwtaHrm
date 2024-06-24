using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.MaritalStatus.Handlers.Commands
{
    public class CreateMaritalStatusCommandHandler : IRequestHandler<CreateMaritalStatusCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.MaritalStatus> _MaritalStatusRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateMaritalStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.MaritalStatus> MaritalStatusRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _MaritalStatusRepository = MaritalStatusRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateMaritalStatusCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateMaritalStatusDtoValidator();
            var validationResult = await validator.ValidateAsync(request.MaritalStatusDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                //   var MaritalStatusName = request.MaritalStatusDto.MaritalStatusName.Trim().ToLower().Replace(" ", string.Empty);

                //  IQueryable<Hrm.Domain.MaritalStatus> MaritalStatuss = _MaritalStatusRepository.Where(x => x.MaritalStatusName.ToLower().Replace(" ", string.Empty) == MaritalStatusName);


                if (MaritalStatusNameExists(request))
                {
                    response.Success = false;
                    response.Message = $"Creation Failed '{request.MaritalStatusDto.MaritalStatusName}' already exists.";
                    response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

                }
                else
                {
                    var MaritalStatus = _mapper.Map<Hrm.Domain.MaritalStatus>(request.MaritalStatusDto);

                    MaritalStatus = await _unitOfWork.Repository<Hrm.Domain.MaritalStatus>().Add(MaritalStatus);
                    await _unitOfWork.Save();


                    response.Success = true;
                    response.Message = "Creation Successful";
                    response.Id = MaritalStatus.MaritalStatusId;
                }
            }

            return response;
        }
        private bool MaritalStatusNameExists(CreateMaritalStatusCommand request)
        {
            var MaritalStatusName = request.MaritalStatusDto.MaritalStatusName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.MaritalStatus> MaritalStatuss = _MaritalStatusRepository.Where(x => x.MaritalStatusName.Trim().ToLower().Replace(" ", string.Empty) == MaritalStatusName);

            return MaritalStatuss.Any();
        }
    }
}
