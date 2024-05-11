using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.BloodGroups.Requests.Queries;
using Hrm.Application.Features.WeekDay.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.WeekDay.Handlers.Queries
{
    public class GetSelectedWeekDayRequestHandler : IRequestHandler<GetSelectedWeekDayRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.WeekDay> _WeekDayRepository;


        public GetSelectedWeekDayRequestHandler(IHrmRepository<Hrm.Domain.WeekDay> WeekDayRepository)
        {
            _WeekDayRepository = WeekDayRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedWeekDayRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.WeekDay> BloodGroups = await _WeekDayRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = BloodGroups.Select(x => new SelectedModel
            {
                Name = x.WeekDayName,
                Id = x.WeekDayId
            }).ToList();
            return selectModels;
        }
    }
}