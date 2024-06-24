using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.PostingOrderInfo;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.PostingOrderInfo.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.PostingOrderInfo.Handlers.Queries
{
    public class GetPostingOrderInfoByIdRequestHandler : IRequestHandler<GetPostingOrderInfoByIdRequest, PostingOrderInfoDto>
    {

        private readonly IHrmRepository<Hrm.Domain.PostingOrderInfo> _PostingOrderInfoRepository;
        private readonly IMapper _mapper;
        public GetPostingOrderInfoByIdRequestHandler(IHrmRepository<Hrm.Domain.PostingOrderInfo> PostingOrderInfoRepositoy, IMapper mapper)
        {
            _PostingOrderInfoRepository = PostingOrderInfoRepositoy;
            _mapper = mapper;
        }

        public async Task<PostingOrderInfoDto> Handle(GetPostingOrderInfoByIdRequest request, CancellationToken cancellationToken)
        {
            var PostingOrderInfo = await _PostingOrderInfoRepository.Get(request.PostingOrderInfoId);
            return _mapper.Map<PostingOrderInfoDto>(PostingOrderInfo);
        }
    }
}
