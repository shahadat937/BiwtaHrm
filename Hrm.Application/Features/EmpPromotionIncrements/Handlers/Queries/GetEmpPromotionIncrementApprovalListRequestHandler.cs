using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpPromotionIncrement;
using Hrm.Application.Features.EmpPromotionIncrements.Requests.Queries;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpPromotionIncrements.Handlers.Queries
{
    public class GetEmpPromotionIncrementApprovalListRequestHandler : IRequestHandler<GetEmpPromotionIncrementApprovalListRequest, object>
    {

        private readonly IHrmRepository<EmpPromotionIncrement> _EmpPromotionIncrementRepository;
        private readonly IHrmRepository<EmpJobDetail> _EmpJobDetailRepository;
        private readonly IMapper _mapper;
        public GetEmpPromotionIncrementApprovalListRequestHandler(IHrmRepository<Hrm.Domain.EmpPromotionIncrement> EmpPromotionIncrementRepository, IMapper mapper, IHrmRepository<EmpJobDetail> empJobDetailRepository)
        {
            _EmpPromotionIncrementRepository = EmpPromotionIncrementRepository;
            _mapper = mapper;
            _EmpJobDetailRepository = empJobDetailRepository;
        }

        public async Task<object> Handle(GetEmpPromotionIncrementApprovalListRequest request, CancellationToken cancellationToken)
        {
            if (request.Id != 0)
            {
                var empJobDetail = await _EmpJobDetailRepository.FindOneAsync(x => x.EmpId == request.Id);

                IQueryable<EmpPromotionIncrement> EmpPromotionIncrements = _EmpPromotionIncrementRepository.Where(x => x.IsApproval == true && x.CurrentDepartmentId == empJobDetail.DepartmentId)
                .Include(x => x.EmpBasicInfo)
                .Include(x => x.ApplicationBy)
                .Include(x => x.OrderBy)
                .Include(x => x.ApproveBy)
                .Include(x => x.CurrentDepartment)
                .Include(x => x.CurrentSection)
                .Include(x => x.CurrentDesignation)
                    .ThenInclude(x => x.DesignationSetup)
                .Include(x => x.CurrentGrade)
                .Include(x => x.CurrentScale)
                .Include(x => x.UpdateDesignation)
                    .ThenInclude(x => x.DesignationSetup)
                .Include(x => x.UpdateGrade)
                .Include(x => x.UpdateScale);

                EmpPromotionIncrements = EmpPromotionIncrements.OrderBy(x => x.ApproveStatus);

                var EmpPromotionIncrementDtos = _mapper.Map<List<EmpPromotionIncrementDto>>(EmpPromotionIncrements);

                return EmpPromotionIncrementDtos;
            }
            else
            {
                IQueryable<EmpPromotionIncrement> EmpPromotionIncrements = _EmpPromotionIncrementRepository.Where(x => x.IsApproval == true)
                .Include(x => x.EmpBasicInfo)
                .Include(x => x.ApplicationBy)
                .Include(x => x.OrderBy)
                .Include(x => x.ApproveBy)
                .Include(x => x.CurrentDepartment)
                .Include(x => x.CurrentSection)
                .Include(x => x.CurrentDesignation)
                    .ThenInclude(x => x.DesignationSetup)
                .Include(x => x.CurrentGrade)
                .Include(x => x.CurrentScale)
                .Include(x => x.UpdateDesignation)
                    .ThenInclude(x => x.DesignationSetup)
                .Include(x => x.UpdateGrade)
                .Include(x => x.UpdateScale);

                EmpPromotionIncrements = EmpPromotionIncrements.OrderBy(x => x.ApproveStatus);

                var EmpPromotionIncrementDtos = _mapper.Map<List<EmpPromotionIncrementDto>>(EmpPromotionIncrements);

                return EmpPromotionIncrementDtos;
            }
            
        }
    }
}

