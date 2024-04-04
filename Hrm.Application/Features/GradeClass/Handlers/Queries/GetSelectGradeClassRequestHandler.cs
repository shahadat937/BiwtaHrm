using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.GradeClass.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.GradeClass.Handlers.Queries
{

    public class GetSelectGradeClassRequestHandler : IRequestHandler<GetSelectGradeClassRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.GradeClass> _GradeClassRepository;


        public GetSelectGradeClassRequestHandler(IHrmRepository<Hrm.Domain.GradeClass> GradeClassRepository)
        {
            _GradeClassRepository = GradeClassRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectGradeClassRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.GradeClass> GradeClasss = await _GradeClassRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = GradeClasss.Select(x => new SelectedModel
            {
                Name = x.GradeClassName,
                Id = x.GradeClassId
            }).ToList();
            return selectModels;
        }
    }
}
