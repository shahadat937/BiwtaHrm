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
    public class UpdateEmpRewardPunishmentCommandHandler : IRequestHandler<UpdateEmpRewardPunishmentCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<EmpRewardPunishment> _EmpPersonalInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEmpRewardPunishmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<EmpRewardPunishment> EmpPersonalInfoRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _EmpPersonalInfoRepository = EmpPersonalInfoRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateEmpRewardPunishmentCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var EmpRewardPunishment = await _unitOfWork.Repository<EmpRewardPunishment>().Get(request.EmpRewardPunishmentDto.Id);

            _mapper.Map(request.EmpRewardPunishmentDto, EmpRewardPunishment);

            await _unitOfWork.Repository<EmpRewardPunishment>().Update(EmpRewardPunishment);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Update Successfull";

            return response;
        }

    }
}