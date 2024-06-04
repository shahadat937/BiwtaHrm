using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Employee;
using Hrm.Application.DTOs.EmployeeType;
using Hrm.Application.Features.Employee.Requests.Queries;
using Hrm.Application.Features.EmployeeType.Handlers.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Employee.Handlers.Queries
{
    public class GetAllEmployeeRequestHandler : IRequestHandler<GetAllEmployeeRequest, object>
    {

        private readonly IHrmRepository<Employees> _EmployeeRepository;
        private readonly IMapper _mapper;
        public GetAllEmployeeRequestHandler(IHrmRepository<Employees> EmployeeRepository, IMapper mapper)
        {
            _EmployeeRepository = EmployeeRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetAllEmployeeRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Employees> Employee = _EmployeeRepository.Where(x => true);

            Employee = Employee.OrderByDescending(x => x.EmpId);

            var EmployeeDtos = _mapper.Map<List<EmployeesDto>>(Employee);

            return EmployeeDtos;
        }
    }
}