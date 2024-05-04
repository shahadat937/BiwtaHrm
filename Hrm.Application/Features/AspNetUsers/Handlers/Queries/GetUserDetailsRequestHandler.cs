using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.AspNetUsers;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.Features.AspNetUsers.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.AspNetUsers.Handlers.Queries
{
    public class GetUserDetailsRequestHandler : IRequestHandler<GetUserDetailsRequest, object>
    {
        // private readonly IUTOfficerTypeRepository _UTOfficerTypeRepository;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Hrm.Domain.AspNetUsers> _AspNetUserRepository;
        public GetUserDetailsRequestHandler(IHrmRepository<Hrm.Domain.AspNetUsers> AspNetUserRepository, IMapper mapper)
        {
            _AspNetUserRepository = AspNetUserRepository;
            _mapper = mapper;
        }
        public async Task<object> Handle(GetUserDetailsRequest request, CancellationToken cancellationToken)
        {

            IQueryable<Hrm.Domain.AspNetUsers> users = _AspNetUserRepository.Where(x => x.Id == request.Id);


            var userDtos = _mapper.Map<List<AspNetUserDto>>(users.ToList());

            return userDtos;
        }
    }
}
