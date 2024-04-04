using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Subject.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Subject.Handlers.Queries
{ 
    public class GetSelectedSubjectRequestHandler : IRequestHandler<GetSelectedSubjectRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.Subject> _SubjectRepository;


        public GetSelectedSubjectRequestHandler(IHrmRepository<Hrm.Domain.Subject> SubjectRepository)
        {
            _SubjectRepository = SubjectRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedSubjectRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.Subject> Subjects = await _SubjectRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = Subjects.Select(x => new SelectedModel 
            {
                Name = x.SubjectName,
                Id = x.SubjectId
            }).ToList();
            return selectModels;
        }
    }
}
 