using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Modules;
using Hrm.Application.Features.Modules.Requests.Queries;
using Hrm.Domain;
using MediatR;


namespace Hrm.Application.Features.Modules.Handlers.Queries
{
    public class GetModulesDetailRequestHandler : IRequestHandler<GetModuleDetailRequest, ModuleDto>
    {
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Module> _ModuleRepository;
        public GetModulesDetailRequestHandler(IHrmRepository<Module> ModuleRepository, IMapper mapper)
        {
            _ModuleRepository = ModuleRepository;
            _mapper = mapper;
        }
        public async Task<ModuleDto> Handle(GetModuleDetailRequest request, CancellationToken cancellationToken)
        {
            var Module = await _ModuleRepository.Get(request.Id);
            return _mapper.Map<ModuleDto>(Module);
        }
    }
}
