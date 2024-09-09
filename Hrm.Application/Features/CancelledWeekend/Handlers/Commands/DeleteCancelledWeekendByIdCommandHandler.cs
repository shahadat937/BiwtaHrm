using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.CancelledWeekend.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.CancelledWeekend.Handlers.Commands
{
    public class DeleteCancelledWeekendByIdCommandHandler: IRequestHandler<DeleteCancelledWeekendByIdCommand,BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteCancelledWeekendByIdCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteCancelledWeekendByIdCommand request, CancellationToken cancellationToken)
        {
            var cancelledWeekend = await _unitOfWork.Repository<Hrm.Domain.CancelledWeekend>().Get(request.Id);

            if(cancelledWeekend == null)
            {
                throw new NotFoundException(nameof(cancelledWeekend),request.Id);
            }

            await _unitOfWork.Repository<Hrm.Domain.CancelledWeekend>().Delete(cancelledWeekend);
            await _unitOfWork.Save();

            var response = new BaseCommandResponse();
            response.Success = true;
            response.Message = "Delete Successful";
            response.Id = request.Id;

            return response;
        }
    }
}
