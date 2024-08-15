using AutoMapper;
using MediatR;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hrm.Application.Features.Modules.Requests.Queries;
using Hrm.Application.DTOs.Common.Validators;
using Hrm.Application.DTOs.Modules;
using Hrm.Application.Models;
using Hrm.Application.Contracts.Persistence;
using Hrm.Domain;

namespace Hrm.Application.Features.Modules.Handlers.Queries
{
    public class GetModuleListRequestHandler : IRequestHandler<GetModuleListRequest, object>
    {

        private readonly IHrmRepository<Module> _ModuleRepository;

        private readonly IMapper _mapper;

        public GetModuleListRequestHandler(IHrmRepository<Module> ModuleRepository, IMapper mapper)
        {
            _ModuleRepository = ModuleRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetModuleListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();

            IQueryable<Module> Modules = _ModuleRepository.Where(x => x.IsActive == true);

            Modules = Modules.OrderBy(x => x.MenuPosition);

            var ModulesDtos = _mapper.Map<List<ModuleDto>>(Modules);


            return ModulesDtos;
        }
    }
}
