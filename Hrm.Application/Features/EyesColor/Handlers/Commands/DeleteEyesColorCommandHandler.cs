using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using MediatR;

using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Domain;

namespace hrm.Application.Features.EyesColors.Handlers.Commands
{
    public class DeleteEyesColorCommandHandler : IRequestHandler<DeleteEyesColorCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteEyesColorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteEyesColorCommand request, CancellationToken cancellationToken)
        {
            var EyesColor = await _unitOfWork.Repository<EyesColor>().Get(request.EyesColorId);

            if (EyesColor == null)
                throw new NotFoundException(nameof(EyesColor), request.EyesColorId);

            await _unitOfWork.Repository<EyesColor>().Delete(EyesColor);
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
