using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.HolidayType.Validators;
using Hrm.Application.Features.HolidayType.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;

namespace Hrm.Application.Features.HolidayType.Handlers.Commands
{
    public class CreateHolidayTypeCommandHandler : IRequestHandler<CreateEmployeeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateHolidayTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateHolidayTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.HolidayTypeDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var HolidayType = _mapper.Map<Hrm.Domain.HolidayType>(request.HolidayTypeDto);

                HolidayType = await _unitOfWork.Repository<Hrm.Domain.HolidayType>().Add(HolidayType);
                await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = HolidayType.HolidayTypeId;
            }

            return response;
        }

    }
}
