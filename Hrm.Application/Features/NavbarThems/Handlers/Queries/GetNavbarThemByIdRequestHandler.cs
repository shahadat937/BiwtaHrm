using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.NavbarThem;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.NavbarThems.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.NavbarThems.Handlers.Queries
{
    public class GetNavbarThemByIdRequestHandler : IRequestHandler<GetNavbarThemByIdRequest, NavbarThemDto>
    {

        private readonly IHrmRepository<Hrm.Domain.NavbarThem> _NavbarThemRepository;
        private readonly IMapper _mapper;
        public GetNavbarThemByIdRequestHandler(IHrmRepository<Hrm.Domain.NavbarThem> NavbarThemRepositoy, IMapper mapper)
        {
            _NavbarThemRepository = NavbarThemRepositoy;
            _mapper = mapper;
        }

        public async Task<NavbarThemDto> Handle(GetNavbarThemByIdRequest request, CancellationToken cancellationToken)
        {
            var NavbarThem = await _NavbarThemRepository.Get(request.NavbarThemId);
            return _mapper.Map<NavbarThemDto>(NavbarThem);
        }
    }
}
