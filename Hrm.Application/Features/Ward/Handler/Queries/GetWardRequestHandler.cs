using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.TrainingType;
using Hrm.Application.DTOs.Ward;
using Hrm.Application.Features.TrainingType.Requests.Queries;
using Hrm.Application.Features.Ward.Request.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Ward.Handler.Queries
{
    public class GetWardRequestHandler : IRequestHandler<GetWardRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.Ward> _wardRepository;
        private readonly IMapper _mapper;

        public GetWardRequestHandler(IHrmRepository<Hrm.Domain.Ward> wardRepository, IMapper mapper)
        {
            _wardRepository = wardRepository;
            _mapper = mapper;
        }


        public async Task<object> Handle(GetWardRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.Ward> wards = _wardRepository.Where(x => true);
            var WardDtos = await Task.Run(() => _mapper.Map<List<WardDto>>(wards));

            return WardDtos;
        }
    }
}
