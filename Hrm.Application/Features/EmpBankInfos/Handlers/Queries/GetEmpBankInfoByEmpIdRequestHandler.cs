using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpBankInfos.Requests.Queries;
using Hrm.Application.Features.EmpJobDetails.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.DTOs.EmpBankInfo;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.EmpBankInfos.Handlers.Queries
{
    public class GetEmpBankInfoByEmpIdRequestHandler : IRequestHandler<GetEmpBankInfoByEmpIdRequest, List<EmpBankInfoDto>>
    {

        private readonly IHrmRepository<EmpBankInfo> _EmpBankInfoRepository;
        private readonly IMapper _mapper;
        public GetEmpBankInfoByEmpIdRequestHandler(IHrmRepository<EmpBankInfo> EmpBankInfoRepository, IMapper mapper)
        {
            _EmpBankInfoRepository = EmpBankInfoRepository;
            _mapper = mapper;
        }

        public async Task<List<EmpBankInfoDto>> Handle(GetEmpBankInfoByEmpIdRequest request, CancellationToken cancellationToken)
        {
            List<EmpBankInfo> empBankInfos = await _EmpBankInfoRepository.Where(x => x.EmpId == request.Id)
                .Include(x => x.AccountType)
                .Include(x => x.Bank)
                .Include(x => x.BankBranch)
                .ToListAsync(cancellationToken);

            if (empBankInfos == null)
            {
                return null;
            }

            List<EmpBankInfoDto> result = _mapper.Map<List<EmpBankInfoDto>>(empBankInfos);

            return result;
        }
    }
}