using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Gender;
using Hrm.Application.DTOs.Gender;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Gender.Requests.Queries;
using Hrm.Application.Models;
using MediatR;
using SchoolManagement.Application.DTOs.Common.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Gender.Handlers.Queries
{
    public class GetGenderRequestHandler : IRequestHandler<GetGenderRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.Gender> _GenderRepository;
        private readonly IMapper _mapper;
        public GetGenderRequestHandler(IHrmRepository<Hrm.Domain.Gender> GenderRepository, IMapper mapper)
        {
            _GenderRepository = GenderRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetGenderRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.Gender> Gender = _GenderRepository.Where(x => true);

            var GenderDtos = _mapper.Map<List<GenderDto>>(Gender);

            return GenderDtos;
        }
    }
}