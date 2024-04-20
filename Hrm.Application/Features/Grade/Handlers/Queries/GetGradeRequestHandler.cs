using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Grade;
using Hrm.Application.DTOs.GradeType;
using Hrm.Application.Features.Grade.Requests.Queries;
using Hrm.Application.Features.GradeType.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Grade.Handlers.Queries
{
    public class GetGradeRequestHandler : IRequestHandler<GetGradeRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.Grade> _gradeRepository;
        private readonly IMapper _mapper;
        public GetGradeRequestHandler(IHrmRepository<Hrm.Domain.Grade> gradeRepository, IMapper mapper)
        {
            _gradeRepository = gradeRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetGradeRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.Grade> grades = _gradeRepository.Where(x => true);
            grades = grades.OrderByDescending(x => x.GradeId);
            var gradeDto = await Task.Run(() => _mapper.Map<List<GradeDto>>(grades));

            return gradeDto;
        }
    }
}