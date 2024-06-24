using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.TrainingType;
using Hrm.Application.Features.TrainingType.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.TrainingType.Handlers.Queries
{
    public class GetTrainingTypeRequestHandler : IRequestHandler<GetTrainingTypeRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.TrainingType> _trainingTypeRepository;
        private readonly IMapper _mapper;

        public GetTrainingTypeRequestHandler(IHrmRepository<Hrm.Domain.TrainingType> trainingTypeRepository, IMapper mapper)
        {
            _trainingTypeRepository = trainingTypeRepository;
            _mapper = mapper;
        }


        public async Task<object> Handle(GetTrainingTypeRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.TrainingType> trainingTypes = _trainingTypeRepository.Where(x => true);
            var TrainingTypeDtos = await Task.Run(() => _mapper.Map<List<TrainingTypeDto>>(trainingTypes));

            return TrainingTypeDtos;
        }
    }
}
