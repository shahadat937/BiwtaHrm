using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Grade.Requests.Queries;
using Hrm.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Hrm.Application.DTOs.Grade;

namespace Hrm.Application.Features.Grade.Handlers.Queries
{
    public class GetGradeByEmpIdRequestHandler: IRequestHandler<GetGradeByEmpIdRequest,GradeDto>
    {
        private readonly IHrmRepository<Hrm.Domain.Grade> _GradeRepo;
        private readonly IHrmRepository<Hrm.Domain.EmpJobDetail> _EmpJobDetailRepo;
        private readonly IMapper _mapper;

        public GetGradeByEmpIdRequestHandler(IHrmRepository<Domain.Grade> gradeRepo, IHrmRepository<Hrm.Domain.EmpJobDetail> empJobDetailRepo, IMapper mapper)
        {
            _GradeRepo = gradeRepo;
            _EmpJobDetailRepo = empJobDetailRepo;
            _mapper = mapper;
        }

        public async Task<GradeDto> Handle(GetGradeByEmpIdRequest request, CancellationToken cancellationToken)
        {
            var gradeId = await _EmpJobDetailRepo.Where(x => x.EmpId == request.EmpId).Select(x => x.PresentGradeId).FirstOrDefaultAsync();

            if(gradeId == null)
            {
                throw new NotFoundException("Grade Information For Employee",request.EmpId);
            }

            var grade = await _GradeRepo.Where(x => x.GradeId == gradeId)
                .FirstOrDefaultAsync();

            var gradeDto = _mapper.Map<GradeDto>(grade);

            return gradeDto;
        }
    }
}
