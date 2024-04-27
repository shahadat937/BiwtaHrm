using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Subject;
using Hrm.Application.DTOs.Subject;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Subject.Requests.Queries;
using Hrm.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Subject.Handlers.Queries
{
    public class GetSubjectRequestHandler : IRequestHandler<GetSubjectRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.Subject> _SubjectRepository;
        private readonly IMapper _mapper;
        public GetSubjectRequestHandler(IHrmRepository<Hrm.Domain.Subject> SubjectRepository, IMapper mapper)
        {
            _SubjectRepository = SubjectRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetSubjectRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.Subject> Subject = _SubjectRepository.Where(x => true);
            Subject = Subject.OrderByDescending(x => x.SubjectId);
            var SubjectDtos = _mapper.Map<List<SubjectDto>>(Subject);

            return SubjectDtos;
        }
    }
}