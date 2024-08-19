using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.FormFieldType.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FormFieldType.Handlers.Queries
{
    public class GetSelectedFormFieldTypeRequestHandler: IRequestHandler<GetSelectedFormFieldTypeRequest, object>
    {
        private readonly IHrmRepository<Hrm.Domain.FormFieldType> _repository;
        
        public GetSelectedFormFieldTypeRequestHandler(IHrmRepository<Hrm.Domain.FormFieldType> repository)
        {
            _repository = repository;
        }

        public async Task<object> Handle(GetSelectedFormFieldTypeRequest request, CancellationToken cancellationToken)
        {
            var querable = _repository.Where(x => true).AsQueryable();

            var formFieldType = await querable.Select(x => new SelectedModel
            {
                Id = x.FieldTypeId,
                Name = x.FieldTypeName
            }).ToListAsync();

            return formFieldType;
        }
    }
}
