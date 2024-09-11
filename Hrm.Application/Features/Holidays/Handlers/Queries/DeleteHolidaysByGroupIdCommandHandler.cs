using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.appraisalFormType.Requests.Commands;
using Hrm.Application.Features.Holidays.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Holidays.Handlers.Queries
{
    public class DeleteHolidaysByGroupIdCommandHandler: IRequestHandler<DeleteHolidaysByGroupIdCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteHolidaysByGroupIdCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteHolidaysByGroupIdCommand request, CancellationToken cancellationToken)
        {
            List<Hrm.Domain.Holidays> holidays = await _unitOfWork.Repository<Hrm.Domain.Holidays>().Where(x => x.GroupId == request.GroupId).ToListAsync();


            if(holidays.Count<=0)
            {
                throw new NotFoundException(nameof(holidays), request.GroupId);
            }

            foreach(var holiday in holidays)
            {
                await _unitOfWork.Repository<Hrm.Domain.Holidays>().Delete(holiday);
            }

            await _unitOfWork.Save();

            var response = new BaseCommandResponse();
            response.Success = true;
            response.Message = "Delete Successful";
            response.Id = request.GroupId;

            return response;
        }
    }
}
