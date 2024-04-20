using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Institute;
using Hrm.Application.Features.Institute.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Institute.Handlers.Queries
{
    public class GetInstituteRequestHandler : IRequestHandler<GetInstituteRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.Institute> _InstituteRepository;
        private readonly IMapper _mapper;
        public GetInstituteRequestHandler(IHrmRepository<Hrm.Domain.Institute> InstituteRepository, IMapper mapper)
        {
            _InstituteRepository = InstituteRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetInstituteRequest request, CancellationToken cancellationToken)
        {
            //IQueryable<Hrm.Domain.Institute> Institute = _InstituteRepository.Where(x => true);

            //var InstituteDtos = _mapper.Map<List<InstituteDto>>(Institute);

            //return InstituteDtos;



            IQueryable<Hrm.Domain.Institute> Institute = _InstituteRepository.Where(x => true);

            // Use Task.Run to offload the synchronous operation to a background thread
            var InstituteDtos = await Task.Run(() => _mapper.Map<List<InstituteDto>>(Institute));

            return InstituteDtos;
        }
    }
}
