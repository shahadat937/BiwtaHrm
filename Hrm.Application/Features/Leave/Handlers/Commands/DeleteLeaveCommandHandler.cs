using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using MediatR;

using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Domain;

namespace SchoolManagement.Application.Features.Leaves.Handlers.Commands
{
    public class DeleteLeaveCommandHandler : IRequestHandler<DeleteLeaveCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteLeaveCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteLeaveCommand request, CancellationToken cancellationToken)
        {
            var Leave = await _unitOfWork.Repository<Leave>().Get(request.LeaveId);

            if (Leave == null)
                throw new NotFoundException(nameof(Leave), request.LeaveId);

            await _unitOfWork.Repository<Leave>().Delete(Leave);
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
