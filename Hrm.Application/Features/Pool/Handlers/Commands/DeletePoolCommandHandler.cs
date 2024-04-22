using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using MediatR;

using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Domain;

namespace SchoolManagement.Application.Features.Pools.Handlers.Commands
{
    public class DeletePoolCommandHandler : IRequestHandler<DeletePoolCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeletePoolCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeletePoolCommand request, CancellationToken cancellationToken)
        {
            var Pool = await _unitOfWork.Repository<Pool>().Get(request.PoolId);

            if (Pool == null)
                throw new NotFoundException(nameof(Pool), request.PoolId);

            await _unitOfWork.Repository<Pool>().Delete(Pool);
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
