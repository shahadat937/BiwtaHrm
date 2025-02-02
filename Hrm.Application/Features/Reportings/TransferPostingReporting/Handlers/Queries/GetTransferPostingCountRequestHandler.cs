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
    public class GetTransferPostingCountRequestHandler : IRequestHandler<GetTransferPostingCountRequest, EmpTransferPostingCountDto>
    {

        private readonly IHrmRepository<EmpTransferPosting> _EmpTransferPostingRepository;

        public GetTransferPostingCountRequestHandler(IHrmRepository<EmpTransferPosting> EmpTransferPostingRepository)
        {
            _EmpTransferPostingRepository = EmpTransferPostingRepository;
        }

        public async Task<EmpTransferPostingCountDto> Handle(GetTransferPostingCountRequest request, CancellationToken cancellationToken)
        {

            IQueryable <EmpTransferPosting> query = _EmpTransferPostingRepository.FilterWithInclude(x=> true);
            if (request.DepartmentFrom != 0)
                query = query.Where(tp => tp.CurrentDepartmentId == request.DepartmentFrom);

            if (request.DepartmentTo != 0)
                query = query.Where(tp => tp.TransferDepartmentId == request.DepartmentTo);

            if (request.SectionFrom != 0)
                query = query.Where(tp => tp.CurrentSectionId == request.SectionFrom);

            if (request.SectionTo != 0)
                query = query.Where(tp => tp.TransferSectionId == request.SectionTo);
            if(request.DateFrom != null && request.DateTo != null && request.DateFrom != DateOnly.MinValue && request.DateTo != DateOnly.MinValue)
            {
               query = query.Where(tp => (tp.JoiningDate <= request.DateFrom && tp.JoiningDate >= request.DateTo) || 
                                         (tp.OfficeOrderDate <= request.DateFrom && tp.OfficeOrderDate >= request.DateTo) 
                                      
    ); 
            }
           

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
                JoingRejected = transferPostings.Count(tp => tp.JoiningStatus == false),
                WithoutPromotion = transferPostings.Count(tp => tp.WithPromotion == false),
                WithPromotion = transferPostings.Count(tp => tp.WithPromotion == true),

            };

        }
        }
    }
