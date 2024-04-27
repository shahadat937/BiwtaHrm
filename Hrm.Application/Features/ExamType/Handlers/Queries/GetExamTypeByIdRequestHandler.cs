using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.ExamType;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.ExamType.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.ExamType.Handlers.Queries
{
    public class GetExamTypeByIdRequestHandler : IRequestHandler<GetExamTypeByIdRequest, ExamTypeDto>
    {

        private readonly IHrmRepository<Hrm.Domain.ExamType> _ExamTypeRepository;
        private readonly IMapper _mapper;
        public GetExamTypeByIdRequestHandler(IHrmRepository<Hrm.Domain.ExamType> ExamTypeRepositoy, IMapper mapper)
        {
            _ExamTypeRepository = ExamTypeRepositoy;
            _mapper = mapper;
        }

        public async Task<ExamTypeDto> Handle(GetExamTypeByIdRequest request, CancellationToken cancellationToken)
        {
            var ExamType = await _ExamTypeRepository.Get(request.ExamTypeId);
            return _mapper.Map<ExamTypeDto>(ExamType);
        }
    }
}
