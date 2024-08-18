using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Form;
using Hrm.Application.Features.Form.Requests.Queries;
using Hrm.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Form.Handlers.Queries
{
    public class GetFormByIdRequestHandler: IRequestHandler<GetFormByIdRequest, FormDto>
    {
        private readonly IHrmRepository<Domain.Form> _FormRepository;
        private readonly IMapper _mapper;

        public GetFormByIdRequestHandler(IHrmRepository<Domain.Form> formRepository, IMapper mapper)
        {
            _FormRepository = formRepository;
            _mapper = mapper;
        }

        public async Task<FormDto> Handle(GetFormByIdRequest request, CancellationToken cancellationToken)
        {
            var forms = await _FormRepository.Get(request.FormId);
            if(forms == null)
            {
                throw new NotFoundException(nameof(forms), request.FormId);
            }
            var formDtos = _mapper.Map<FormDto>(forms);
            return formDtos;
        }
    }
}
