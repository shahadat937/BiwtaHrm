using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.FormSchema;
using Hrm.Application.Features.FormSchema.Requests.Queries;
using Hrm.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FormSchema.Handlers.Queries
{
    public class GetFormSchemaByIdRequestHandler: IRequestHandler<GetFormSchemaByIdRequest, FormSchemaDto>
    {
        private readonly IHrmRepository<Hrm.Domain.FormSchema> _repository;
        private readonly IMapper _mapper;

        public GetFormSchemaByIdRequestHandler(IHrmRepository<Domain.FormSchema> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<FormSchemaDto> Handle(GetFormSchemaByIdRequest request, CancellationToken cancellationToken)
        {
            var Schema = await _repository.Where(x => x.SchemaId == request.SchemaId)
                .Include(x => x.FormField)
                .Include(x => x.Form)
                .FirstOrDefaultAsync();

            if(Schema == null)
            {
                throw new NotFoundException(nameof(Schema),request.SchemaId);
            }

            var schemaDto = _mapper.Map<FormSchemaDto>(Schema);

            return schemaDto;
        }
    }
}
