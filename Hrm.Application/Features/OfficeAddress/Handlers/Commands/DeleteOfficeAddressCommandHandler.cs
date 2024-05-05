using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using MediatR;

using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Domain;

namespace hrm.Application.Features.OfficeAddresss.Handlers.Commands
{
    public class DeleteOfficeAddressCommandHandler : IRequestHandler<DeleteOfficeAddressCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteOfficeAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteOfficeAddressCommand request, CancellationToken cancellationToken)
        {
            var OfficeAddress = await _unitOfWork.Repository<OfficeAddress>().Get(request.OfficeAddressId);

            if (OfficeAddress == null)
                throw new NotFoundException(nameof(OfficeAddress), request.OfficeAddressId);

            await _unitOfWork.Repository<OfficeAddress>().Delete(OfficeAddress);
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
