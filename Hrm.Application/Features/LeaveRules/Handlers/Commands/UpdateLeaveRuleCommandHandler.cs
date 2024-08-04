using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.LeaveRules.Validators;
using Hrm.Application.Features.LeaveRules.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.Constants;
using System.Reflection;

namespace Hrm.Application.Features.LeaveRules.Handlers.Commands
{
    public class UpdateLeaveRuleCommandHandler: IRequestHandler<UpdateLeaveRuleCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateLeaveRuleCommandHandler (IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateLeaveRuleCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateLeaveRulesDtoValidator();
            var validationResult = await validator.ValidateAsync(request.updateLeaveRuleDto, cancellationToken);

            if(!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }

            var leaveRule = await _unitOfWork.Repository<Hrm.Domain.LeaveRules>().Get(request.updateLeaveRuleDto.RuleId);

            if(leaveRule == null)
            {
                throw new NotFoundException(nameof(leaveRule), request.updateLeaveRuleDto.RuleId);
            }

            Type ruleName = typeof(LeaveRule);
            FieldInfo[] fieldinfo = ruleName.GetFields(BindingFlags.Public | BindingFlags.Static);

            bool isValidRuleName = fieldinfo.Where(x => x.Name == request.updateLeaveRuleDto.RuleName).Any();

            if (!isValidRuleName)
            {
                response.Success = false;
                response.Message = "Rule name is invalid";
                response.Id = request.updateLeaveRuleDto.RuleId;
                return response;
            }

            _mapper.Map(request.updateLeaveRuleDto, leaveRule);

            await _unitOfWork.Repository<Hrm.Domain.LeaveRules>().Update(leaveRule);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Update Successful";
            response.Id = request.updateLeaveRuleDto.RuleId;

            return response;
        }
    }
}
