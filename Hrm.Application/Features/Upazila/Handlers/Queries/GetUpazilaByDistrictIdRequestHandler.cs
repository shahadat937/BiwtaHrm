﻿using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.District;
using Hrm.Application.DTOs.Upazila;
using Hrm.Application.Features.District.Requests.Queries;
using Hrm.Application.Features.Upazila.Requests.Queries;
using Hrm.Domain;
using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Upazila.Handlers.Queries
{
    public class GetUpazilaByDistrictIdRequestHandler:IRequestHandler<GetUpazilaByDistrictIdRequest,List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.Upazila> _UpazilaRepository;
        private readonly IMapper _mapper;
        public GetUpazilaByDistrictIdRequestHandler(IHrmRepository<Hrm.Domain.Upazila> UpazilaRepositoy, IMapper mapper)
        {
            _UpazilaRepository = UpazilaRepositoy;
            _mapper = mapper;
        }
        public async Task<List<SelectedModel>> Handle(GetUpazilaByDistrictIdRequest request, CancellationToken cancellationToken)
        {

            ICollection<Hrm.Domain.Upazila> Upazilas = _UpazilaRepository.FilterWithInclude(x => x.DistrictId == request.DistrictId).ToList();

            List<SelectedModel> SelectedModel = Upazilas.Select(x => new SelectedModel
            {
                Id = x.UpazilaId,
                Name = x.UpazilaName
            }).OrderBy(x => x.Name).ToList();
            //   var DivisionDtos = _mapper.Map<List<DivisionDto>>(DivisionNames);
            return SelectedModel;


        }
    }
}
