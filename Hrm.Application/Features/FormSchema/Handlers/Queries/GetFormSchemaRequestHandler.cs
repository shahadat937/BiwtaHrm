using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.FormSchema;
using Hrm.Application.Features.FormSchema.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FormSchema.Handlers.Queries
{
    public class GetFormSchemaRequestHandler: IRequestHandler<GetFormSchemaRequest,List<FormSchemaDto>>
    {
        private readonly IHrmRepository<Hrm.Domain.FormSchema> _repository;
        private readonly IMapper _mapper;

        public GetFormSchemaRequestHandler(IHrmRepository<Domain.FormSchema> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<FormSchemaDto>> Handle(GetFormSchemaRequest request, CancellationToken cancellationToken)
        {
            var formSchema = await _repository.Where(x => true)
                .Include(x => x.FormField)
                .Include(x => x.Form)
                .ToListAsync();

            var formSchemaList = _mapper.Map<List<FormSchemaDto>>(formSchema);

            return formSchemaList;
        }
    }
}
