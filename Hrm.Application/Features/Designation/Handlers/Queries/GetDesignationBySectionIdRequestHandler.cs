using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Designation.Requests.Queries;
using Hrm.Application.Features.OfficeBranch.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Designation.Handlers.Queries
{
    public class GetDesignationBySectionIdRequestHandler : IRequestHandler<GetDesignationBySectionIdRequest, List<SelectedModel>>
    {

        private readonly IHrmRepository<Hrm.Domain.Designation> _DesignationRepository;
        private readonly IMapper _mapper;
        public GetDesignationBySectionIdRequestHandler(IHrmRepository<Hrm.Domain.Designation> DesignationRepository, IMapper mapper)
        {
            _DesignationRepository = DesignationRepository;
            _mapper = mapper;

        }

        public async Task<List<SelectedModel>> Handle(GetDesignationBySectionIdRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.Designation> Designations = _DesignationRepository.Where(x => x.SectionId == request.SectionId)
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

