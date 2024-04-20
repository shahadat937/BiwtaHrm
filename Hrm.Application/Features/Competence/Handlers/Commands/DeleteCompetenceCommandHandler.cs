using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using MediatR;

using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Domain;

namespace SchoolManagement.Application.Features.Competences.Handlers.Commands
{
    public class DeleteCompetenceCommandHandler : IRequestHandler<DeleteCompetenceCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteCompetenceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteCompetenceCommand request, CancellationToken cancellationToken)
        {
            var Competence = await _unitOfWork.Repository<Competence>().Get(request.CompetenceId);

            if (Competence == null)
                throw new NotFoundException(nameof(Competence), request.CompetenceId);

            await _unitOfWork.Repository<Competence>().Delete(Competence);
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
