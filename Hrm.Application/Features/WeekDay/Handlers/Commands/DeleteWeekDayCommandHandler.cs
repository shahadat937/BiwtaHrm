using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using MediatR;

using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Domain;

namespace hrm.Application.Features.Weekends.Handlers.Commands
{
    public class DeleteWeekDayCommandHandler : IRequestHandler<DeleteWeekDayCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteWeekDayCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteWeekDayCommand request, CancellationToken cancellationToken)
        {
            var Weekend = await _unitOfWork.Repository<WeekDay>().Get(request.WeekendId);

            if (Weekend == null)
                throw new NotFoundException(nameof(Weekend), request.WeekendId);

            await _unitOfWork.Repository<WeekDay>().Delete(Weekend);
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
