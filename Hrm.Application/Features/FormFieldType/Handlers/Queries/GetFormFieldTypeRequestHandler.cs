using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.FormFieldType;
using Hrm.Application.Features.FormFieldType.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FormFieldType.Handlers.Queries
{
    public class GetFormFieldTypeRequestHandler: IRequestHandler<GetFormFieldTypeRequest, List<FormFieldTypeDto>>
    {
        private readonly IHrmRepository<Hrm.Domain.FormFieldType> _repository;
        private readonly IMapper _mapper;

        public GetFormFieldTypeRequestHandler(IHrmRepository<Domain.FormFieldType> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<FormFieldTypeDto>> Handle(GetFormFieldTypeRequest request, CancellationToken cancellationToken)
        {
            var formFieldTypes = await _repository.Where(x => true).ToListAsync();

            var formFieldTypedtos = _mapper.Map<List<FormFieldTypeDto>>(formFieldTypes);

            return formFieldTypedtos;
        }
    }
}
