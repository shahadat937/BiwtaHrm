using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmployeeType.Validators;
using Hrm.Application.Features.EmployeeType.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;

namespace Hrm.Application.Features.EmployeeType.Handlers.Commands
{
    public class CreateEmployeeTypeCommandHandler : IRequestHandler<CreateEmployeeTypeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateEmployeeTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateEmployeeTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateEmployeeTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.EmployeeTypeDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var EmployeeType = _mapper.Map<Hrm.Domain.EmployeeType>(request.EmployeeTypeDto);

                EmployeeType = await _unitOfWork.Repository<Hrm.Domain.EmployeeType>().Add(EmployeeType);
                await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = EmployeeType.EmployeeTypeId;
            }

            return response;
        }

    }
}
