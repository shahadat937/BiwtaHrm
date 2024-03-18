using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.GradeClass;
using Hrm.Application.DTOs.GradeType;
using Hrm.Application.Features.GradeClass.Requests.Queries;
using Hrm.Application.Features.GradeType.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.GradeClass.Handlers.Queries
{
    public class GetGradeClassRequestHandler : IRequestHandler<GetGradeClassRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.GradeClass> _gradeClassRepository;
        private readonly IMapper _mapper;
        public GetGradeClassRequestHandler(IHrmRepository<Hrm.Domain.GradeClass> gradeClassRepository, IMapper mapper)
        {
            _gradeClassRepository = gradeClassRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetGradeClassRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.GradeClass> gradeClasses = _gradeClassRepository.Where(x => true);
            var gradeClassDto = await Task.Run(() => _mapper.Map<List<GradeClassDto>>(gradeClasses));

            return gradeClassDto;
        }
    }
}