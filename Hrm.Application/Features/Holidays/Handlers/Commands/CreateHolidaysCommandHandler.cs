using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Holidays.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Holidays.Handlers.Commands
{
    public class CreateHolidaysCommandHandler: IRequestHandler<CreateHolidaysCommand,BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateHolidaysCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateHolidaysCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            // Validation Yet to implement
            // Validation Yet to implement

            var Holidays = _mapper.Map<Hrm.Domain.Holidays>(request.holidayDto);

            Holidays = await _unitOfWork.Repository<Hrm.Domain.Holidays>().Add(Holidays);
            await _unitOfWork.Save();


            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = Holidays.HolidayId;

            return response;
        }
    }
}
