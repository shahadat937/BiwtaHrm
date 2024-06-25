using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.ExamType;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.ExamType.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.ExamType.Handlers.Queries
{
    public class GetExamTypeRequestHandler : IRequestHandler<GetExamTypeRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.ExamType> _ExamTypeRepository;
        private readonly IMapper _mapper;
        public GetExamTypeRequestHandler(IHrmRepository<Hrm.Domain.ExamType> ExamTypeRepository, IMapper mapper)
        {
            _ExamTypeRepository = ExamTypeRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetExamTypeRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.ExamType> ExamTypes = _ExamTypeRepository.Where(x => true);
            ExamTypes = ExamTypes.OrderByDescending(x => x.ExamTypeId);

            var ExamTypeDtos = _mapper.Map<List<ExamTypeDto>>(ExamTypes);

            return ExamTypeDtos;
        }
    }
}
