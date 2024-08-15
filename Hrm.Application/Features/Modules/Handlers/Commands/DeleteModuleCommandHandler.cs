using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Modules.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Modules.Handlers.Commands
{
    public class DeleteModuleCommandHandler : IRequestHandler<DeleteModuleCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteModuleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteModuleCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var Module = await _unitOfWork.Repository<Hrm.Domain.Module>().Get(request.Id);

            if (Module == null)
                throw new NotFoundException(nameof(Module), request.Id);

            await _unitOfWork.Repository<Hrm.Domain.Module>().Delete(Module);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successful";
            response.Id = Module.ModuleId;

            return response;
        }
    }
}