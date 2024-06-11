using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpChildInfos.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpChildInfos.Handlers.Commands
{
    public class CreateEmpChildInfoCommandHandler : IRequestHandler<CreateEmpChildInfoCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<EmpChildInfo> _EmpPersonalInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateEmpChildInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<EmpChildInfo> EmpPersonalInfoRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _EmpPersonalInfoRepository = EmpPersonalInfoRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateEmpChildInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            int empId = 0;

            foreach (var item in request.EmpChildInfoDto)
            {
                empId = item.EmpId;
                if (item.Id == 0 )
                {
                    var EmpChildInfo = _mapper.Map<EmpChildInfo>(item);

                    EmpChildInfo = await _unitOfWork.Repository<EmpChildInfo>().Add(EmpChildInfo);
                }
                else
                {
                    var EmpChildInfo = await _unitOfWork.Repository<EmpChildInfo>().Get(item.Id);

                    _mapper.Map(item, EmpChildInfo);

                    await _unitOfWork.Repository<EmpChildInfo>().Update(EmpChildInfo);
                }
            }

            IQueryable<EmpChildInfo> empChildInfos = _EmpPersonalInfoRepository.Where(x => x.EmpId == empId);

            if (empChildInfos.Any())
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