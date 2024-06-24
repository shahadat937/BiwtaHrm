using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Employee.Requests.Commands;
using Hrm.Application.Features.EmployeeType.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Employee.Handlers.Commands
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Employees> _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEmployeeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Employees> employeeRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _employeeRepository = employeeRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var Employee = await _unitOfWork.Repository<Employees>().Get(request.EmployeesDto.EmpId);

            _mapper.Map(request.EmployeesDto, Employee);

            await _unitOfWork.Repository<Employees>().Update(Employee);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Update Successfull";

            return response;
        }

    }
}
