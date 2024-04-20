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
    public class GetLanguageByIdRequestHandler : IRequestHandler<GetLanguageByIdRequest, LanguageDto>
    {

        private readonly IHrmRepository<Hrm.Domain.Language> _LanguageRepository;
        private readonly IMapper _mapper;
        public GetLanguageByIdRequestHandler(IHrmRepository<Hrm.Domain.Language> LanguageRepositoy, IMapper mapper)
        {
            _LanguageRepository = LanguageRepositoy;
            _mapper = mapper;
        }

        public async Task<LanguageDto> Handle(GetLanguageByIdRequest request, CancellationToken cancellationToken)
        {
            var Language = await _LanguageRepository.Get(request.LanguageId);
            return _mapper.Map<LanguageDto>(Language);
        }
    }
}
