using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using MediatR;

using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Domain;

namespace hrm.Application.Features.Punishments.Handlers.Commands
{
    public class DeletePunishmentCommandHandler : IRequestHandler<DeletePunishmentCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeletePunishmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeletePunishmentCommand request, CancellationToken cancellationToken)
        {
            var Punishment = await _unitOfWork.Repository<Punishment>().Get(request.PunishmentId);

            if (Punishment == null)
                throw new NotFoundException(nameof(Punishment), request.PunishmentId);

            await _unitOfWork.Repository<Punishment>().Delete(Punishment);
            try
            {
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
            //await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
