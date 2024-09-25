using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpOtherResponsibilities.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpOtherResponsibilities.Handlers.Commands
{
    public class CreateEmpOtherResponsibilityCommandHandler : IRequestHandler<CreateEmpOtherResponsibilityCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<EmpOtherResponsibility> _EmpPersonalInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateEmpOtherResponsibilityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<EmpOtherResponsibility> EmpPersonalInfoRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _EmpPersonalInfoRepository = EmpPersonalInfoRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateEmpOtherResponsibilityCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            int empId = 0;

            foreach (var item in request.EmpOtherResponsibilityDto)
            {
                empId = item.EmpId ?? 0;
                if (item.Id == 0 )
                {
                    var EmpOtherResponsibility = _mapper.Map<EmpOtherResponsibility>(item);

                    EmpOtherResponsibility = await _unitOfWork.Repository<EmpOtherResponsibility>().Add(EmpOtherResponsibility);
                }
                else
                {
                    var EmpOtherResponsibility = await _unitOfWork.Repository<EmpOtherResponsibility>().Get(item.Id);

                    _mapper.Map(item, EmpOtherResponsibility);

                    await _unitOfWork.Repository<EmpOtherResponsibility>().Update(EmpOtherResponsibility);
                }
            }

            IQueryable<EmpOtherResponsibility> EmpOtherResponsibilities = _EmpPersonalInfoRepository.Where(x => x.EmpId == empId);

            if (EmpOtherResponsibilities.Any())
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