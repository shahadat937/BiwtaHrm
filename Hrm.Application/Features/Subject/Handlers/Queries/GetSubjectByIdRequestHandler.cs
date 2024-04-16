using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Subject;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Subject.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Subject.Handlers.Queries
{
    public class GetSubjectByIdRequestHandler : IRequestHandler<GetSubjectByIdRequest, SubjectDto>
    {

        private readonly IHrmRepository<Hrm.Domain.Subject> _SubjectRepository;
        private readonly IMapper _mapper;
        public GetSubjectByIdRequestHandler(IHrmRepository<Hrm.Domain.Subject> SubjectRepositoy, IMapper mapper)
        {
            _SubjectRepository = SubjectRepositoy;
            _mapper = mapper;
        }

        public async Task<SubjectDto> Handle(GetSubjectByIdRequest request, CancellationToken cancellationToken)
        {
            var Subject = await _SubjectRepository.Get(request.SubjectId);
            return _mapper.Map<SubjectDto>(Subject);
        }
    }
}
