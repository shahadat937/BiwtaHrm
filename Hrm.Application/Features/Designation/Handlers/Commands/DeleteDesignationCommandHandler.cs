using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using MediatR;

using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Domain;

namespace hrm.Application.Features.Designations.Handlers.Commands
{
    public class DeleteDesignationCommandHandler : IRequestHandler<DeleteDesignationCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteDesignationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteDesignationCommand request, CancellationToken cancellationToken)
        {
            var Designation = await _unitOfWork.Repository<Designation>().Get(request.DesignationId);

            if (Designation == null)
                throw new NotFoundException(nameof(Designation), request.DesignationId);

            await _unitOfWork.Repository<Designation>().Delete(Designation);
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
