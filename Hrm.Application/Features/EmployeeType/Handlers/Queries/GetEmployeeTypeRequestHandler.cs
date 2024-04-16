using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.EmployeeType;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmployeeType.Handlers.Queries
{
    public class GetEmployeeTypeRequestHandler : IRequestHandler<GetEmployeeTypeRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.EmployeeType> _EmployeeTypeRepository;
        private readonly IMapper _mapper;
        public GetEmployeeTypeRequestHandler(IHrmRepository<Hrm.Domain.EmployeeType> EmployeeTypeRepository, IMapper mapper)
        {
            _EmployeeTypeRepository = EmployeeTypeRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetEmployeeTypeRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.EmployeeType> EmployeeType = _EmployeeTypeRepository.Where(x => true);

            EmployeeType = EmployeeType.OrderByDescending(x => x.EmployeeTypeId);

            var EmployeeTypeDtos = _mapper.Map<List<EmployeeTypeDto>>(EmployeeType);

            return EmployeeTypeDtos;
        }
    }
}
