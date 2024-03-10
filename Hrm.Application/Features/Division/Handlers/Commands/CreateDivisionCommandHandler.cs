using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Division.Validators;
using Hrm.Application.Features.Division.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;

namespace Hrm.Application.Features.Division.Handlers.Commands
{
    public class CreateDivisionCommandHandler : IRequestHandler<CreateBloodCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.Division> _DivisionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateDivisionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Division> DivisionRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _DivisionRepository = DivisionRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateBloodCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateDivisionDtoValidator();
            var validationResult = await validator.ValidateAsync(request.DivisionDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var DivisionName = request.DivisionDto.DivisionName.ToLower();

                IQueryable<Hrm.Domain.Division> Divisions = _DivisionRepository.Where(x => x.DivisionName.ToLower() == DivisionName);


                if (Divisions.Any())
                {
                    response.Success = false;
                    response.Message = "Creation Failed Name already exists.";
                    response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
                    
                }
                else
                {
                    var Division = _mapper.Map<Hrm.Domain.Division>(request.DivisionDto);

                    Division = await _unitOfWork.Repository<Hrm.Domain.Division>().Add(Division);
                    await _unitOfWork.Save();


                    response.Success = true;
                    response.Message = "Creation Successful";
                    response.Id = Division.DivisionId;
                }
            }

            return response;
        }

    }
}
