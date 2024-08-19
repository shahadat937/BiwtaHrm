using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.FormSchema;
using Hrm.Application.Features.FormSchema.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FormSchema.Handlers.Queries
{
    public class GetFormSchemaByFilterRequestHandler: IRequestHandler<GetFormSchemaByFilterRequest,List<FormSchemaDto>>
    {
        private readonly IHrmRepository<Hrm.Domain.FormSchema> _repository;
        private readonly IMapper _mapper;

        public GetFormSchemaByFilterRequestHandler(IHrmRepository<Domain.FormSchema> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<FormSchemaDto>> Handle(GetFormSchemaByFilterRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.FormSchema> querable = _repository.Where(x => true)
                .Include(x => x.FormField)
                .Include(x => x.Form)
                .AsQueryable();

            if(request.filters.IsActive.HasValue)
            {
                querable = querable.Where(x=>x.IsActive == request.filters.IsActive);
            }

            if(request.filters.FormId.HasValue)
            {
                querable = querable.Where(x=>x.FormId == request.filters.FormId);
            }

            if(request.filters.FieldId.HasValue)
            {
                querable = querable.Where(x=>x.FieldId == request.filters.FieldId);
            }
            
            if(request.filters.Section.HasValue)
            {
                querable = querable.Where(x=>x.Section == request.filters.Section);
            }

            if(request.filters.SchemaId.HasValue)
            {
                querable = querable.Where(x => x.SchemaId == request.filters.SchemaId);
            }

            var schemas = await querable.ToListAsync();

            var schemaDtos = _mapper.Map<List<FormSchemaDto>>(schemas);

            return schemaDtos;
        }
    }
}
