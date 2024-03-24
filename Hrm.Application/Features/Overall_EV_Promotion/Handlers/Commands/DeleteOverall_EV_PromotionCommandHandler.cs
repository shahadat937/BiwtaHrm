using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using MediatR;

using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Domain;

namespace SchoolManagement.Application.Features.Overall_EV_Promotions.Handlers.Commands
{
    public class DeleteOverall_EV_PromotionCommandHandler : IRequestHandler<DeleteOverall_EV_PromotionCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteOverall_EV_PromotionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteOverall_EV_PromotionCommand request, CancellationToken cancellationToken)
        {
            var Overall_EV_Promotion = await _unitOfWork.Repository<Overall_EV_Promotion>().Get(request.Overall_EV_PromotionId);

            if (Overall_EV_Promotion == null)
                throw new NotFoundException(nameof(Overall_EV_Promotion), request.Overall_EV_PromotionId);

            await _unitOfWork.Repository<Overall_EV_Promotion>().Delete(Overall_EV_Promotion);
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
