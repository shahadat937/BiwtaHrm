using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Section;
using Hrm.Application.DTOs.Organograms;
using Hrm.Application.DTOs.Section;
using Hrm.Application.Features.Organogram.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.Organogram.Handlers.Queries
{
    public class GetSectionsByDepatmentIdRequestHandler : IRequestHandler<GetSectionsByDepatmentIdRequest, List<SectionDto>>
    {

        private readonly IHrmRepository<Hrm.Domain.Section> _SectionRepository;
        private readonly IHrmRepository<Hrm.Domain.Designation> _DesignationRepository;
        private readonly IMapper _mapper;
        public GetSectionsByDepatmentIdRequestHandler(IHrmRepository<Hrm.Domain.Section> SectionRepository, IMapper mapper, IHrmRepository<Domain.Designation> designationRepository)
        {
            _SectionRepository = SectionRepository;
            _mapper = mapper;
            _DesignationRepository = designationRepository;
        }

        public async Task<List<SectionDto>> Handle(GetSectionsByDepatmentIdRequest request, CancellationToken cancellationToken)
        {
            var Sections = _SectionRepository.FilterWithInclude(x => x.DepartmentId == request.DepartmentId && (request.UpperSectionId == 0 ? x.UpperSectionId == null && x.IsActive == true : x.UpperSectionId == request.UpperSectionId && x.IsActive == true)).OrderBy(x => x.Sequence ?? int.MaxValue);

            var SectionList = await Sections.ToListAsync(cancellationToken);

            foreach (var section in SectionList)
            {
                // Count designations for each department
                int count = await _DesignationRepository.CountAsync(x => x.DepartmentId == section.DepartmentId && x.SectionId == section.SectionId && x.IsActive==true);

                // Append count to the department name
                section.SectionName = $"{section.SectionName} ({count})";
            }

            var SectionDtos = _mapper.Map<List<SectionDto>>(SectionList);
            return SectionDtos;
        }

        //public async Task<List<SectionDto>> Handle(GetSectionsByDepatmentIdRequest request, CancellationToken cancellationToken)
        //{
        //    // Load all sections of the department
        //    var allSections = await _SectionRepository
        //        .FilterWithInclude(x => x.DepartmentId == request.DepartmentId)
        //        .OrderBy(x => x.Sequence ?? int.MaxValue)
        //        .ToListAsync(cancellationToken);

        //    // Determine root sections
        //    var rootSections = request.UpperSectionId == 0
        //        ? allSections.Where(x => x.UpperSectionId == null).ToList()
        //        : allSections.Where(x => x.UpperSectionId == request.UpperSectionId).ToList();

        //    var resultSections = new List<Hrm.Domain.Section>();

        //    foreach (var root in rootSections)
        //    {
        //        resultSections.Add(root);
        //        resultSections.AddRange(GetAllChildSections(allSections, root.SectionId));
        //    }

        //    // Count related entities per section (replace with your logic)
        //    foreach (var section in resultSections)
        //    {
        //        int count = await _DesignationRepository.CountAsync(e => e.SectionId == section.SectionId); // Replace with your entity & condition
        //        section.SectionName = $"{section.SectionName} ({count})";
        //    }

        //    var sectionDtos = _mapper.Map<List<SectionDto>>(resultSections);
        //    return sectionDtos;
        //}

        //private List<Hrm.Domain.Section> GetAllChildSections(List<Hrm.Domain.Section> allSections, int parentId)
        //{
        //    var result = new List<Hrm.Domain.Section>();
        //    var children = allSections.Where(s => s.UpperSectionId == parentId).ToList();

        //    foreach (var child in children)
        //    {
        //        result.Add(child);
        //        result.AddRange(GetAllChildSections(allSections, child.SectionId));
        //    }

        //    return result;
        //}

    }
}
