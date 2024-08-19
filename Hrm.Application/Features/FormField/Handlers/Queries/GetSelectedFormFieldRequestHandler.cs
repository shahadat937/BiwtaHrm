using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.FormField.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FormField.Handlers.Queries
{
    public class GetSelectedFormFieldRequestHandler: IRequestHandler<GetSelectedFormFieldRequest,object>
    {
        private readonly IHrmRepository<Hrm.Domain.FormField> _repository;
        
        public GetSelectedFormFieldRequestHandler(IHrmRepository<Hrm.Domain.FormField> repository)
        {
            _repository = repository;
        }

        public async Task<object> Handle(GetSelectedFormFieldRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.FormField> querable = _repository.Where(x => true)
                .AsQueryable();

            var formFields = await querable.Select(x => new SelectedModel
            {
                Id = x.FieldId,
                Name = x.FieldName
            }).ToListAsync();

            return formFields;
        }
    }
}
