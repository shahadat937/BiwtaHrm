using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Designation.Requests.Queries;
using Hrm.Application.Features.Section.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Section.Handlers.Queries
{
    public class GetSectionLastSequenceRequestHandler : IRequestHandler<GetSectionLastSequenceRequest, int>
    {
        private readonly IHrmRepository<Domain.Section> _SectionRepository;

        public GetSectionLastSequenceRequestHandler(
            IHrmRepository<Domain.Section> SectionRepository)
        {
            _SectionRepository = SectionRepository;

        }

        public async Task<int> Handle(GetSectionLastSequenceRequest request, CancellationToken cancellationToken)
        {
            if (request.SectionId == 0)
            {
                var menuPosition = _SectionRepository.Where(x => x.DepartmentId == request.DepartmentId && x.UpperSectionId == null)
                    .OrderByDescending(x => x.Sequence)
                    .Select(x => x.Sequence)
                    .FirstOrDefault();
                return menuPosition + 1 ?? 1;
            }
            else
            {
                var menuPosition = _SectionRepository.Where(x => x.UpperSectionId == request.SectionId)
                    .OrderByDescending(x => x.Sequence)
                    .Select(x => x.Sequence)
                    .FirstOrDefault();
                return menuPosition + 1 ?? 1;
            }
        }
    }
}
