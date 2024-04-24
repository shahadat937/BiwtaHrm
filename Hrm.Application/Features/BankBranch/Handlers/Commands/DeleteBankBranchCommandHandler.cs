using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using MediatR;

using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Domain;

namespace SchoolManagement.Application.Features.BankBranchs.Handlers.Commands
{
    public class DeleteBankBranchCommandHandler : IRequestHandler<DeleteBankBranchCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteBankBranchCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteBankBranchCommand request, CancellationToken cancellationToken)
        {
            var BankBranch = await _unitOfWork.Repository<BankBranch>().Get(request.BankBranchId);

            if (BankBranch == null)
                throw new NotFoundException(nameof(BankBranch), request.BankBranchId);

            await _unitOfWork.Repository<BankBranch>().Delete(BankBranch);
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
