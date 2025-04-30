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

namespace Hrm.Application.Features.Organogram.Handlers.Queries
{
    public class GetSectionsByDepatmentIdRequestHandler : IRequestHandler<GetSectionsByDepatmentIdRequest, List<SectionDto>>
    {

        private readonly IHrmRepository<Hrm.Domain.Section> _SectionRepository;
        private readonly IMapper _mapper;
        public GetSectionsByDepatmentIdRequestHandler(IHrmRepository<Hrm.Domain.Section> SectionRepository, IMapper mapper)
        {
            _SectionRepository = SectionRepository;
            _mapper = mapper;
        }

        public async Task<List<SectionDto>> Handle(GetSectionsByDepatmentIdRequest request, CancellationToken cancellationToken)
        {
            var Sections = _SectionRepository.FilterWithInclude(x => x.DepartmentId == request.DepartmentId && (request.UpperSectionId == 0? x.UpperSectionId == null : x.UpperSectionId == request.UpperSectionId));
            var SectionDtos = _mapper.Map<List<SectionDto>>(Sections);
            return SectionDtos;
        }
    }
}
