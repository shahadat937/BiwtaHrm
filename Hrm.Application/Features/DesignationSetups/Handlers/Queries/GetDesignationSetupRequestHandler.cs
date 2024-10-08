using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.DesignationSetup;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.DesignationSetups.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.DesignationSetups.Handlers.Queries
{
    public class GetDesignationSetupRequestHandler : IRequestHandler<GetDesignationSetupRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.DesignationSetup> _DesignationSetupRepository;
        private readonly IMapper _mapper;
        public GetDesignationSetupRequestHandler(IHrmRepository<Hrm.Domain.DesignationSetup> DesignationSetupRepository, IMapper mapper)
        {
            _DesignationSetupRepository = DesignationSetupRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetDesignationSetupRequest request, CancellationToken cancellationToken)
        {
            // Fetch blood groups from repository
            IQueryable<Hrm.Domain.DesignationSetup> DesignationSetups = _DesignationSetupRepository.Where(x => true);

            // Order blood groups by descending order
            DesignationSetups = DesignationSetups.OrderByDescending(x => x.Id);

            // Map the ordered blood groups to DesignationSetupDto
            var DesignationSetupDtos = _mapper.Map<List<DesignationSetupDto>>(DesignationSetups.ToList());

            return DesignationSetupDtos;
        }
    }
}
