using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.TrainingName;
using Hrm.Application.Features.TrainingName.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.TrainingName.Handlers.Queries
{
    public class GetTrainingNameRequestHandler : IRequestHandler<GetTrainingNameRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.TrainingName> _TrainingNameRepository;
        private readonly IMapper _mapper;
        public GetTrainingNameRequestHandler(IHrmRepository<Hrm.Domain.TrainingName> TrainingNameRepository, IMapper mapper)
        {
            _TrainingNameRepository = TrainingNameRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetTrainingNameRequest request, CancellationToken cancellationToken)
        {
            //IQueryable<Hrm.Domain.TrainingName> TrainingName = _TrainingNameRepository.Where(x => true);

            //var TrainingNameDtos = _mapper.Map<List<TrainingNameDto>>(TrainingName);

            //return TrainingNameDtos;



            IQueryable<Hrm.Domain.TrainingName> TrainingName = _TrainingNameRepository.Where(x => true);

            // Use Task.Run to offload the synchronous operation to a background thread
            var TrainingNameDtos = await Task.Run(() => _mapper.Map<List<TrainingNameDto>>(TrainingName));

            return TrainingNameDtos;
        }
    }
}
