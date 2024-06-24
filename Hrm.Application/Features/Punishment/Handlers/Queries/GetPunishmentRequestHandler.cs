using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Punishment;
using Hrm.Application.DTOs.Punishment;
using Hrm.Application.DTOs.Punishment;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Punishment.Requests.Queries;
using Hrm.Application.Features.Punishment.Requests.Queries;
using Hrm.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Punishment.Handlers.Queries
{
    public class GetPunishmentRequestHandler : IRequestHandler<GetPunishmentRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.Punishment> _PunishmentRepository;
        private readonly IMapper _mapper;
        public GetPunishmentRequestHandler(IHrmRepository<Hrm.Domain.Punishment> PunishmentRepository, IMapper mapper)
        {
            _PunishmentRepository = PunishmentRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetPunishmentRequest request, CancellationToken cancellationToken)
        {
            // Fetch blood groups from repository
            IQueryable<Hrm.Domain.Punishment> Punishments = _PunishmentRepository.Where(x => true);

            // Order blood groups by descending order
            Punishments = Punishments.OrderByDescending(x => x.PunishmentId);

            // Map the ordered blood groups to PunishmentDto
            var PunishmentDtos = _mapper.Map<List<PunishmentDto>>(Punishments.ToList());

            return PunishmentDtos;
        }
    }
}