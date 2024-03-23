using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.TaskName;
using Hrm.Application.Features.TaskName.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.TaskName.Handlers.Queries
{
 
    public class GetTaskNameRequestHandler : IRequestHandler<GetTaskNameRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.TaskName> _TaskNameRepository;
        private readonly IMapper _mapper;
        public GetTaskNameRequestHandler(IHrmRepository<Hrm.Domain.TaskName> TaskNameRepository, IMapper mapper)
        {
            _TaskNameRepository = TaskNameRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetTaskNameRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.TaskName> TaskName = _TaskNameRepository.Where(x => true);

            var TaskNameDtos = await Task.Run(() => _mapper.Map<List<TaskNameDto>>(TaskName));

            return TaskNameDtos;
        }
    }

}
