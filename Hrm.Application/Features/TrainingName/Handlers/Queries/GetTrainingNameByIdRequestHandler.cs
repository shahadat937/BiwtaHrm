using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.TrainingName;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.TrainingName.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.TrainingName.Handlers.Queries
{
    public class GetTrainingNameByIdRequestHandler : IRequestHandler<GetTrainingNameByIdRequest, TrainingNameDto>
    {

        private readonly IHrmRepository<Hrm.Domain.TrainingName> _TrainingNameRepository;
        private readonly IMapper _mapper;
        public GetTrainingNameByIdRequestHandler(IHrmRepository<Hrm.Domain.TrainingName> TrainingNameRepositoy, IMapper mapper)
        {
            _TrainingNameRepository = TrainingNameRepositoy;
            _mapper = mapper;
        }

        public async Task<TrainingNameDto> Handle(GetTrainingNameByIdRequest request, CancellationToken cancellationToken)
        {
            var TrainingName = await _TrainingNameRepository.Get(request.TrainingNameId);
            return _mapper.Map<TrainingNameDto>(TrainingName);
        }
    }
}
