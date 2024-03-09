using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using MediatR;

using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Domain;

namespace SchoolManagement.Application.Features.Religions.Handlers.Commands
{
    public class DeleteReligionCommandHandler : IRequestHandler<DeleteReligionCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteReligionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteReligionCommand request, CancellationToken cancellationToken)
        {
            var Religion = await _unitOfWork.Repository<Religion>().Get(request.ReligionId);

            if (Religion == null)
                throw new NotFoundException(nameof(Religion), request.ReligionId);

            await _unitOfWork.Repository<Religion>().Delete(Religion);
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
