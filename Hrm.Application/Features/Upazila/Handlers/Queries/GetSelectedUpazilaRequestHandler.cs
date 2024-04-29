using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Upazila;
using Hrm.Application.Features.Upazila.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Upazila.Handlers.Queries
{ 
    public class GetSelectedUpazilaRequestHandler : IRequestHandler<GetSelectedUpazilaRequest, UpazilaDto>
    {
        private readonly IHrmRepository<Hrm.Domain.Upazila> _UpazilaRepository;
        private readonly IMapper _mapper;

        public GetSelectedUpazilaRequestHandler(IMapper mapper,IHrmRepository<Hrm.Domain.Upazila> UpazilaRepository)
        {
            _UpazilaRepository = UpazilaRepository;
            _mapper = mapper;
        }

        public async Task<UpazilaDto> Handle(GetSelectedUpazilaRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.Upazila> Upazilas = await _UpazilaRepository.FilterAsync(x => x.IsActive);
            var UpazilasDtos = _mapper.Map<UpazilaDto>(Upazilas);
            return UpazilasDtos;
        }
    }
}
 