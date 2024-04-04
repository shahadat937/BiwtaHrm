using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.TrainingType;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.TrainingType.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.TrainingType.Handlers.Queries
{
    public class GetTrainingTypeByIdRequestHandler : IRequestHandler<GetTrainingTypeByIdRequest, TrainingTypeDto>
    {

        private readonly IHrmRepository<Hrm.Domain.TrainingType> _TrainingTypeRepository;
        private readonly IMapper _mapper;
        public GetTrainingTypeByIdRequestHandler(IHrmRepository<Hrm.Domain.TrainingType> TrainingTypeRepositoy, IMapper mapper)
        {
            _TrainingTypeRepository = TrainingTypeRepositoy;
            _mapper = mapper;
        }

        public async Task<TrainingTypeDto> Handle(GetTrainingTypeByIdRequest request, CancellationToken cancellationToken)
        {
            var TrainingType = await _TrainingTypeRepository.Get(request.TrainingTypeId);
            return _mapper.Map<TrainingTypeDto>(TrainingType);
        }
    }
}
