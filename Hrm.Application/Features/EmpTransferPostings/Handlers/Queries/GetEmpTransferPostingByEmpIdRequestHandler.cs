using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpTransferPosting;
using Hrm.Application.Features.EmpTransferPostings.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpTransferPostings.Handlers.Queries
{
    public class GetEmpTransferPostingByEmpIdRequestHandler : IRequestHandler<GetEmpTransferPostingByEmpIdRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.EmpTransferPosting> _EmpTransferPostingRepository;
        private readonly IMapper _mapper;
        public GetEmpTransferPostingByEmpIdRequestHandler(IHrmRepository<Hrm.Domain.EmpTransferPosting> EmpTransferPostingRepository, IMapper mapper)
        {
            _EmpTransferPostingRepository = EmpTransferPostingRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetEmpTransferPostingByEmpIdRequest request, CancellationToken cancellationToken)
        {
            var EmpTransferPostings = await _EmpTransferPostingRepository.Where(x => x.EmpId == request.Id && x.ApplicationStatus == null)
                .Include(x => x.EmpBasicInfo)
                .Include(x => x.ApplicationBy)
                .Include(x => x.OrderBy)
                .Include(x => x.TransferApproveBy)
                .Include(x => x.DeptReleaseBy)
                .Include(x => x.JoiningReportingBy)
                .Include(x => x.CurrentOffice)
                .Include(x => x.TransferOffice)
                .Include(x => x.CurrentDepartment)
                .Include(x => x.TransferDepartment)
                .Include(x => x.CurrentDesignation)
                .Include(x => x.TransferDesignation)
                .Include(x => x.CurrentSection)
                .Include(x => x.TransferSection)
                .Include(x => x.ReleaseType)
                .Include(x => x.DeptReleaseType)
                .FirstOrDefaultAsync(cancellationToken);

            if (EmpTransferPostings == null)
            {
                return null;
            }

            var EmpTransferPostingsDto = _mapper.Map<EmpTransferPostingDto>(EmpTransferPostings);

            return EmpTransferPostingsDto;
        }
    }
}
