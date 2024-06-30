using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Holidays.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Holidays.Handlers.Commands
{
    public class DeleteHolidaysCommandHandler:IRequestHandler<DeleteHolidaysCommand,BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteHolidaysCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteHolidaysCommand request, CancellationToken cancellationToken)
        {
            var holiday = await _unitOfWork.Repository<Hrm.Domain.Holidays>().Get(request.HolidayId);

            if(holiday == null)
            {
                throw new NotFoundException(nameof(holiday), request.HolidayId);
            }

            await _unitOfWork.Repository<Hrm.Domain.Holidays>().Delete(holiday);
            await _unitOfWork.Save();

            var response = new BaseCommandResponse();
            response.Success = true;
            response.Message = "Delete Successfull";
            return response;

        }
    }
}
