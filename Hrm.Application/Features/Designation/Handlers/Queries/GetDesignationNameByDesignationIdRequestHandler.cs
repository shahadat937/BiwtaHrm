using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Designation;
using Hrm.Application.Features.Designation.Requests.Queries;
using Hrm.Application.Features.OfficeBranch.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.OfficeBranch.Handlers.Queries
{
    public class GetDesignationNameByDesignationIdRequestHandler : IRequestHandler<GetDesignationNameByDesignationIdRequest, DesignationDto>
    {

        private readonly IHrmRepository<Hrm.Domain.Designation> _DesignationRepository;
        private readonly IMapper _mapper;
        public GetDesignationNameByDesignationIdRequestHandler(IHrmRepository<Hrm.Domain.Designation> DesignationRepository, IMapper mapper)
        {
            _DesignationRepository = DesignationRepository;
            _mapper = mapper;

        }

        public async Task<DesignationDto> Handle(GetDesignationNameByDesignationIdRequest request, CancellationToken cancellationToken)
        {
            var  Designations = _DesignationRepository.Where(x => x.DesignationId == request.DesignationId)
                .Include(x => x.DesignationSetup).FirstOrDefault(); ;

            return _mapper.Map<DesignationDto>(Designations);




        }
    }
}
