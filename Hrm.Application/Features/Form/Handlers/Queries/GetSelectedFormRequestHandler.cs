using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Form.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Form.Handlers.Queries
{
    public class GetSelectedFormRequestHandler: IRequestHandler<GetSelectedFormRequest, object>
    {
        private readonly IHrmRepository<Hrm.Domain.Form> _FormRepository;

        public GetSelectedFormRequestHandler(IHrmRepository<Domain.Form> formRepository)
        {
            _FormRepository = formRepository;
        }

        public async Task<object> Handle(GetSelectedFormRequest request, CancellationToken cancellationToken)
        {

            IQueryable<Hrm.Domain.Form> querable = _FormRepository.Where(x => true).AsQueryable();

            var formList = await querable.Select(x => new SelectedModel
            {
                Id = x.FormId,
                Name = x.FormName
            }).ToListAsync();

            return formList;
        }
    }
}
