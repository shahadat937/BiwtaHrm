using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Punishment;
using Hrm.Application.DTOs.Punishment;
using Hrm.Application.Exceptions;
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
            IQueryable<Hrm.Domain.Punishment> Punishment = _PunishmentRepository.Where(x => true);

            var PunishmentDtos = await Task.Run(() => _mapper.Map<List<PunishmentDto>>(Punishment));

            return PunishmentDtos;
        }
    }
}