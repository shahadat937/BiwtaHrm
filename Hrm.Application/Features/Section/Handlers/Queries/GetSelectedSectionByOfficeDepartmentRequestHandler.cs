using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Section.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Section.Handlers.Queries
{
    public class GetSelectedSectionByOfficeDepartmentRequestHandler : IRequestHandler<GetSelectedSectionByOfficeDepartmentRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.Section> _SectionRepository;


        public GetSelectedSectionByOfficeDepartmentRequestHandler(IHrmRepository<Hrm.Domain.Section> SectionRepository)
        {
            _SectionRepository = SectionRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedSectionByOfficeDepartmentRequest request, CancellationToken cancellationToken)
        {

            //if (request.OfficeId != 0 && request.DepartmentId != 0)
            //{
                ICollection<Hrm.Domain.Section> Sections = await _SectionRepository.FilterAsync(x => x.DepartmentId == request.DepartmentId);
                List<SelectedModel> selectModels = Sections.Select(x => new SelectedModel
                {
                    Name = x.SectionName,
                    Id = x.SectionId
                }).ToList();
                return selectModels;
            //}
            //else
            //{
            //    ICollection<Hrm.Domain.Section> Sections = await _SectionRepository.FilterAsync(x => x.OfficeId == request.OfficeId && x.DepartmentId == null);
            //    List<SelectedModel> selectModels = Sections.Select(x => new SelectedModel
            //    {
            //        Name = x.SectionName,
            //        Id = x.SectionId
            //    }).ToList();
            //    return selectModels;
            //}
            
        }
    }
}