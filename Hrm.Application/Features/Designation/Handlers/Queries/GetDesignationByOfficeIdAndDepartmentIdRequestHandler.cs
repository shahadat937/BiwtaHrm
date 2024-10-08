using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Designation;
using Hrm.Application.Features.Designation.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Designation.Handlers.Queries
{
    public class GetDesignationByOfficeIdAndDepartmentIdRequestHandler : IRequestHandler<GetDesignationByOfficeIdAndDepartmentIdRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.Designation> _DesignationRepository;
        private readonly IMapper _mapper;
        public GetDesignationByOfficeIdAndDepartmentIdRequestHandler(IHrmRepository<Hrm.Domain.Designation> DesignationRepository, IMapper mapper)
        {
            _DesignationRepository = DesignationRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetDesignationByOfficeIdAndDepartmentIdRequest request, CancellationToken cancellationToken)
        {
            var designations = _DesignationRepository
            .Where(x => true)
            .Include(d => d.Office)
            .Include(d => d.DesignationSetup)
            .Include(d => d.Department)
            .Where(x => x.OfficeId == request.OfficeId && x.DepartmentId == request.DepartmentId)
            .OrderByDescending(x => x.DesignationId);

            var designationDtos = _mapper.Map<List<DesignationDto>>(await designations.ToListAsync(cancellationToken));

            return designationDtos;
        }
    }
}
