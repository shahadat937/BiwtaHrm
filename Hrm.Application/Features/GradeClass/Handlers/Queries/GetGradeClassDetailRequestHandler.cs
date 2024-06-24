using AutoMapper;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hrm.Application.Features.GradeClasss.Requests.Queries;
using Hrm.Application.DTOs.GradeClass;
using Hrm.Application.Contracts.Persistence;
using Hrm.Domain;

namespace Hrm.Application.Features.GradeClasss.Handlers.Queries
{
    public class GetGradeClassDetailRequestHandler : IRequestHandler<GetGradeClassDetailRequest, GradeClassDto>
    {
        // private readonly IUTOfficerTypeRepository _UTOfficerTypeRepository;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Hrm.Domain.GradeClass> _GradeClassRepository;
        public GetGradeClassDetailRequestHandler(IHrmRepository<Hrm.Domain.GradeClass> GradeClassRepository, IMapper mapper)
        {
            _GradeClassRepository = GradeClassRepository;
            _mapper = mapper;
        }
        public async Task<GradeClassDto> Handle(GetGradeClassDetailRequest request, CancellationToken cancellationToken)
        {
            var GradeClass = await _GradeClassRepository.Get(request.GradeClassId);
            return _mapper.Map<GradeClassDto>(GradeClass);
        }
    }
}
