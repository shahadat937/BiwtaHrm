using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using MediatR;

using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Domain;

namespace hrm.Application.Features.HairColors.Handlers.Commands
{
    public class DeleteHairColorCommandHandler : IRequestHandler<DeleteHairColorCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteHairColorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteHairColorCommand request, CancellationToken cancellationToken)
        {
            var HairColor = await _unitOfWork.Repository<HairColor>().Get(request.HairColorId);

            if (HairColor == null)
                throw new NotFoundException(nameof(HairColor), request.HairColorId);

            await _unitOfWork.Repository<HairColor>().Delete(HairColor);
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
