using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Grade.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Grade.Handlers.Queries
{

    public class GetSelectGradeRequestHandler : IRequestHandler<GetSelectGradeRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.Grade> _GradeRepository;


        public GetSelectGradeRequestHandler(IHrmRepository<Hrm.Domain.Grade> GradeRepository)
        {
            _GradeRepository = GradeRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectGradeRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.Grade> Grades = await _GradeRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = Grades.Select(x => new SelectedModel
            {
                Name = x.GradeName,
                Id = x.GradeId
            }).ToList();
            return selectModels;
        }
    }
}
