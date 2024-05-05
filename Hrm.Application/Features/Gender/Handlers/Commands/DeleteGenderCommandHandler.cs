using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using MediatR;

using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Domain;

namespace hrm.Application.Features.Genders.Handlers.Commands
{
    public class DeleteGenderCommandHandler : IRequestHandler<DeleteGenderCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteGenderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteGenderCommand request, CancellationToken cancellationToken)
        {
            var Gender = await _unitOfWork.Repository<Gender>().Get(request.GenderId);

            if (Gender == null)
                throw new NotFoundException(nameof(Gender), request.GenderId);

            await _unitOfWork.Repository<Gender>().Delete(Gender);
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
