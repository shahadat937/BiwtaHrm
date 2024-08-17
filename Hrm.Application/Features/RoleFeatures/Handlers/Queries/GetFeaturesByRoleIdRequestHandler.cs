using Hrm.Application.DTOs.RoleFeatures;
using Hrm.Application.Features.RoleFeatures.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.RoleFeatures.Handlers.Queries
{
    public class GetFeaturesByRoleIdRequestHandler
    {
         //: IRequestHandler<GetFeaturesByRoleIdRequest, List<FeatureDto>>
        //private readonly IApplicationDbContext _context;

        //public GetFeaturesByRoleIdRequestHandler(IApplicationDbContext context)
        //{
        //    _context = context;
        //}

        //public async Task<List<RoleFeatureDto>> Handle(GetFeaturesByRoleIdRequest request, CancellationToken cancellationToken)
        //{
        //    var features = await _context.RoleFeatures
        //        .Where(rf => rf.RoleId == request.RoleId)
        //        .Join(
        //            _context.Features,
        //            rf => rf.FeatureKey,
        //            f => f.FeatureId,
        //            (rf, f) => new FeatureDto
        //            {
        //                FeatureId = f.FeatureId,
        //                FeatureName = f.FeatureName,
        //                ViewStatus = rf.ViewStatus,
        //                Add = rf.Add,
        //                Update = rf.Update,
        //                Delete = rf.Delete
        //            }
        //        ).ToListAsync(cancellationToken);

        //    return features;
        //}
    }

}
