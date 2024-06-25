using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.WeekDay.Validators;
using Hrm.Application.Features.Weekend.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;

namespace Hrm.Application.Features.Weekend.Handlers.Commands
{
    public class CreateWeekDayCommandHandler : IRequestHandler<CreateWeekDayCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateWeekDayCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateWeekDayCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateWeekDayDtoValidator();
            var validationResult = await validator.ValidateAsync(request.WeekendDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var Weekend = _mapper.Map<Hrm.Domain.WeekDay>(request.WeekendDto);

                Weekend = await _unitOfWork.Repository<Hrm.Domain.WeekDay>().Add(Weekend);
                await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = Weekend.WeekDayId;
            }

            return response;
        }

    }
}
