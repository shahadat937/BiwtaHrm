using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Section;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Section.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Section.Handlers.Queries
{
    public class GetExamTypeByIdRequestHandler : IRequestHandler<GetSectionByIdRequest, SectionDto>
    {

        private readonly IHrmRepository<Hrm.Domain.Section> _SectionRepository;
        private readonly IMapper _mapper;
        public GetExamTypeByIdRequestHandler(IHrmRepository<Hrm.Domain.Section> SectionRepositoy, IMapper mapper)
        {
            _SectionRepository = SectionRepositoy;
            _mapper = mapper;
        }

        public async Task<SectionDto> Handle(GetSectionByIdRequest request, CancellationToken cancellationToken)
        {
            var Section = await _SectionRepository.Get(request.SectionId);
            return _mapper.Map<SectionDto>(Section);
        }
    }
}
