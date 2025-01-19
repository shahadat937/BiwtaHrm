using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.FormSection;
using Hrm.Application.Features.FormSection.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.FormSection.Handlers.Queries
{
    public class GetFormSectionRequestHandler : IRequestHandler<GetFormSectionRequest,List<GetFormSectionDto>>
    {
        private readonly IHrmRepository<Hrm.Domain.FormSection> _FormSectionRepo;
        private readonly IMapper _mapper;

        public GetFormSectionRequestHandler(IHrmRepository<Domain.FormSection> formSectionRepo, IMapper mapper)
        {
            _FormSectionRepo = formSectionRepo;
            _mapper = mapper;
        }

        public async Task<List<GetFormSectionDto>> Handle(GetFormSectionRequest request, CancellationToken cancellationToken)
        {
            var FormSection = await _FormSectionRepo.Where(x => true).ToListAsync();

            var FormSectionDto = _mapper.Map<List<GetFormSectionDto>>(FormSection);

            return FormSectionDto;
        }
    }
}
