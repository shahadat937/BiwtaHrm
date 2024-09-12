using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Dashboard.Widgets;
using Hrm.Application.Features.Dashboards.Requests.Queries;
using Hrm.Application.Features.Designation.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Designation.Handlers.Queries
{
    public class GetDesignationLastPositionRequestHandler : IRequestHandler<GetDesignationLastPositionRequest, int>
    {
        private readonly IHrmRepository<Domain.Designation> _DesignationRepository;

        public GetDesignationLastPositionRequestHandler(
            IHrmRepository<Domain.Designation> DesignationRepository)
        {
            _DesignationRepository = DesignationRepository;

        }

        public async Task<int> Handle(GetDesignationLastPositionRequest request, CancellationToken cancellationToken)
        {
            if(request.SectionId == 0)
            {
                var menuPosition = _DesignationRepository.Where(x => x.DepartmentId == request.DepartmentId && x.SectionId == null)
                    .OrderByDescending(x => x.MenuPosition)
                    .Select(x => x.MenuPosition)
                    .FirstOrDefault();
                return menuPosition + 1 ?? 1;
            }
            else
            {
                var menuPosition = _DesignationRepository.Where(x => x.SectionId == request.SectionId)
                    .OrderByDescending(x => x.MenuPosition)
                    .Select(x => x.MenuPosition)
                    .FirstOrDefault();
                return menuPosition + 1 ?? 1;
            }
        }
    }
}