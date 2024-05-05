using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using MediatR;

using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Domain;

namespace hrm.Application.Features.Shifts.Handlers.Commands
{
    public class DeleteShiftCommandHandler : IRequestHandler<DeleteShiftCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteShiftCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteShiftCommand request, CancellationToken cancellationToken)
        {
            var Shift = await _unitOfWork.Repository<Shift>().Get(request.ShiftId);

            if (Shift == null)
                throw new NotFoundException(nameof(Shift), request.ShiftId);

            await _unitOfWork.Repository<Shift>().Delete(Shift);
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
