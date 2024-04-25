using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Grade;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Grade.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Grade.Handlers.Queries
{
    public class GetGradeRequestHandler : IRequestHandler<GetGradeRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.Grade> _GradeRepository;
        private readonly IHrmRepository<Hrm.Domain.GradeClass> _GradeClassRepository;
        private readonly IHrmRepository<Hrm.Domain.GradeType> _GradeTypeRepository;
        private readonly IMapper _mapper;
        public GetGradeRequestHandler(IHrmRepository<Hrm.Domain.Grade> GradeRepository, IMapper mapper,IHrmRepository<Domain.GradeType> GradeTypeRepository, IHrmRepository<Domain.GradeClass> GradeClassRepository)
        {
            _GradeRepository = GradeRepository;
            _mapper = mapper;
            _GradeClassRepository = GradeClassRepository;
            _GradeTypeRepository = GradeTypeRepository;
        }

        public async Task<object> Handle(GetGradeRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.Grade> Grades = _GradeRepository.FilterWithInclude(x => true);
            Grades = Grades.OrderByDescending(x => x.GradeId);

            var GradeDtos = new List<GradeDto>();

            foreach (var Grade in Grades)
            {
                var GradeDto = _mapper.Map<GradeDto>(Grade);
                var GradeClassName = await GetGradeClassName(Grade.GradeClassId);
                var GradeTypeName = await GetGradeTypeName(Grade.GradeTypeId);
                GradeDto.GradeClassName= GradeClassName;
                GradeDto.GradeTypeName = GradeTypeName;
                GradeDtos.Add(GradeDto);
            }

            return GradeDtos;
        }

        private async Task<string?> GetGradeClassName(int? GradeClassId)
        {
            if (GradeClassId.HasValue)
            {
                var GradeClass = await _GradeClassRepository.Get(GradeClassId.Value);
                return GradeClass?.GradeClassName;
            }
            return null;
        }
        private async Task<string?> GetGradeTypeName(int? GradeTypeId)
        {
            if (GradeTypeId.HasValue)
            {
                var GradeType = await _GradeTypeRepository.Get(GradeTypeId.Value);
                return GradeType?.GradeTypeName;
            }
            return null;
        }
    }
}
