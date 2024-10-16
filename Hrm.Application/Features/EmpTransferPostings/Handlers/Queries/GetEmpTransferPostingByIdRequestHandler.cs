using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpTransferPosting;
using Hrm.Application.Features.EmpTransferPostings.Requests.Queries;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpTransferPostings.Handlers.Queries
{
    public class GetEmpTransferPostingByIdRequestHandler : IRequestHandler<GetEmpTransferPostingByIdRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.EmpTransferPosting> _EmpTransferPostingRepository;
        private readonly IMapper _mapper;
        public GetEmpTransferPostingByIdRequestHandler(IHrmRepository<Hrm.Domain.EmpTransferPosting> EmpTransferPostingRepository, IMapper mapper)
        {
            _EmpTransferPostingRepository = EmpTransferPostingRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetEmpTransferPostingByIdRequest request, CancellationToken cancellationToken)
        {
            var EmpTransferPostings = await _EmpTransferPostingRepository.Where(x=> x.Id == request.Id)
                .Include(x => x.EmpBasicInfo)
                .Include(x => x.ApplicationBy)
                .Include(x => x.OrderOfficeBy)
                .Include(x => x.CurrentGrade)
                .Include(x => x.UpdateGrade)
                .Include(x => x.CurrentScale)
                .Include(x => x.UpdateScale)
                .Include(x => x.TransferApproveBy)
                .Include(x => x.DeptReleaseBy)
                .Include(x => x.JoiningReportingBy)
                .Include(x => x.CurrentOffice)
                .Include(x => x.TransferOffice)
                .Include(x => x.CurrentDepartment)
                .Include(x => x.TransferDepartment)
                .Include(x => x.CurrentDesignation)
                    .ThenInclude(x => x.DesignationSetup)
                .Include(x => x.TransferDesignation)
                    .ThenInclude(x => x.DesignationSetup)
                .Include(x => x.CurrentSection)
                .Include(x => x.TransferSection)
                .Include(x => x.ReleaseType)
                .Include(x => x.DeptReleaseType)
                .FirstOrDefaultAsync(cancellationToken); ;

            if (EmpTransferPostings == null)
            {
                return null;
            }

            var EmpTransferPostingsDto = _mapper.Map<EmpTransferPostingDto>(EmpTransferPostings);

            return EmpTransferPostingsDto;
        }
    }
}

