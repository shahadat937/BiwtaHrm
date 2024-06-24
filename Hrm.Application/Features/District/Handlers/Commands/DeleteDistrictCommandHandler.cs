using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using MediatR;

using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Domain;

namespace hrm.Application.Features.Districts.Handlers.Commands
{
    public class DeleteDistrictCommandHandler : IRequestHandler<DeleteDistrictCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteDistrictCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteDistrictCommand request, CancellationToken cancellationToken)
        {
            var District = await _unitOfWork.Repository<District>().Get(request.DistrictId);

            if (District == null)
                throw new NotFoundException(nameof(District), request.DistrictId);

            await _unitOfWork.Repository<District>().Delete(District);
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
