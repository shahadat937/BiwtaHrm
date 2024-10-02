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
    public class UpdateEmpOtherResponsibilityStatusCommandHandler : IRequestHandler<UpdateEmpOtherResponsibilityStatusCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<EmpOtherResponsibility> _EmpPersonalInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEmpOtherResponsibilityStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<EmpOtherResponsibility> EmpPersonalInfoRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _EmpPersonalInfoRepository = EmpPersonalInfoRepository;
        }
        public async Task<BaseCommandResponse> Handle(UpdateEmpOtherResponsibilityStatusCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var empOtherResponsibility = await _unitOfWork.Repository<EmpOtherResponsibility>().Get(request.Id);

            empOtherResponsibility.ServiceStatus = false;
            empOtherResponsibility.IsActive = false;

            await _unitOfWork.Repository<EmpOtherResponsibility>().Update(empOtherResponsibility);
            response.Success = true;
            response.Message = "Service Status De-activated";
            
            await _unitOfWork.Save();


            return response;
        }
    }
}