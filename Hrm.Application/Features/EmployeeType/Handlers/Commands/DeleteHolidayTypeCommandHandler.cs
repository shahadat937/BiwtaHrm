using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using MediatR;

using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Domain;

namespace SchoolManagement.Application.Features.HanHolidayTypesdlers.Commands
{
    public class DeleteHolidayTypeCommandHandler : IRequestHandler<DeleteHolidayTypeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteHolidayTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteHolidayTypeCommand request, CancellationToken cancellationToken)
        {
            var HolidayType = await _unitOfWork.Repository<HolidayType>().Get(request.HolidayTypeId);

            if (HolidayType == null)
                throw new NotFoundException(nameof(HolidayType), request.HolidayTypeId);

            await _unitOfWork.Repository<HolidayType>().Delete(HolidayType);
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
