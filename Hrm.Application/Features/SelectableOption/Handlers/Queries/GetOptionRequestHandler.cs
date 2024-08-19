using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.SelectableOption;
using Hrm.Application.Features.SelectableOption.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SelectableOption.Handlers.Queries
{
    public class GetOptionRequestHandler: IRequestHandler<GetOptionRequest,List<SelectableOptionDto>>
    {
        private readonly IHrmRepository<Hrm.Domain.SelectableOption> _repository;
        private readonly IMapper _mapper;

        public GetOptionRequestHandler(IHrmRepository<Domain.SelectableOption> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<SelectableOptionDto>> Handle(GetOptionRequest request, CancellationToken cancellationToken)
        {
            var Options = await _repository.Where(x => true)
                .Include(x => x.FormField)
                .ToListAsync();

            var OptionDto = _mapper.Map<List<SelectableOptionDto>>(Options);

            return OptionDto;
        }
    }
}
