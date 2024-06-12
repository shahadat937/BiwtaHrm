using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpBankInfos.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpBankInfos.Handlers.Commands
{
    public class CreateEmpBankInfoCommandHandler : IRequestHandler<CreateEmpBankInfoCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<EmpBankInfo> _EmpPersonalInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateEmpBankInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<EmpBankInfo> EmpPersonalInfoRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _EmpPersonalInfoRepository = EmpPersonalInfoRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateEmpBankInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            int empId = 0;

            foreach (var item in request.EmpBankInfoDto)
            {
                empId = item.EmpId;
                if (item.Id == 0 )
                {
                    var EmpBankInfo = _mapper.Map<EmpBankInfo>(item);

                    EmpBankInfo = await _unitOfWork.Repository<EmpBankInfo>().Add(EmpBankInfo);
                }
                else
                {
                    var EmpBankInfo = await _unitOfWork.Repository<EmpBankInfo>().Get(item.Id);

                    _mapper.Map(item, EmpBankInfo);

                    await _unitOfWork.Repository<EmpBankInfo>().Update(EmpBankInfo);
                }
            }

            IQueryable<EmpBankInfo> empBankInfos = _EmpPersonalInfoRepository.Where(x => x.EmpId == empId);

            if (empBankInfos.Any())
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