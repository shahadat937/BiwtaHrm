using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.appraisalFormType;
using Hrm.Application.Features.appraisalFormType.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.appraisalFormType.Handlers.Queries
{
    public class GetappraisalFormTypeRequestHandler : IRequestHandler<GetappraisalFormTypeRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.appraisalFormType> _appraisalFormTypeRepository;
        private readonly IMapper _mapper;
        public GetappraisalFormTypeRequestHandler(IHrmRepository<Hrm.Domain.appraisalFormType> appraisalFormTypeRepository, IMapper mapper)
        {
            _appraisalFormTypeRepository = appraisalFormTypeRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetappraisalFormTypeRequest request, CancellationToken cancellationToken)
        {
            //IQueryable<Hrm.Domain.appraisalFormType> appraisalFormType = _appraisalFormTypeRepository.Where(x => true);

            //var appraisalFormTypeDtos = _mapper.Map<List<appraisalFormTypeDto>>(appraisalFormType);

            //return appraisalFormTypeDtos;



            IQueryable<Hrm.Domain.appraisalFormType> appraisalFormType = _appraisalFormTypeRepository.Where(x => true);

            // Use Task.Run to offload the synchronous operation to a background thread
            var appraisalFormTypeDtos = await Task.Run(() => _mapper.Map<List<appraisalFormTypeDto>>(appraisalFormType));

            return appraisalFormTypeDtos;
        }
    }
}
