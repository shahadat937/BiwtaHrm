using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpPsiTrainingInfos.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpPsiTrainingInfos.Handlers.Commands
{
    public class CreateEmpPsiTrainingInfoCommandHandler : IRequestHandler<CreateEmpPsiTrainingInfoCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<EmpPsiTrainingInfo> _EmpPersonalInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateEmpPsiTrainingInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<EmpPsiTrainingInfo> EmpPersonalInfoRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _EmpPersonalInfoRepository = EmpPersonalInfoRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateEmpPsiTrainingInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            int empId = 0;

            foreach (var item in request.EmpPsiTrainingInfoDto)
            {
                empId = item.EmpId;
                if (item.Id == 0 )
                {
                    var EmpPsiTrainingInfo = _mapper.Map<EmpPsiTrainingInfo>(item);

                    EmpPsiTrainingInfo = await _unitOfWork.Repository<EmpPsiTrainingInfo>().Add(EmpPsiTrainingInfo);
                }
                else
                {
                    var EmpPsiTrainingInfo = await _unitOfWork.Repository<EmpPsiTrainingInfo>().Get(item.Id);

                    _mapper.Map(item, EmpPsiTrainingInfo);

                    await _unitOfWork.Repository<EmpPsiTrainingInfo>().Update(EmpPsiTrainingInfo);
                }
            }

            IQueryable<EmpPsiTrainingInfo> empPsiTrainingInfos = _EmpPersonalInfoRepository.Where(x => x.EmpId == empId);

            if (empPsiTrainingInfos.Any())
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