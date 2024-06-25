using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.ChildStatus;
using Hrm.Application.DTOs.EmployeeType;
using Hrm.Application.DTOs.Gender;
using Hrm.Application.DTOs.Religion;
using Hrm.Application.Features.BloodGroups.Requests.Queries;
using Hrm.Application.Features.ChildStatus.Requests.Queries;
using Hrm.Application.Features.EmployeeType.Requests.Queries;
using Hrm.Application.Features.Gender.Requests.Queries;
using Hrm.Application.Features.Religion.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Gender.Handlers.Queries
{
    public class GetGenderRequestByIdHandler : IRequestHandler<GetGenderByIdRequest, GenderDto>
    {
        // private readonly IUTOfficerTypeRepository _UTOfficerTypeRepository;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Hrm.Domain.Gender> _GenderRepository;
        public GetGenderRequestByIdHandler(IHrmRepository<Hrm.Domain.Gender> GenderRepository, IMapper mapper)
        {
            _GenderRepository = GenderRepository;
            _mapper = mapper;
        }
        public async Task<GenderDto> Handle(GetGenderByIdRequest request, CancellationToken cancellationToken)
        {
            var Gender = await _GenderRepository.Get(request.GenderId);
            return _mapper.Map<GenderDto>(Gender);
        }
    }
}
