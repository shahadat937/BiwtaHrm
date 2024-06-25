using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpLanguageInfos.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpLanguageInfos.Handlers.Commands
{
    public class CreateEmpLanguageInfoCommandHandler : IRequestHandler<CreateEmpLanguageInfoCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<EmpLanguageInfo> _EmpPersonalInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateEmpLanguageInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<EmpLanguageInfo> EmpPersonalInfoRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _EmpPersonalInfoRepository = EmpPersonalInfoRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateEmpLanguageInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            int empId = 0;

            foreach (var item in request.EmpLanguageInfoDto)
            {
                empId = item.EmpId;
                if (item.Id == 0 )
                {
                    var EmpLanguageInfo = _mapper.Map<EmpLanguageInfo>(item);

                    EmpLanguageInfo = await _unitOfWork.Repository<EmpLanguageInfo>().Add(EmpLanguageInfo);
                }
                else
                {
                    var EmpLanguageInfo = await _unitOfWork.Repository<EmpLanguageInfo>().Get(item.Id);

                    _mapper.Map(item, EmpLanguageInfo);

                    await _unitOfWork.Repository<EmpLanguageInfo>().Update(EmpLanguageInfo);
                }
            }

            IQueryable<EmpLanguageInfo> empLanguageInfos = _EmpPersonalInfoRepository.Where(x => x.EmpId == empId);

            if (empLanguageInfos.Any())
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