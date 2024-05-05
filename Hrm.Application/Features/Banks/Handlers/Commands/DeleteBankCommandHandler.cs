using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using MediatR;

using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Domain;

namespace hrm.Application.Features.Banks.Handlers.Commands
{
    public class DeleteBankCommandHandler : IRequestHandler<DeleteBankCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteBankCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteBankCommand request, CancellationToken cancellationToken)
        {
            var Bank = await _unitOfWork.Repository<Hrm.Domain.Bank>().Get(request.BankId);

            if (Bank == null)
                throw new NotFoundException(nameof(Bank), request.BankId);

            await _unitOfWork.Repository<Hrm.Domain.Bank>().Delete(Bank);
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
