using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Scale;
using Hrm.Application.DTOs.ScaleGradeView;
using Hrm.Application.Features.ScaleGradeView.Requests.Queries;
using Hrm.Application.Features.Scales.Requests.Queries;
using Hrm.Application.Features.Scales.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.ScaleGradeView.Handlers.Queries
{
    public class GetScaleGradeViewCommandHandler : IRequestHandler<GetScaleGradeViewRequest, object>
    {
        private readonly IHrmRepository<Hrm.Domain.ScaleGradeView> _scaleRepository;
        private readonly IMapper _mapper;
        public GetScaleGradeViewCommandHandler(IHrmRepository<Hrm.Domain.ScaleGradeView> scaleRepository, IMapper mapper)
        {
            _scaleRepository = scaleRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetScaleGradeViewRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.ScaleGradeView> ScaleGradeView = _scaleRepository.Where(x => true);
            
            var ScaleGradeViewDtos = _mapper.Map<List<ScaleGradeViewDto>>(ScaleGradeView);

            return ScaleGradeViewDtos;
        }
    }

}
