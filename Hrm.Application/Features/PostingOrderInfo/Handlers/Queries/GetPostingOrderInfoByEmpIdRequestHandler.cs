//using AutoMapper;
//using Hrm.Application.Contracts.Persistence;
//using Hrm.Application.DTOs.PostingOrderInfo;
//using Hrm.Application.Features.PostingOrderInfo.Requests.Queries;
//using Hrm.Domain;
//using Hrm.Shared.Models;
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Hrm.Application.Features.PostingOrderInfo.Handlers.Queries
//{
//    public class GetDiviosionByCountryIdRequestHandler : IRequestHandler<GetPostingOrderInfoByCountryIdRequest, List<SelectedModel>>
//    {
//        private readonly IHrmRepository<Hrm.Domain.PostingOrderInfo> _PostingOrderInfoRepository;
//        private readonly IMapper _mapper;
//        public GetDiviosionByCountryIdRequestHandler(IHrmRepository<Hrm.Domain.PostingOrderInfo> PostingOrderInfoRepositoy, IMapper mapper)
//        {
//            _PostingOrderInfoRepository = PostingOrderInfoRepositoy;
//            _mapper = mapper;

//        }
//        public async Task<List<SelectedModel>> Handle(GetPostingOrderInfoByCountryIdRequest request, CancellationToken cancellationToken)
//        {
//            //IQueryable<Hrm.Domain.PostingOrderInfo> PostingOrderInfos = _PostingOrderInfoRepository.Get(request.CountryId);
//            //var PostingOrderInfoDtos = new List<PostingOrderInfoDto>();
//            //return PostingOrderInfoDtos;
//            ICollection<Hrm.Domain.PostingOrderInfo> PostingOrderInfos = _PostingOrderInfoRepository.FilterWithInclude(x => x.CountryId == request.CountryId).ToList();
//            List<SelectedModel> PostingOrderInfoNames = PostingOrderInfos.Select(x => new SelectedModel
//            {
//                Id = x.PostingOrderInfoId,
//                Name = x.PostingOrderInfoName
//            }).ToList();
//         //   var PostingOrderInfoDtos = _mapper.Map<List<PostingOrderInfoDto>>(PostingOrderInfoNames);
//            return PostingOrderInfoNames;
//        }
//    }
//}
