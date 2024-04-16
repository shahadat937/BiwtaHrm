using AutoMapper;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hrm.Application.Features.GradeTypes.Requests.Queries;
using Hrm.Application.DTOs.GradeType;
using Hrm.Application.Contracts.Persistence;
using Hrm.Domain;

namespace Hrm.Application.Features.GradeTypes.Handlers.Queries
{
    public class GetGradeTypeDetailRequestHandler : IRequestHandler<GetGradeTypeDetailRequest, GradeTypeDto>
    {
        // private readonly IUTOfficerTypeRepository _UTOfficerTypeRepository;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Hrm.Domain.GradeType> _GradeTypeRepository;
        public GetGradeTypeDetailRequestHandler(IHrmRepository<Hrm.Domain.GradeType> GradeTypeRepository, IMapper mapper)
        {
            _GradeTypeRepository = GradeTypeRepository;
            _mapper = mapper;
        }
        public async Task<GradeTypeDto> Handle(GetGradeTypeDetailRequest request, CancellationToken cancellationToken)
        {
            var GradeType = await _GradeTypeRepository.Get(request.GradeTypeId);
            return _mapper.Map<GradeTypeDto>(GradeType);
        }
    }
}
