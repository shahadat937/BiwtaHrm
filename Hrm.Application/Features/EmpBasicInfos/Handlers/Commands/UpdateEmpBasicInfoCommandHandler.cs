using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Employee.Requests.Commands;
using Hrm.Application.Features.EmpBasicInfos.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpBasicInfos.Handlers.Commands
{
    public class UpdateEmpBasicInfoCommandHandler : IRequestHandler<UpdateEmpBasicInfoCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<EmpBasicInfo> _EmpBasicInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEmpBasicInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<EmpBasicInfo> EmpBasicInfoRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _EmpBasicInfoRepository = EmpBasicInfoRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateEmpBasicInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var EmpBasicInfo = await _unitOfWork.Repository<EmpBasicInfo>().Get(request.EmpBasicInfoDto.Id);

            _mapper.Map(request.EmpBasicInfoDto, EmpBasicInfo);

            await _unitOfWork.Repository<EmpBasicInfo>().Update(EmpBasicInfo);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Update Successfull";

            return response;
        }

    }
}