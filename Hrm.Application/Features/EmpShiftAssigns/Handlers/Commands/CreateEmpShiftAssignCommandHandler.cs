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
    public class CreateEmpShiftAssignCommandHandler : IRequestHandler<CreateEmpShiftAssignCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateEmpShiftAssignCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateEmpShiftAssignCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var EmpShiftAssign = _mapper.Map<EmpShiftAssign>(request.EmpShiftAssignDto);

            EmpShiftAssign = await _unitOfWork.Repository<EmpShiftAssign>().Add(EmpShiftAssign);
            await _unitOfWork.Save();


            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = EmpShiftAssign.Id;

            return response;
        }
    }
}
