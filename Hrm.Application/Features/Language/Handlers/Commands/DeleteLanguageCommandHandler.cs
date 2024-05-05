using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using MediatR;

using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Domain;

namespace hrm.Application.Features.Languages.Handlers.Commands
{
    public class DeleteLanguageCommandHandler : IRequestHandler<DeleteLanguageCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteLanguageCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
        {
            var Language = await _unitOfWork.Repository<Language>().Get(request.LanguageId);

            if (Language == null)
                throw new NotFoundException(nameof(Language), request.LanguageId);

            await _unitOfWork.Repository<Language>().Delete(Language);
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
