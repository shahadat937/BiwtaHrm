using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmployeeType;
using Hrm.Application.Features.EmployeeType.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmployeeType.Handlers.Queries
{
    public class GetFirstEmployeeTypeIdRequestHandler : IRequestHandler<GetFirstEmployeeTypeIdRequest, int>
    {
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Hrm.Domain.EmployeeType> _EmployeeTypeRepository;
        public GetFirstEmployeeTypeIdRequestHandler(IHrmRepository<Hrm.Domain.EmployeeType> EmployeeTypeRepository, IMapper mapper)
        {
            _EmployeeTypeRepository = EmployeeTypeRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(GetFirstEmployeeTypeIdRequest request, CancellationToken cancellationToken)
        {
            var employeeTypeId = await _EmployeeTypeRepository.FindOneAsync(x => x.IsActive == true);
            return employeeTypeId.EmployeeTypeId;
        }
    }
}
