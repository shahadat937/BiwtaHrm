using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Scale;
using Hrm.Application.DTOs.Grade_cls_type_Vw;
using Hrm.Application.Features.Grade_cls_type_Vw.Requests.Queries;
using Hrm.Application.Features.Scales.Requests.Queries;
using Hrm.Application.Features.Scales.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Grade_cls_type_Vw.Handlers.Queries
{
    public class GetGrade_cls_type_VwCommandHandler : IRequestHandler<GetGrade_cls_type_VwRequest, object>
    {
        private readonly IHrmRepository<Hrm.Domain.Grade_cls_type_Vw> _scaleRepository;
        private readonly IMapper _mapper;
        public GetGrade_cls_type_VwCommandHandler(IHrmRepository<Hrm.Domain.Grade_cls_type_Vw> scaleRepository, IMapper mapper)
        {
            _scaleRepository = scaleRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetGrade_cls_type_VwRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.Grade_cls_type_Vw> Grade_cls_type_Vw = _scaleRepository.Where(x => true);

            var Grade_cls_type_VwDtos = _mapper.Map<List<Grade_cls_type_VwDto>>(Grade_cls_type_Vw);

            return Grade_cls_type_VwDtos;
        }
    }

}
