using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Section;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Section.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.DTOs.Designation;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.Section.Handlers.Queries
{
    public class GetSectionRequestHandler : IRequestHandler<GetSectionRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.Section> _SectionRepository;
        private readonly IMapper _mapper;
        public GetSectionRequestHandler(IHrmRepository<Hrm.Domain.Section> SectionRepository, IMapper mapper)
        {
            _SectionRepository = SectionRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetSectionRequest request, CancellationToken cancellationToken)
        {
            var Sections = _SectionRepository.Where(x => true).OrderByDescending(x => x.SectionId);

            var SectionDtos = _mapper.Map<List<CreateSectionDto>>(await Sections.ToListAsync(cancellationToken));

            return SectionDtos;
        }
    }
}
