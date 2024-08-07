using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.LeaveRules.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.LeaveRules.Handlers.Commands
{
    public class DeleteLeaveRuleCommandHandler: IRequestHandler<DeleteLeaveRuleCommand,BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteLeaveRuleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteLeaveRuleCommand request, CancellationToken cancellationToken)
        {
            var leaveRule = await _unitOfWork.Repository<Hrm.Domain.LeaveRules>().Get(request.LeaveRuleId);

            if(leaveRule == null)
            {
                throw new NotFoundException(nameof(leaveRule), request.LeaveRuleId);
            }

            await _unitOfWork.Repository<Hrm.Domain.LeaveRules>().Delete(leaveRule);
            await _unitOfWork.Save();

            var response = new BaseCommandResponse();
            response.Success = true;
            response.Message = "Delete Successful";
            response.Id = request.LeaveRuleId;

            return response;

        }
    }
}
