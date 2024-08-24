using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.AspNetUsers;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.Features.AspNetUsers.Requests.Queries;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.AspNetUsers.Handlers.Queries
{
    public class GetUserListRequestHandler : IRequestHandler<GetUserListRequest, object>
    {

        private readonly IHrmRepository<Domain.AspNetUsers> _aspNetUserRepository;
        private readonly IMapper _mapper;

        public GetUserListRequestHandler(IHrmRepository<Domain.AspNetUsers> aspNetUserRepository, IMapper mapper)
        {
            _aspNetUserRepository = aspNetUserRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetUserListRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Domain.AspNetUsers> aspNetUsers = _aspNetUserRepository.Where(x => true)
                .Include(x => x.EmpBasicInfo)
                .ThenInclude(ebi => ebi.EmpJobDetail)
                .ThenInclude(ejd => ejd.Department)
                .Include(x => x.EmpBasicInfo)
                .ThenInclude(ebi => ebi.EmpJobDetail)
                .ThenInclude(ejd => ejd.Designation);

            aspNetUsers = aspNetUsers.OrderByDescending(x => x.DateCreated);

            var UsersDtos = _mapper.Map<List<AspNetUserDto>>(aspNetUsers.ToList());

            return UsersDtos;
        }
    }
}
