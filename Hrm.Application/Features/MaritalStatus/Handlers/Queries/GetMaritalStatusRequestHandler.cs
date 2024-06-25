using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.MaritalStatus.Handlers.Queries
{
    public class GetMaritalStatusRequestHandler : IRequestHandler<GetMaritalStatusRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.MaritalStatus> _maritalStatusRepository;
        private readonly IMapper _mapper;
        public GetMaritalStatusRequestHandler(IHrmRepository<Hrm.Domain.MaritalStatus> maritalStatusRepository, IMapper mapper)
        {
            _maritalStatusRepository = maritalStatusRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetMaritalStatusRequest request, CancellationToken cancellationToken)
        {
            //IQueryable<Hrm.Domain.MaritalStatus> maritalStatus = _maritalStatusRepository.Where(x => true);

            //var MaritalStatusDtos = _mapper.Map<List<MaritalStatusDto>>(maritalStatus);

            //return MaritalStatusDtos;



            IQueryable<Hrm.Domain.MaritalStatus> maritalStatus = _maritalStatusRepository.Where(x => true);

            // Use Task.Run to offload the synchronous operation to a background thread
            var MaritalStatusDtos = await Task.Run(() => _mapper.Map<List<MaritalStatusDto>>(maritalStatus));

            return MaritalStatusDtos;
        }
    }
}
