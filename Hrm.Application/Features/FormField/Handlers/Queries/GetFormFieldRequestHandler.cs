using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.FormField;
using Hrm.Application.Features.FormField.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FormField.Handlers.Queries
{
    public class GetFormFieldRequestHandler: IRequestHandler<GetFormFieldRequest, List<FormFieldDto>>
    {
        private readonly IHrmRepository<Hrm.Domain.FormField> _repository;
        private readonly IMapper _mapper;

        public GetFormFieldRequestHandler(IHrmRepository<Domain.FormField> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<FormFieldDto>> Handle(GetFormFieldRequest request, CancellationToken cancellationToken)
        {
            var formField = await _repository.Where(x => true)
                .Include(x => x.FieldType)
                .ToListAsync();

            var formFieldDto = _mapper.Map<List<FormFieldDto>>(formField);

            return formFieldDto;
        }
    }
}
