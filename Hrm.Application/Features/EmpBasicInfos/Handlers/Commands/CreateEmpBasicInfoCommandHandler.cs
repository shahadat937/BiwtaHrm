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
    public class CreateEmpBasicInfoCommandHandler : IRequestHandler<CreateEmpBasicInfoCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<EmpBasicInfo> _EmpBasicInfoRepository;
        public CreateEmpBasicInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<EmpBasicInfo> EmpBasicInfoRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _EmpBasicInfoRepository = EmpBasicInfoRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateEmpBasicInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var FindPNo = await _EmpBasicInfoRepository.FindOneAsync(x=>x.PersonalFileNo == request.EmpBasicInfoDto.PersonalFileNo);
            var IdCardNo = await _EmpBasicInfoRepository.FindOneAsync(x => x.IdCardNo == request.EmpBasicInfoDto.IdCardNo);

            if (FindPNo != null && request.EmpBasicInfoDto.PersonalFileNo != null)
            {
                response.Success = false;
                response.Message = $"Creation Failed Personal File No '{request.EmpBasicInfoDto.PersonalFileNo}' already Exists";
            }

            else if (IdCardNo != null)
            {
                response.Success = false;
                response.Message = $"Creation Failed PMIS ID '{request.EmpBasicInfoDto.IdCardNo}' already Exists";
            }
            else
            {
                var EmpBasicInfo = _mapper.Map<EmpBasicInfo>(request.EmpBasicInfoDto);

                EmpBasicInfo = await _unitOfWork.Repository<EmpBasicInfo>().Add(EmpBasicInfo);
                await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = EmpBasicInfo.Id;
            }

            return response;
        }
    }
}
