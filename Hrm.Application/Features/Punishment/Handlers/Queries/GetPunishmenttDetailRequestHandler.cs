using AutoMapper;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hrm.Application.Features.Punishments.Requests.Queries;
using Hrm.Application.DTOs.Punishment;
using Hrm.Application.Contracts.Persistence;
using Hrm.Domain;

namespace Hrm.Application.Features.Punishments.Handlers.Queries
{
    public class GetPunishmentDetailRequestHandler : IRequestHandler<GetPunishmentDetailRequest, PunishmentDto>
    {
        // private readonly IUTOfficerTypeRepository _UTOfficerTypeRepository;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Hrm.Domain.Punishment> _PunishmentRepository;
        public GetPunishmentDetailRequestHandler(IHrmRepository<Hrm.Domain.Punishment> PunishmentRepository, IMapper mapper)
        {
            _PunishmentRepository = PunishmentRepository;
            _mapper = mapper;
        }
        public async Task<PunishmentDto> Handle(GetPunishmentDetailRequest request, CancellationToken cancellationToken)
        {
            var Punishment = await _PunishmentRepository.Get(request.PunishmentId);
            return _mapper.Map<PunishmentDto>(Punishment);
        }
    }
}
