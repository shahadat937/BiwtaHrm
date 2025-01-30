using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Organograms;
using Hrm.Application.DTOs.Reporting;
using Hrm.Application.Features.Reportings.TransferPosting.Requests.Queries;
using Hrm.Application.Models;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Reportings.TransferPosting.Handlers.Queries
{
    public class GetTransferPostingReportingRequestHandler : IRequestHandler<GetTransferPostingReportingRequest, EmpTransferPostingCountDto>
    {

        private readonly IHrmRepository<EmpTransferPosting> _EmpTransferPostingRepository;

        public GetTransferPostingReportingRequestHandler(IHrmRepository<EmpTransferPosting> EmpTransferPostingRepository)
        {
            _EmpTransferPostingRepository = EmpTransferPostingRepository;
        }

        public async Task<EmpTransferPostingCountDto> Handle(GetTransferPostingReportingRequest request, CancellationToken cancellationToken)
        {

            //var query = await _EmpTransferPostingRepository.GetAll();
            IQueryable <EmpTransferPosting> query = _EmpTransferPostingRepository.FilterWithInclude(x=> true);
            if (request.DepartmentFrom != 0)
                query = query.Where(tp => tp.CurrentDepartmentId == request.DepartmentFrom);

            if (request.DepartmentTo != 0)
                query = query.Where(tp => tp.TransferDepartmentId == request.DepartmentTo);

            if (request.SectionFrom != 0)
                query = query.Where(tp => tp.CurrentSectionId == request.SectionFrom);

            if (request.SectionTo != 0)
                query = query.Where(tp => tp.TransferSectionId == request.SectionTo);

            var transferPostings = await query.ToListAsync(); // Single DB call

            return new EmpTransferPostingCountDto
            {
                TotalApplication = transferPostings.Count,
                TotalApproved = transferPostings.Count(tp => tp.ApplicationStatus == true),
                TotalDepartmentPending = transferPostings.Count(tp => tp.IsDepartmentApprove == null),
                TotalDepartmentApprove = transferPostings.Count(tp => tp.IsDepartmentApprove == true),
                TotalDepartmentReject = transferPostings.Count(tp => tp.IsDepartmentApprove == false),
                JoingingPending = transferPostings.Count(tp => tp.JoiningStatus == null),
                JoingingApproved = transferPostings.Count(tp => tp.JoiningStatus == true),
                JoingRejected = transferPostings.Count(tp => tp.JoiningStatus == false)
            };

        }
        }
    }
