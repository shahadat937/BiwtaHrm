using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Upazila;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Upazila.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Upazila.Handlers.Queries
{
    public class GetUpazilaByIdRequestHandler : IRequestHandler<GetUpazilaByIdRequest, UpazilaDto>
    {

        private readonly IHrmRepository<Hrm.Domain.Upazila> _UpazilaRepository;
        private readonly IMapper _mapper;
        public GetUpazilaByIdRequestHandler(IHrmRepository<Hrm.Domain.Upazila> UpazilaRepositoy, IMapper mapper)
        {
            _UpazilaRepository = UpazilaRepositoy;
            _mapper = mapper;
        }

        public async Task<UpazilaDto> Handle(GetUpazilaByIdRequest request, CancellationToken cancellationToken)
        {
            var Upazila = await _UpazilaRepository.Get(request.UpazilaId);
            return _mapper.Map<UpazilaDto>(Upazila);
        }
    }
}
