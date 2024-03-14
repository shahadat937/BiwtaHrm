using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Designation.Validators;
using Hrm.Application.Features.Designation.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Hrm.Application.DTOs.Designation.Validators;
using Hrm.Application.Features.Designation.Requests.Commands;

namespace Hrm.Application.Features.Designation.Handlers.Commands
{
    public class CreateDesignationCommandHandler : IRequestHandler<CreateDesignationCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateDesignationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateDesignationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateDesignationDtoValidator();
            var validationResult = await validator.ValidateAsync(request.DesignationDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var Designation = _mapper.Map<Hrm.Domain.Designation>(request.DesignationDto);

                Designation = await _unitOfWork.Repository<Hrm.Domain.Designation>().Add(Designation);
                await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = Designation.DesignationId;
            }

            return response;
        }

    }
}
