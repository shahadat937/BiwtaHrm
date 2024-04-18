using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.GradeType;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.GradeType.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.GradeType.Handlers.Queries
{
    public class GetGradeTypeReuqestHandler : IRequestHandler<GetGradeTypeReuqest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.GradeType> _gradeTypeRepository;
        private readonly IMapper _mapper;
        public GetGradeTypeReuqestHandler(IHrmRepository<Hrm.Domain.GradeType> gradeTypeRepository, IMapper mapper)
        {
            _gradeTypeRepository = gradeTypeRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetGradeTypeReuqest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.GradeType> gradeTypes = _gradeTypeRepository.Where(x => true);
            gradeTypes = gradeTypes.OrderByDescending(x => x.GradeTypeId);
            var gradeTypeDto = await Task.Run(() => _mapper.Map<List<GradeTypeDto>>(gradeTypes));

            return gradeTypeDto;
        }
    }
}
