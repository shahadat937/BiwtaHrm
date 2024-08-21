using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.SelectableOption;
using Hrm.Application.Features.SelectableOption.Requests.Queries;
using Hrm.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SelectableOption.Handlers.Queries
{
    public class GetOptionByIdRequestHandler: IRequestHandler<GetOptionByIdRequest, SelectableOptionDto>
    {
        private readonly IHrmRepository<Hrm.Domain.SelectableOption> _repository;
        private readonly IMapper _mapper;

        public GetOptionByIdRequestHandler (IHrmRepository<Domain.SelectableOption> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SelectableOptionDto> Handle(GetOptionByIdRequest request, CancellationToken cancellationToken)
        {
            var Option = await _repository.Where(x => x.OptionId == request.OptionId)
                .Include(x => x.FormField)
                .FirstOrDefaultAsync();

            if(Option == null)
            {
                throw new NotFoundException(nameof(Option),request.OptionId);
            }

            var OptionDto = _mapper.Map<SelectableOptionDto>(Option);

            return OptionDto;
        }
    }
}
