using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpTransferPostings.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpTransferPostings.Handlers.Queries
{
    public class GetCurrentDeptJoinDateByEmpIdRequestHandler : IRequestHandler<GetCurrentDeptJoinDateByEmpIdRequest, object>
    {
        private readonly IHrmRepository<EmpTransferPosting> _EmpEmpTransferPostingRepository;
        private readonly IHrmRepository<EmpWorkHistory> _EmpWorkHistoryRepository;
        private readonly IHrmRepository<EmpJobDetail> _EmpJobDetailRepository;
        private readonly IHrmRepository<EmpPromotionIncrement> _EmpPromotionIncrementRepository;
        private readonly IMapper _mapper;
        public GetCurrentDeptJoinDateByEmpIdRequestHandler(IHrmRepository<EmpTransferPosting> EmpEmpTransferPostingRepository, IMapper mapper, IHrmRepository<EmpWorkHistory> empWorkHistoryRepository, IHrmRepository<EmpJobDetail> empJobDetailRepository, IHrmRepository<EmpPromotionIncrement> empPromotionIncrementRepository)
        {
            _EmpEmpTransferPostingRepository = EmpEmpTransferPostingRepository;
            _mapper = mapper;
            _EmpWorkHistoryRepository = empWorkHistoryRepository;
            _EmpJobDetailRepository = empJobDetailRepository;
            _EmpPromotionIncrementRepository = empPromotionIncrementRepository;
        }

        public async Task<object> Handle(GetCurrentDeptJoinDateByEmpIdRequest request, CancellationToken cancellationToken)
        {
            var transferPostings = _EmpEmpTransferPostingRepository
                .Where(tp => tp.EmpId == request.EmpId && tp.ApplicationStatus == true);

            var workHistories = _EmpWorkHistoryRepository
                .Where(wh => wh.EmpId == request.EmpId);

            var jobDetails = _EmpJobDetailRepository
                .Where(jd => jd.EmpId == request.EmpId);

            var promotionIncrements = _EmpPromotionIncrementRepository
                .Where(pi => pi.EmpId == request.EmpId && pi.UpdateDesignationId != null);

            var latestTransferPostingJoiningDate = transferPostings
                .OrderByDescending(tp => tp.JoiningDate)
                .Select(tp => tp.JoiningDate)
                .FirstOrDefault();

            var latestWorkHistoryReleaseDate = workHistories
                .OrderByDescending(wh => wh.ReleaseDate)
                .Select(wh => wh.ReleaseDate)
                .FirstOrDefault();

            var extendedlatestWorkHistoryReleaseDate = latestWorkHistoryReleaseDate?.AddDays(1);

            var latestJobDetailJoiningDate = jobDetails
                .OrderByDescending(jd => jd.JoiningDate)
                .Select(jd => jd.JoiningDate)
                .FirstOrDefault();

            var latestPromotionIncrementsDate = promotionIncrements
                .OrderByDescending(wh => wh.EffectiveDate)
                .Select(wh => wh.EffectiveDate)
                .FirstOrDefault();

            var latestDate = new[] { latestTransferPostingJoiningDate, extendedlatestWorkHistoryReleaseDate, latestJobDetailJoiningDate, latestPromotionIncrementsDate }
                .Where(d => d != null).Max();


            return latestDate;

        }
    }
}
