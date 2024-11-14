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
    public class GetEmpPromotionIncrementByEmpIdRequestHandler : IRequestHandler<GetEmpPromotionIncrementByEmpIdRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.EmpPromotionIncrement> _EmpPromotionIncrementRepository;
        private readonly IMapper _mapper;
        public GetEmpPromotionIncrementByEmpIdRequestHandler(IHrmRepository<Hrm.Domain.EmpPromotionIncrement> EmpPromotionIncrementRepository, IMapper mapper)
        {
            _EmpPromotionIncrementRepository = EmpPromotionIncrementRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetEmpPromotionIncrementByEmpIdRequest request, CancellationToken cancellationToken)
        {
            var EmpPromotionIncrements = await _EmpPromotionIncrementRepository.Where(x => x.EmpId == request.Id && x.ApplicationStatus == null)
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
                .Include(x => x.UpdateScale)
                .FirstOrDefaultAsync(cancellationToken);


            var EmpPromotionIncrementDtos = _mapper.Map<EmpPromotionIncrementDto>(EmpPromotionIncrements);

            return EmpPromotionIncrementDtos;
        }
    }
}
