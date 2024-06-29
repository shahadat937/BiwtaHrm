using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Workday.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Workday.Handlers.Commands
{
    public class DeleteWorkdayCommandHandler : IRequestHandler<DeleteWorkdayCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteWorkdayCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteWorkdayCommand request, CancellationToken cancellationToken)
        {
            var Workday = await _unitOfWork.Repository<Hrm.Domain.Workday>().Get(request.WorkdayId);
            var response = new BaseCommandResponse();
            if(Workday == null)
            {
                throw new NotFoundException(nameof(Workday), request.WorkdayId);
            }

            await _unitOfWork.Repository<Hrm.Domain.Workday>().Delete(Workday);
            await _unitOfWork.Save();

            response.Message = "Delete Successfull";
            response.Success = true;
            response.Id = Workday.WorkdayId;

            return response;
        }
    }
}
