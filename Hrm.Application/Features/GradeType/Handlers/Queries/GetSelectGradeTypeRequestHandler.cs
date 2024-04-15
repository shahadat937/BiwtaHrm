using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.GradeType.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.GradeType.Handlers.Queries
{

    public class GetSelectGradeTypeRequestHandler : IRequestHandler<GetSelectGradeTypeRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.GradeType> _GradeTypeRepository;


        public GetSelectGradeTypeRequestHandler(IHrmRepository<Hrm.Domain.GradeType> GradeTypeRepository)
        {
            _GradeTypeRepository = GradeTypeRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectGradeTypeRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.GradeType> GradeTypes = await _GradeTypeRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = GradeTypes.Select(x => new SelectedModel
            {
                Name = x.GradeTypeName,
                Id = x.GradeTypeId
            }).ToList();
            return selectModels;
        }
    }
}
