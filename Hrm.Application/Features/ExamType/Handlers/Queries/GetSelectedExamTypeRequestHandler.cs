using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.ExamType.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.ExamType.Handlers.Queries
{ 
    public class GetSelectedExamTypeRequestHandler : IRequestHandler<GetSelectedExamTypeRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.ExamType> _ExamTypeRepository;


        public GetSelectedExamTypeRequestHandler(IHrmRepository<Hrm.Domain.ExamType> ExamTypeRepository)
        {
            _ExamTypeRepository = ExamTypeRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedExamTypeRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.ExamType> ExamTypes = await _ExamTypeRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = ExamTypes.Select(x => new SelectedModel 
            {
                Name = x.ExamTypeName,
                Id = x.ExamTypeId
            }).OrderBy(x => x.Name).ToList();
            return selectModels;
        }
    }
}
 