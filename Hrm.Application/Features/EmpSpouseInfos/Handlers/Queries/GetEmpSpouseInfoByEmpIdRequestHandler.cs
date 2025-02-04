﻿using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpSpouseInfos.Requests.Queries;
using Hrm.Application.Features.EmpJobDetails.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.DTOs.EmpSpouseInfo;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.EmpSpouseInfos.Handlers.Queries
{
    public class GetEmpSpouseInfoByEmpIdRequestHandler : IRequestHandler<GetEmpSpouseInfoByEmpIdRequest, List<EmpSpouseInfoDto>>
    {

        private readonly IHrmRepository<EmpSpouseInfo> _EmpSpouseInfoRepository;
        private readonly IMapper _mapper;
        public GetEmpSpouseInfoByEmpIdRequestHandler(IHrmRepository<EmpSpouseInfo> EmpSpouseInfoRepository, IMapper mapper)
        {
            _EmpSpouseInfoRepository = EmpSpouseInfoRepository;
            _mapper = mapper;
        }

        public async Task<List<EmpSpouseInfoDto>> Handle(GetEmpSpouseInfoByEmpIdRequest request, CancellationToken cancellationToken)
        {
            List<EmpSpouseInfo> empSpouseInfos = await _EmpSpouseInfoRepository.Where(x => x.EmpId == request.Id).Include(x => x.Occupation).ToListAsync(cancellationToken);

            if (empSpouseInfos == null)
            {
                return null;
            }

            List<EmpSpouseInfoDto> result = _mapper.Map<List<EmpSpouseInfoDto>>(empSpouseInfos);

            return result;
        }
    }
}