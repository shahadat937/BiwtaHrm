using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Form;
using Hrm.Application.Features.Form.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Form.Handlers.Queries
{
    public class GetFormRequestHandler: IRequestHandler<GetFormRequest, List<FormDto>>
    {
        private readonly IHrmRepository<Domain.Form> _FormRepository;
        private readonly IMapper _mapper;

        public GetFormRequestHandler(IHrmRepository<Domain.Form> formRepository, IMapper mapper)
        {
            _FormRepository = formRepository;
            _mapper = mapper;
        }


        public async Task<List<FormDto>> Handle(GetFormRequest request, CancellationToken cancellationToken)
        {
            var forms = await _FormRepository.GetAll();
            var formList = _mapper.Map<List<FormDto>>(forms);

            return formList;
        }
    }
}
