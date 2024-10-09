using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Designations.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Designations.Handlers.Queries
{ 
    public class GetSelectedDesignationRequestHandler : IRequestHandler<GetSelectedDesignationRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.Designation> _DesignationRepository;


        public GetSelectedDesignationRequestHandler(IHrmRepository<Hrm.Domain.Designation> DesignationRepository)
        {
            _DesignationRepository = DesignationRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedDesignationRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.Designation> Designations = _DesignationRepository.Where(x => x.IsActive)
                    .Include(x => x.DesignationSetup);

            List<Hrm.Domain.Designation> designations = await Designations.ToListAsync(cancellationToken);

            List<SelectedModel> selectModels = designations
                .GroupBy(x => x.DesignationSetupId)
                .Select(x => x.FirstOrDefault())
                .Select(x => new SelectedModel 
            {
                Name = x.DesignationSetup.Name,
                Id = x.DesignationId
            }).ToList();
            return selectModels;
        }
    }
}
 