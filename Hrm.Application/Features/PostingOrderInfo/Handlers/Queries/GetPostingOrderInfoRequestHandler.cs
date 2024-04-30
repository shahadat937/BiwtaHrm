//using AutoMapper;
//using Hrm.Application.Contracts.Persistence;
//using Hrm.Application.DTOs.PostingOrderInfo;
//using Hrm.Application.DTOs.MaritalStatus;
//using Hrm.Application.Features.PostingOrderInfo.Requests.Queries;
//using Hrm.Application.Features.MaritalStatus.Requests.Queries;
//using Hrm.Domain;
//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Hrm.Application.Features.PostingOrderInfo.Handlers.Queries
//{
//    public class GetPostingOrderInfoRequestHandler : IRequestHandler<GetPostingOrderInfoRequest, object>
//    {

//        private readonly IHrmRepository<Hrm.Domain.PostingOrderInfo> _PostingOrderInfoRepository;
//        private readonly IHrmRepository<Hrm.Domain.Country> _countryRepository;
//        private readonly IMapper _mapper;
//        public GetPostingOrderInfoRequestHandler(IHrmRepository<Hrm.Domain.PostingOrderInfo> PostingOrderInfoRepository, IMapper mapper, IHrmRepository<Domain.Country> countryRepository)
//        {
//            _PostingOrderInfoRepository = PostingOrderInfoRepository;
//            _mapper = mapper;
//            _countryRepository = countryRepository;
//        }

//        public async Task<object> Handle(GetPostingOrderInfoRequest request, CancellationToken cancellationToken)
//        {
//            IQueryable<Hrm.Domain.PostingOrderInfo> PostingOrderInfos = _PostingOrderInfoRepository.FilterWithInclude(x => true);
//            PostingOrderInfos = PostingOrderInfos.OrderByDescending(x => x.PostingOrderInfoId);

//            var PostingOrderInfoDtos = new List<PostingOrderInfoDto>();

//            foreach (var PostingOrderInfo in PostingOrderInfos)
//            {
//                var PostingOrderInfoDto = _mapper.Map<PostingOrderInfoDto>(PostingOrderInfo);
//                var countryName = await GetCountryName(PostingOrderInfo.CountryId);
//                PostingOrderInfoDto.CountryName = countryName;
//                PostingOrderInfoDtos.Add(PostingOrderInfoDto);
//            }

//            return PostingOrderInfoDtos;
//        }

//        private async Task<string?> GetCountryName(int? countryId)
//        {
//            if (countryId.HasValue)
//            {
//                var country = await _countryRepository.Get(countryId.Value);
//                return country?.CountryName;
//            }
//            return null;
//        }
//    }
//}
