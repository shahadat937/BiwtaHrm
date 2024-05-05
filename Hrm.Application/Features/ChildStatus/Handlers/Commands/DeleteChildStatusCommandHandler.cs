using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using MediatR;

using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Domain;

namespace hrm.Application.Features.ChildStatuss.Handlers.Commands
{
    public class DeleteChildStatusCommandHandler : IRequestHandler<DeleteChildStatusCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteChildStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteChildStatusCommand request, CancellationToken cancellationToken)
        {
            var ChildStatus = await _unitOfWork.Repository<ChildStatus>().Get(request.ChildStatusId);

            if (ChildStatus == null)
                throw new NotFoundException(nameof(ChildStatus), request.ChildStatusId);

            await _unitOfWork.Repository<ChildStatus>().Delete(ChildStatus);
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
