using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using MediatR;

using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Domain;

namespace SchoolManagement.Application.Features.Boards.Handlers.Commands
{
    public class DeleteBoardCommandHandler : IRequestHandler<DeleteBoardCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteBoardCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteBoardCommand request, CancellationToken cancellationToken)
        {
            var Board = await _unitOfWork.Repository<Board>().Get(request.BoardId);

            if (Board == null)
                throw new NotFoundException(nameof(Board), request.BoardId);

            await _unitOfWork.Repository<Board>().Delete(Board);
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
