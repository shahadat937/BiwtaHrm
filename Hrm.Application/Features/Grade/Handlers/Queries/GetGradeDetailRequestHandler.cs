using AutoMapper;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hrm.Application.Features.Grades.Requests.Queries;
using Hrm.Application.DTOs.Grade;
using Hrm.Application.Contracts.Persistence;
using Hrm.Domain;

namespace Hrm.Application.Features.Grades.Handlers.Queries
{
    public class GetGradeDetailRequestHandler : IRequestHandler<GetGradeDetailRequest, GradeDto>
    {
        // private readonly IUTOfficerTypeRepository _UTOfficerTypeRepository;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Hrm.Domain.Grade> _GradeRepository;
        public GetGradeDetailRequestHandler(IHrmRepository<Hrm.Domain.Grade> GradeRepository, IMapper mapper)
        {
            _GradeRepository = GradeRepository;
            _mapper = mapper;
        }
        public async Task<GradeDto> Handle(GetGradeDetailRequest request, CancellationToken cancellationToken)
        {
            var Grade = await _GradeRepository.Get(request.GradeId);
            return _mapper.Map<GradeDto>(Grade);
        }
    }
}
