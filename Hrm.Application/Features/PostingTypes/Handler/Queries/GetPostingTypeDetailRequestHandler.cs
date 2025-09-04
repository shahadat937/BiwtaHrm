using AutoMapper;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hrm.Application.Features.PostingTypes.Request.Queries;
using Hrm.Application.DTOs.PostingType;
using Hrm.Application.Contracts.Persistence;
using Hrm.Domain;

namespace Hrm.Application.Features.PostingTypes.Handlers.Queries
{
    public class GetPostingTypeDetailRequestHandler : IRequestHandler<GetPostingTypeDetailRequest, PostingTypeDto>
    {
        // private readonly IUTOfficerTypeRepository _UTOfficerTypeRepository;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Hrm.Domain.PostingType> _PostingTypeRepository;
        public GetPostingTypeDetailRequestHandler(IHrmRepository<Hrm.Domain.PostingType> PostingTypeRepository, IMapper mapper)
        {
            _PostingTypeRepository = PostingTypeRepository;
            _mapper = mapper;
        }
        public async Task<PostingTypeDto> Handle(GetPostingTypeDetailRequest request, CancellationToken cancellationToken)
        {
            var PostingType = await _PostingTypeRepository.Get(request.PostingTypeId);
            return _mapper.Map<PostingTypeDto>(PostingType);
        }
    }
}
