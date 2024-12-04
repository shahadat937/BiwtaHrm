using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpTrainingInfos.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpTrainingInfos.Handlers.Commands
{
    public class CreateEmpTrainingInfoCommandHandler : IRequestHandler<CreateEmpTrainingInfoCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<EmpTrainingInfo> _EmpPersonalInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateEmpTrainingInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<EmpTrainingInfo> EmpPersonalInfoRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _EmpPersonalInfoRepository = EmpPersonalInfoRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateEmpTrainingInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            int empId = 0;

            foreach (var item in request.EmpTrainingInfoDto)
            {
                empId = item.EmpId;
                if (item.Id == 0 )
                {
                    var EmpTrainingInfo = _mapper.Map<EmpTrainingInfo>(item);

                    EmpTrainingInfo = await _unitOfWork.Repository<EmpTrainingInfo>().Add(EmpTrainingInfo);
                }
                else
                {
                    var EmpTrainingInfo = await _unitOfWork.Repository<EmpTrainingInfo>().Get(item.Id);

                    _mapper.Map(item, EmpTrainingInfo);

                    await _unitOfWork.Repository<EmpTrainingInfo>().Update(EmpTrainingInfo);
                }
            }

            IQueryable<EmpTrainingInfo> EmpTrainingInfos = _EmpPersonalInfoRepository.Where(x => x.EmpId == empId);

            if (EmpTrainingInfos.Any())
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