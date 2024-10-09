using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.OfficeBranch.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.OfficeBranch.Handlers.Queries
{
    public class GetDesignationByDepartmentIdRequestHandler : IRequestHandler<GetDesignationByDepartmentIdRequest, List<SelectedModel>>
    {

        private readonly IHrmRepository<Hrm.Domain.Designation> _DesignationRepository;
        private readonly IMapper _mapper;
        public GetDesignationByDepartmentIdRequestHandler(IHrmRepository<Hrm.Domain.Designation> DesignationRepository, IMapper mapper)
        {
            _DesignationRepository = DesignationRepository;
            _mapper = mapper;

        }

        public async Task<List<SelectedModel>> Handle(GetDesignationByDepartmentIdRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.Designation> Designations = _DesignationRepository.Where(x => x.DepartmentId == request.DepartmentId && x.SectionId == null)
                .Include(x => x.DesignationSetup);

            List<Hrm.Domain.Designation> designations = await Designations.ToListAsync(cancellationToken);

            List<SelectedModel> SelectedModel = designations
                    .GroupBy(x => x.DesignationSetupId)
                    .Select(x => x.FirstOrDefault())
                    .Select(x => new SelectedModel
            {
                Id = x.DesignationId,
                Name = x.DesignationSetup.Name,
            }).ToList();
            return SelectedModel;
        }
    }
}
