using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Employee.Requests.Commands;
using Hrm.Application.Features.EmpPersonalInfos.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpPersonalInfos.Handlers.Commands
{
    public class UpdateEmpPersonalInfoCommandHandler : IRequestHandler<UpdateEmpPersonalInfoCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<EmpPersonalInfo> _EmpPersonalInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEmpPersonalInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<EmpPersonalInfo> EmpPersonalInfoRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _EmpPersonalInfoRepository = EmpPersonalInfoRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateEmpPersonalInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var EmpPersonalInfo = await _unitOfWork.Repository<EmpPersonalInfo>().Get(request.EmpPersonalInfoDto.Id);

            _mapper.Map(request.EmpPersonalInfoDto, EmpPersonalInfo);

            await _unitOfWork.Repository<EmpPersonalInfo>().Update(EmpPersonalInfo);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Update Successfull";

            return response;
        }

    }
}