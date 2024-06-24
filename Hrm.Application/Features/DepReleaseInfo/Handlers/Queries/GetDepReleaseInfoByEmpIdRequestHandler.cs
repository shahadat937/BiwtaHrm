//using AutoMapper;
//using Hrm.Application.Contracts.Persistence;
//using Hrm.Application.DTOs.DepReleaseInfo;
//using Hrm.Application.Features.DepReleaseInfo.Requests.Queries;
//using Hrm.Domain;
//using Hrm.Shared.Models;
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Hrm.Application.Features.DepReleaseInfo.Handlers.Queries
//{
//    public class GetDiviosionByCountryIdRequestHandler : IRequestHandler<GetDepReleaseInfoByCountryIdRequest, List<SelectedModel>>
//    {
//        private readonly IHrmRepository<Hrm.Domain.DepReleaseInfo> _DepReleaseInfoRepository;
//        private readonly IMapper _mapper;
//        public GetDiviosionByCountryIdRequestHandler(IHrmRepository<Hrm.Domain.DepReleaseInfo> DepReleaseInfoRepositoy, IMapper mapper)
//        {
//            _DepReleaseInfoRepository = DepReleaseInfoRepositoy;
//            _mapper = mapper;

//        }
//        public async Task<List<SelectedModel>> Handle(GetDepReleaseInfoByCountryIdRequest request, CancellationToken cancellationToken)
//        {
//            //IQueryable<Hrm.Domain.DepReleaseInfo> DepReleaseInfos = _DepReleaseInfoRepository.Get(request.CountryId);
//            //var DepReleaseInfoDtos = new List<DepReleaseInfoDto>();
//            //return DepReleaseInfoDtos;
//            ICollection<Hrm.Domain.DepReleaseInfo> DepReleaseInfos = _DepReleaseInfoRepository.FilterWithInclude(x => x.CountryId == request.CountryId).ToList();
//            List<SelectedModel> DepReleaseInfoNames = DepReleaseInfos.Select(x => new SelectedModel
//            {
//                Id = x.DepReleaseInfoId,
//                Name = x.DepReleaseInfoName
//            }).ToList();
//         //   var DepReleaseInfoDtos = _mapper.Map<List<DepReleaseInfoDto>>(DepReleaseInfoNames);
//            return DepReleaseInfoNames;
//        }
//    }
//}
