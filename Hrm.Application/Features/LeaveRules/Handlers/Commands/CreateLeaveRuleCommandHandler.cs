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
    public class CreateLeaveRuleCommandHandler: IRequestHandler<CreateLeaveRuleCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateLeaveRuleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveRuleCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLeaveRulesDtoValidator();
            var validationResult = await validator.ValidateAsync(request.createleaveRuleDto);

            if(!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }

            Type ruleName = typeof(LeaveRule);
            FieldInfo[] fieldinfo = ruleName.GetFields(BindingFlags.Public | BindingFlags.Static);

            bool isValidRuleName = fieldinfo.Where(x => x.Name == request.createleaveRuleDto.RuleName).Any();

            if(!isValidRuleName)
            {
                response.Success = false;
                response.Message = "Rule name is invalid";
                response.Id = request.createleaveRuleDto.RuleId;
                return response;
            }

            var leaveRule = _mapper.Map<Hrm.Domain.LeaveRules>(request.createleaveRuleDto);
            
            await _unitOfWork.Repository<Hrm.Domain.LeaveRules>().Add(leaveRule);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = leaveRule.RuleId;

            return response;

        }
    }
}
