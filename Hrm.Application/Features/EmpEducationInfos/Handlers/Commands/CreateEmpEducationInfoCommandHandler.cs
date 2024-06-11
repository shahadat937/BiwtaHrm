using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpEducationInfos.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpEducationInfos.Handlers.Commands
{
    public class CreateEmpEducationInfoCommandHandler : IRequestHandler<CreateEmpEducationInfoCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<EmpEducationInfo> _EmpPersonalInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateEmpEducationInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<EmpEducationInfo> EmpPersonalInfoRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _EmpPersonalInfoRepository = EmpPersonalInfoRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateEmpEducationInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            int empId = 0;

            foreach (var item in request.EmpEducationInfoDto)
            {
                empId = item.EmpId;
                if (item.Id == 0 )
                {
                    var EmpEducationInfo = _mapper.Map<EmpEducationInfo>(item);

                    EmpEducationInfo = await _unitOfWork.Repository<EmpEducationInfo>().Add(EmpEducationInfo);
                }
                else
                {
                    var EmpEducationInfo = await _unitOfWork.Repository<EmpEducationInfo>().Get(item.Id);

                    _mapper.Map(item, EmpEducationInfo);

                    await _unitOfWork.Repository<EmpEducationInfo>().Update(EmpEducationInfo);
                }
            }

            IQueryable<EmpEducationInfo> empEducationInfos = _EmpPersonalInfoRepository.Where(x => x.EmpId == empId);

            if (empEducationInfos.Any())
            {
                response.Success = true;
                response.Message = "Update Successful";
            }
            else
            {
                response.Success = true;
                response.Message = "Create Successful";
            }
            await _unitOfWork.Save();


            return response;
        }
    }
}