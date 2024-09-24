using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.ResponsibilityType;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.ResponsibilityType.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.DTOs.ResponsibilityType;
using Hrm.Application.Features.ResponsibilityType.Requests.Queries;

namespace Hrm.Application.Features.ResponsibilityType.Handlers.Queries
{
    public class GetResponsibilityTypeRequestHandler : IRequestHandler<GetResponsibilityTypeRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.ResponsibilityType> _ResponsibilityTypeRepository;
        private readonly IMapper _mapper;
        public GetResponsibilityTypeRequestHandler(IHrmRepository<Hrm.Domain.ResponsibilityType> ResponsibilityTypeRepository, IMapper mapper)
        {
            _ResponsibilityTypeRepository = ResponsibilityTypeRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetResponsibilityTypeRequest request, CancellationToken cancellationToken)
        {
            // Fetch blood groups from repository
            IQueryable<Hrm.Domain.ResponsibilityType> ResponsibilityTypes = _ResponsibilityTypeRepository.Where(x => true);

            // Order blood groups by descending order
            ResponsibilityTypes = ResponsibilityTypes.OrderByDescending(x => x.Id);

            // Map the ordered blood groups to ResponsibilityTypeDto
            var ResponsibilityTypeDtos = _mapper.Map<List<ResponsibilityTypeDto>>(ResponsibilityTypes.ToList());

            return ResponsibilityTypeDtos;
        }
    }
}
