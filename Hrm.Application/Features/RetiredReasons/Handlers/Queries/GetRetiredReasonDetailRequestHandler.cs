using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.RetiredReason;
using Hrm.Application.Features.RetiredReasons.Requests.Queries;
using Hrm.Domain;
using MediatR;


namespace Hrm.Application.Features.RetiredReasons.Handlers.Queries
{
    public class GetRetiredReasonsDetailRequestHandler : IRequestHandler<GetRetiredReasonDetailRequest, RetiredReasonDto>
    {
        private readonly IMapper _mapper;
        private readonly IHrmRepository<RetiredReason> _RetiredReasonRepository;
        public GetRetiredReasonsDetailRequestHandler(IHrmRepository<RetiredReason> RetiredReasonRepository, IMapper mapper)
        {
            _RetiredReasonRepository = RetiredReasonRepository;
            _mapper = mapper;
        }
        public async Task<RetiredReasonDto> Handle(GetRetiredReasonDetailRequest request, CancellationToken cancellationToken)
        {
            var RetiredReason = await _RetiredReasonRepository.Get(request.Id);
            return _mapper.Map<RetiredReasonDto>(RetiredReason);
        }
    }
}
