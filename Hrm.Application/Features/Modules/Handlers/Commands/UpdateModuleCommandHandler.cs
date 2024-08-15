using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Modules.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Modules.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Modules.Handlers.Commands
{
    public class UpdateModuleCommandHandler : IRequestHandler<UpdateModuleCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateModuleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateModuleCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var Module = await _unitOfWork.Repository<Hrm.Domain.Module>().Get(request.ModuleDto.ModuleId);

            if (Module is null)
                throw new NotFoundException(nameof(Module), request.ModuleDto.ModuleId);

            _mapper.Map(request.ModuleDto, Module);

            await _unitOfWork.Repository<Hrm.Domain.Module>().Update(Module);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Update Successful";
            response.Id = Module.ModuleId;

            return response;
        }
    }
}