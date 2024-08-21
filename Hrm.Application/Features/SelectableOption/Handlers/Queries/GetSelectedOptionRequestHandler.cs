using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.SelectableOption.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SelectableOption.Handlers.Queries
{
    public class GetSelectedOptionRequestHandler: IRequestHandler<GetSelectedOptionRequest, object>
    {
        private readonly IHrmRepository<Hrm.Domain.SelectableOption> _repository;
        private readonly IMapper _mapper;

        public GetSelectedOptionRequestHandler(IHrmRepository<Domain.SelectableOption> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetSelectedOptionRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.SelectableOption> querable = _repository.Where(x => x.IsActive == true).AsQueryable();

            var Options = await querable.Select(x => new SelectedModel
            {
                Id = x.OptionId,
                Name = x.OptionName
            }).ToListAsync();

            return Options;
        }
    }
}
