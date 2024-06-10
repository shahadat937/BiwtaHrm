using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpSpouseInfos.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpSpouseInfos.Handlers.Commands
{
    public class UpdateEmpSpouseInfoCommandHandler : IRequestHandler<UpdateEmpSpouseInfoCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<EmpSpouseInfo> _EmpPersonalInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEmpSpouseInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<EmpSpouseInfo> EmpPersonalInfoRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _EmpPersonalInfoRepository = EmpPersonalInfoRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateEmpSpouseInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var EmpSpouseInfo = await _unitOfWork.Repository<EmpSpouseInfo>().Get(request.EmpSpouseInfoDto.Id);

            _mapper.Map(request.EmpSpouseInfoDto, EmpSpouseInfo);

            await _unitOfWork.Repository<EmpSpouseInfo>().Update(EmpSpouseInfo);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Update Successfull";

            return response;
        }

    }
}
