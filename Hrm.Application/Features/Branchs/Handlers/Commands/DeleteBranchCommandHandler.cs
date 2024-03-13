using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using MediatR;

using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Domain;

namespace SchoolManagement.Application.Features.Branchs.Handlers.Commands
{
    public class DeleteBranchCommandHandler : IRequestHandler<DeleteBranchCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteBranchCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteBranchCommand request, CancellationToken cancellationToken)
        {
            var Branch = await _unitOfWork.Repository<Branch>().Get(request.BranchId);

            if (Branch == null)
                throw new NotFoundException(nameof(Branch), request.BranchId);

            await _unitOfWork.Repository<Branch>().Delete(Branch);
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
