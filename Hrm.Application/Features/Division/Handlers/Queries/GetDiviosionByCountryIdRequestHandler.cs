using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Division;
using Hrm.Application.Features.Division.Requests.Queries;
using Hrm.Domain;
using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Division.Handlers.Queries
{
    public class GetDiviosionByCountryIdRequestHandler : IRequestHandler<GetDivisionByCountryIdRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.Division> _DivisionRepository;
        private readonly IMapper _mapper;
        public GetDiviosionByCountryIdRequestHandler(IHrmRepository<Hrm.Domain.Division> DivisionRepositoy, IMapper mapper)
        {
            _DivisionRepository = DivisionRepositoy;
            _mapper = mapper;

        }
        public async Task<List<SelectedModel>> Handle(GetDivisionByCountryIdRequest request, CancellationToken cancellationToken)
        {
            //IQueryable<Hrm.Domain.Division> divisions = _DivisionRepository.Get(request.CountryId);
            //var divisionDtos = new List<DivisionDto>();
            //return divisionDtos;
            ICollection<Hrm.Domain.Division> Divisions = _DivisionRepository.FilterWithInclude(x => x.CountryId == request.CountryId).ToList();
            List<SelectedModel> DivisionNames = Divisions.Select(x => new SelectedModel
            {
                Id = x.DivisionId,
                Name = x.DivisionName
            }).ToList();
         //   var DivisionDtos = _mapper.Map<List<DivisionDto>>(DivisionNames);
            return DivisionNames;
        }
    }
}
