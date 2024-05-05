using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using MediatR; 

using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Domain;

namespace hrm.Application.Features.Office.Handlers.Commands
{
    public class DeleteOfficeCommandHandler : IRequestHandler<DeleteOfficeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteOfficeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteOfficeCommand request, CancellationToken cancellationToken)
        {
            var Office = await _unitOfWork.Repository<Hrm.Domain.Office>().Get(request.OfficeId);

            if (Office == null)
                throw new NotFoundException(nameof(Office), request.OfficeId);

            await _unitOfWork.Repository<Hrm.Domain.Office>().Delete(Office);
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
