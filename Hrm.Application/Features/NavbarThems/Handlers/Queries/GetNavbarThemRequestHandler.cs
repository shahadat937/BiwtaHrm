using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.NavbarThem;
using Hrm.Application.DTOs.NavbarThem;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.NavbarThems.Requests.Queries;
using Hrm.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.NavbarThems.Handlers.Queries
{
    public class GetNavbarThemRequestHandler : IRequestHandler<GetNavbarThemRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.NavbarThem> _NavbarThemRepository;
        private readonly IMapper _mapper;
        public GetNavbarThemRequestHandler(IHrmRepository<Hrm.Domain.NavbarThem> NavbarThemRepository, IMapper mapper)
        {
            _NavbarThemRepository = NavbarThemRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetNavbarThemRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.NavbarThem> NavbarThems = _NavbarThemRepository.Where(x => true);

            NavbarThems = NavbarThems.OrderByDescending(x => x.Id);

            var NavbarThemDtos = _mapper.Map<List<NavbarThemDto>>(NavbarThems);

            return NavbarThemDtos;
        }
    }
}