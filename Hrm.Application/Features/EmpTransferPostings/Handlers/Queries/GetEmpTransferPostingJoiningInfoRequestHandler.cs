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
    public class GetEmpTransferPostingJoiningInfoRequestHandler : IRequestHandler<GetEmpTransferPostingJoiningInfoRequest, object>
    {

        private readonly IHrmRepository<EmpTransferPosting> _EmpTransferPostingRepository;
        private readonly IHrmRepository<EmpJobDetail> _EmpJobDetailRepository;
        private readonly IMapper _mapper;
        public GetEmpTransferPostingJoiningInfoRequestHandler(IHrmRepository<Hrm.Domain.EmpTransferPosting> EmpTransferPostingRepository, IMapper mapper, IHrmRepository<EmpJobDetail> empJobDetailRepository)
        {
            _EmpTransferPostingRepository = EmpTransferPostingRepository;
            _mapper = mapper;
            _EmpJobDetailRepository = empJobDetailRepository;
        }

        public async Task<object> Handle(GetEmpTransferPostingJoiningInfoRequest request, CancellationToken cancellationToken)
        {
            if (request.Id != 0)
            {
                var empJobDetail = await _EmpJobDetailRepository.FindOneAsync(x => x.EmpId == request.Id);

                IQueryable<EmpTransferPosting> EmpTransferPostings = _EmpTransferPostingRepository.Where(x => (x.DeptApproveStatus == true || x.IsDepartmentApprove == false) && (x.TransferApproveStatus == true || x.IsTransferApprove == false) && x.IsJoining == true && x.TransferDepartmentId == empJobDetail.DepartmentId)
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

                EmpTransferPostings = EmpTransferPostings.OrderBy(x => x.JoiningStatus).ThenByDescending(x => x.DateCreated);

                var EmpTransferPostingDtos = _mapper.Map<List<EmpTransferPostingDto>>(EmpTransferPostings);

                return EmpTransferPostingDtos;
            }
            else
            {
                IQueryable<EmpTransferPosting> EmpTransferPostings = _EmpTransferPostingRepository.Where(x => (x.DeptApproveStatus == true || x.IsDepartmentApprove == false) && (x.TransferApproveStatus == true || x.IsTransferApprove == false) && x.IsJoining == true)
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

                EmpTransferPostings = EmpTransferPostings.OrderBy(x => x.JoiningStatus).ThenByDescending(x => x.DateCreated);

                var EmpTransferPostingDtos = _mapper.Map<List<EmpTransferPostingDto>>(EmpTransferPostings);

                return EmpTransferPostingDtos;
            }
        }
    }
}