using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.ChildStatus.Validators;
using Hrm.Application.Features.ChildStatus.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.ChildStatus.Handlers.Commands
{
    public class CreateChildStatusCommandHandler : IRequestHandler<CreateChildStatusCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.ChildStatus> _ChildStatusRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateChildStatusCommandHandler(IHrmRepository<Hrm.Domain.ChildStatus> ChildStatusRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _ChildStatusRepository = ChildStatusRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateChildStatusCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateChildStatusDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.ChildStatusDto);

            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                var ChildStatusName = request.ChildStatusDto.ChildStatusName.ToLower();

                IQueryable<Hrm.Domain.ChildStatus> ChildStatuss = _ChildStatusRepository.Where(x => x.ChildStatusName.ToLower() == ChildStatusName);

                if (ChildStatusNameExists(request))
                {
                    response.Success = false;
                    response.Message = $"Creation Failed '{request.ChildStatusDto.ChildStatusName}' already exists.";

                    //response.Message = "Creation Failed, Name already exists";
                    response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
                }
                else
                {
                    var ChildStatus = _mapper.Map<Hrm.Domain.ChildStatus>(request.ChildStatusDto);

                    ChildStatus = await _unitOfWork.Repository<Hrm.Domain.ChildStatus>().Add(ChildStatus);
                    await _unitOfWork.Save();

                    response.Success = true;
                    response.Message = "Creation Successfull";
                    response.Id = ChildStatus.ChildStatusId;
                }
            }
            return response;
        }
        private bool ChildStatusNameExists(CreateChildStatusCommand request)
        {
            var ChildStatusName = request.ChildStatusDto.ChildStatusName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.ChildStatus> ChildStatuss = _ChildStatusRepository.Where(x => x.ChildStatusName.Trim().ToLower().Replace(" ", string.Empty) == ChildStatusName);

            return ChildStatuss.Any();
        }
    }
}
