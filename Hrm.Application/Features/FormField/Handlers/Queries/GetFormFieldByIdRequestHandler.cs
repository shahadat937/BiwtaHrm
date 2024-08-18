using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.FormField;
using Hrm.Application.Features.FormField.Requests.Queries;
using Hrm.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FormField.Handlers.Queries
{
    public class GetFormFieldByIdRequestHandler: IRequestHandler<GetFormFieldByIdRequest,FormFieldDto>
    {
        private readonly IHrmRepository<Hrm.Domain.FormField> _repository;
        private readonly IMapper _mapper;

        public GetFormFieldByIdRequestHandler(IHrmRepository<Domain.FormField> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<FormFieldDto> Handle(GetFormFieldByIdRequest request, CancellationToken cancellationToken)
        {
            var formField = await _repository.Where(x => x.FieldId == request.FieldId).
                Include(x => x.FieldType).FirstOrDefaultAsync();

            if (formField == null)
            {
                throw new NotFoundException(nameof(formField), request.FieldId);
            }

            var formFieldDto = _mapper.Map<FormFieldDto>(formField);

            return formFieldDto;
        }
    }
}
