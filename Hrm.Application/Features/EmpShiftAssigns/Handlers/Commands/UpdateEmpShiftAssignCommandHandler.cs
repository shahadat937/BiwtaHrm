using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Employee.Requests.Commands;
using Hrm.Application.Features.EmpShiftAssigns.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpShiftAssigns.Handlers.Commands
{
    public class UpdateEmpShiftAssignCommandHandler : IRequestHandler<UpdateEmpShiftAssignCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<EmpShiftAssign> _EmpShiftAssignRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEmpShiftAssignCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<EmpShiftAssign> EmpShiftAssignRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _EmpShiftAssignRepository = EmpShiftAssignRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateEmpShiftAssignCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var EmpShiftAssign = await _unitOfWork.Repository<EmpShiftAssign>().Get(request.EmpShiftAssignDto.Id);

            _mapper.Map(request.EmpShiftAssignDto, EmpShiftAssign);

            await _unitOfWork.Repository<EmpShiftAssign>().Update(EmpShiftAssign);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Update Successfull";

            return response;
        }

    }
}