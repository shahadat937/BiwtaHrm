using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Punishments.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Punishments.Handlers.Queries
{ 
    public class GetSelectedPunishmentRequestHandler : IRequestHandler<GetSelectedPunishmentRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.Punishment> _PunishmentRepository;


        public GetSelectedPunishmentRequestHandler(IHrmRepository<Hrm.Domain.Punishment> PunishmentRepository)
        {
            _PunishmentRepository = PunishmentRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedPunishmentRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.Punishment> Punishments = await _PunishmentRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = Punishments.Select(x => new SelectedModel 
            {
                Name = x.PunishmentName,
                Id = x.PunishmentId
            }).ToList();
            return selectModels;
        }
    }
}
 