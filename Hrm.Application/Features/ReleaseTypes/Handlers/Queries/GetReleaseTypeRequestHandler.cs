using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.ReleaseType;
using Hrm.Application.DTOs.ReleaseType;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.ReleaseTypes.Requests.Queries;
using Hrm.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.ReleaseTypes.Handlers.Queries
{
    public class GetReleaseTypeRequestHandler : IRequestHandler<GetReleaseTypeRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.ReleaseType> _ReleaseTypeRepository;
        private readonly IMapper _mapper;
        public GetReleaseTypeRequestHandler(IHrmRepository<Hrm.Domain.ReleaseType> ReleaseTypeRepository, IMapper mapper)
        {
            _ReleaseTypeRepository = ReleaseTypeRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetReleaseTypeRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.ReleaseType> ReleaseTypes = _ReleaseTypeRepository.Where(x => true);

            ReleaseTypes = ReleaseTypes.OrderByDescending(x => x.ReleaseTypeId);

            var ReleaseTypeDtos = _mapper.Map<List<ReleaseTypeDto>>(ReleaseTypes);

            return ReleaseTypeDtos;
        }
    }
}