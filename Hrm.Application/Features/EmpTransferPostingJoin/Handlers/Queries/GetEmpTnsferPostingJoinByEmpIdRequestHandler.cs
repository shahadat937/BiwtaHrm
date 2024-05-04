//using AutoMapper;
//using Hrm.Application.Contracts.Persistence;
//using Hrm.Application.DTOs.EmpTnsferPostingJoin;
//using Hrm.Application.Features.EmpTnsferPostingJoin.Requests.Queries;
//using Hrm.Domain;
//using Hrm.Shared.Models;
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Hrm.Application.Features.EmpTnsferPostingJoin.Handlers.Queries
//{
//    public class GetDiviosionByCountryIdRequestHandler : IRequestHandler<GetEmpTnsferPostingJoinByCountryIdRequest, List<SelectedModel>>
//    {
//        private readonly IHrmRepository<Hrm.Domain.EmpTnsferPostingJoin> _EmpTnsferPostingJoinRepository;
//        private readonly IMapper _mapper;
//        public GetDiviosionByCountryIdRequestHandler(IHrmRepository<Hrm.Domain.EmpTnsferPostingJoin> EmpTnsferPostingJoinRepositoy, IMapper mapper)
//        {
//            _EmpTnsferPostingJoinRepository = EmpTnsferPostingJoinRepositoy;
//            _mapper = mapper;

//        }
//        public async Task<List<SelectedModel>> Handle(GetEmpTnsferPostingJoinByCountryIdRequest request, CancellationToken cancellationToken)
//        {
//            //IQueryable<Hrm.Domain.EmpTnsferPostingJoin> EmpTnsferPostingJoins = _EmpTnsferPostingJoinRepository.Get(request.CountryId);
//            //var EmpTnsferPostingJoinDtos = new List<EmpTnsferPostingJoinDto>();
//            //return EmpTnsferPostingJoinDtos;
//            ICollection<Hrm.Domain.EmpTnsferPostingJoin> EmpTnsferPostingJoins = _EmpTnsferPostingJoinRepository.FilterWithInclude(x => x.CountryId == request.CountryId).ToList();
//            List<SelectedModel> EmpTnsferPostingJoinNames = EmpTnsferPostingJoins.Select(x => new SelectedModel
//            {
//                Id = x.EmpTnsferPostingJoinId,
//                Name = x.EmpTnsferPostingJoinName
//            }).ToList();
//         //   var EmpTnsferPostingJoinDtos = _mapper.Map<List<EmpTnsferPostingJoinDto>>(EmpTnsferPostingJoinNames);
//            return EmpTnsferPostingJoinNames;
//        }
//    }
//}



