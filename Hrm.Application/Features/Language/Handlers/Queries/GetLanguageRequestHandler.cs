using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Language;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Language.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Language.Handlers.Queries
{
    public class GetLanguageRequestHandler : IRequestHandler<GetLanguageRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.Language> _LanguageRepository;
        private readonly IMapper _mapper;
        public GetLanguageRequestHandler(IHrmRepository<Hrm.Domain.Language> LanguageRepository, IMapper mapper)
        {
            _LanguageRepository = LanguageRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetLanguageRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.Language> Languages = _LanguageRepository.Where(x => true);

            var LanguageDtos = _mapper.Map<List<LanguageDto>>(Languages);

            return LanguageDtos;
        }
    }
}
