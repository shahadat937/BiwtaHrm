using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpRewardPunishments.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpRewardPunishments.Handlers.Commands
{
    public class CreateEmpRewardPunishmentCommandHandler : IRequestHandler<CreateEmpRewardPunishmentCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateEmpRewardPunishmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateEmpRewardPunishmentCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var EmpRewardPunishment = _mapper.Map<EmpRewardPunishment>(request.EmpRewardPunishmentDto);

            EmpRewardPunishment = await _unitOfWork.Repository<EmpRewardPunishment>().Add(EmpRewardPunishment);
            await _unitOfWork.Save();


            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = EmpRewardPunishment.Id;

            return response;
        }
    }
}