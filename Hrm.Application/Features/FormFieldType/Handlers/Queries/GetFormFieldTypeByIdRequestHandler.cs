using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.FormFieldType;
using Hrm.Application.Features.FormFieldType.Requests.Queries;
using Hrm.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FormFieldType.Handlers.Queries
{
    public class GetFormFieldTypeByIdRequestHandler: IRequestHandler<GetFormFieldTypeByIdRequest, FormFieldTypeDto>
    {
        private readonly IHrmRepository<Hrm.Domain.FormFieldType> _repository;
        private readonly IMapper _mapper;

        public GetFormFieldTypeByIdRequestHandler(IHrmRepository<Domain.FormFieldType> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<FormFieldTypeDto> Handle(GetFormFieldTypeByIdRequest request, CancellationToken cancellationToken)
        {
            var formFieldType = await _repository.Get(request.FieldTypeId);

            if(formFieldType==null)
            {
                throw new NotFoundException(nameof(formFieldType), request.FieldTypeId);
            }

            var formFieldTypedto = _mapper.Map<FormFieldTypeDto>(formFieldType);

            return formFieldTypedto;
        }
    }
}
