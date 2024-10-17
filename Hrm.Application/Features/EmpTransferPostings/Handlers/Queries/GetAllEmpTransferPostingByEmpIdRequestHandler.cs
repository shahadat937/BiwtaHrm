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
    public class GetAllEmpTransferPostingByEmpIdRequestHandler : IRequestHandler<GetAllEmpTransferPostingByEmpIdRequest, object>
    {

        private readonly IHrmRepository<EmpTransferPosting> _EmpTransferPostingRepository;
        private readonly IMapper _mapper;
        public GetAllEmpTransferPostingByEmpIdRequestHandler(IHrmRepository<Hrm.Domain.EmpTransferPosting> EmpTransferPostingRepository, IMapper mapper)
        {
            _EmpTransferPostingRepository = EmpTransferPostingRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetAllEmpTransferPostingByEmpIdRequest request, CancellationToken cancellationToken)
        {
            IQueryable<EmpTransferPosting> EmpTransferPostings = _EmpTransferPostingRepository.Where(x => x.EmpId == request.Id && x.ApplicationStatus == true)
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
                .Include(x => x.DeptReleaseType);

            EmpTransferPostings = EmpTransferPostings.OrderByDescending(x => x.Id);

            var EmpTransferPostingDtos = _mapper.Map<List<EmpTransferPostingDto>>(EmpTransferPostings);

            return EmpTransferPostingDtos;
        }
    }
}
