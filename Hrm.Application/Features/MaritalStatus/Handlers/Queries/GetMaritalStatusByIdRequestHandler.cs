using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmployeeType;
using Hrm.Application.DTOs.Gender;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.EmployeeType.Requests.Queries;
using Hrm.Application.Features.Gender.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.MaritalStatus.Handlers.Queries
{
    public class GetMaritalStatusByIdRequestHandler : IRequestHandler<GetMaritalStatusByIdRequest, MaritalStatusDto>
    {
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Hrm.Domain.MaritalStatus> _MaritalStatusRepository;
        public GetMaritalStatusByIdRequestHandler(IHrmRepository<Hrm.Domain.MaritalStatus> MaritalStatusRepository, IMapper mapper)
        {
            _MaritalStatusRepository = MaritalStatusRepository;
            _mapper = mapper;
        }
        public async Task<MaritalStatusDto> Handle(GetMaritalStatusByIdRequest request, CancellationToken cancellationToken)
        {
            var MaritalStatus = await _MaritalStatusRepository.Get(request.MaritalStatusId);
            return _mapper.Map<MaritalStatusDto>(MaritalStatus);
        }
    }
}
