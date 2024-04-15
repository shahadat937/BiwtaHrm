using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.ChildStatus;
using Hrm.Application.DTOs.EmployeeType;
using Hrm.Application.DTOs.Religion;
using Hrm.Application.Features.BloodGroups.Requests.Queries;
using Hrm.Application.Features.ChildStatus.Requests.Queries;
using Hrm.Application.Features.EmployeeType.Requests.Queries;
using Hrm.Application.Features.Religion.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmployeeType.Handlers.Queries
{
    public class GetEmployeeTypeRequestByIdHandler : IRequestHandler<GetEmployeeTypeByIdRequest, EmployeeTypeDto>
    {
        // private readonly IUTOfficerTypeRepository _UTOfficerTypeRepository;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Hrm.Domain.EmployeeType> _EmployeeTypeRepository;
        public GetEmployeeTypeRequestByIdHandler(IHrmRepository<Hrm.Domain.EmployeeType> EmployeeTypeRepository, IMapper mapper)
        {
            _EmployeeTypeRepository = EmployeeTypeRepository;
            _mapper = mapper;
        }
        public async Task<EmployeeTypeDto> Handle(GetEmployeeTypeByIdRequest request, CancellationToken cancellationToken)
        {
            var ChildStatus = await _EmployeeTypeRepository.Get(request.EmployeeTypeId);
            return _mapper.Map<EmployeeTypeDto>(ChildStatus);
        }
    }
}
