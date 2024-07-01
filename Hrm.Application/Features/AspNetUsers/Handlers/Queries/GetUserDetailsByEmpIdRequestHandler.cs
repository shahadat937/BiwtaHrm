using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.AspNetUsers.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.AspNetUsers.Handlers.Queries
{
    public class GetUserDetailsByEmpIdRequestHandler : IRequestHandler<GetUserDetailsByEmpIdRequest, object>
    {
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Hrm.Domain.AspNetUsers> _AspNetUserRepository;
        public GetUserDetailsByEmpIdRequestHandler(IHrmRepository<Hrm.Domain.AspNetUsers> AspNetUserRepository, IMapper mapper)
        {
            _AspNetUserRepository = AspNetUserRepository;
            _mapper = mapper;
        }
        public async Task<object> Handle(GetUserDetailsByEmpIdRequest request, CancellationToken cancellationToken)
        {

            var aspNetUsers = await _AspNetUserRepository.FindOneAsync(x => x.EmpId == request.Id);

            return aspNetUsers;
        }
    }
}