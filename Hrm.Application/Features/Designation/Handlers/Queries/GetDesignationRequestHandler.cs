using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Designation;
using Hrm.Application.DTOs.Designation;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Designation.Requests.Queries;
using Hrm.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Designation.Handlers.Queries
{
    public class GetDesignationRequestHandler : IRequestHandler<GetDesignationRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.Designation> _DesignationRepository;
        private readonly IMapper _mapper;
        public GetDesignationRequestHandler(IHrmRepository<Hrm.Domain.Designation> DesignationRepository, IMapper mapper)
        {
            _DesignationRepository = DesignationRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetDesignationRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.Designation> Designation = _DesignationRepository.Where(x => true);

            var DesignationDtos = _mapper.Map<List<DesignationDto>>(Designation);

            return DesignationDtos;
        }
    }
}