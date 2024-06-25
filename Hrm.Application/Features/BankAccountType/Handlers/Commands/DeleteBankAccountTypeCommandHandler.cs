using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using MediatR;

using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Domain;

namespace hrm.Application.Features.BankAccountTypes.Handlers.Commands
{
    public class DeleteBankAccountTypeCommandHandler : IRequestHandler<DeleteBankAccountTypeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteBankAccountTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteBankAccountTypeCommand request, CancellationToken cancellationToken)
        {
            var BankAccountType = await _unitOfWork.Repository<BankAccountType>().Get(request.BankAccountTypeId);

            if (BankAccountType == null)
                throw new NotFoundException(nameof(BankAccountType), request.BankAccountTypeId);

            await _unitOfWork.Repository<BankAccountType>().Delete(BankAccountType);
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
