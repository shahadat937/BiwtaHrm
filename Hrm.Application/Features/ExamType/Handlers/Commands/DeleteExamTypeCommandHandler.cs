using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using MediatR;

using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Domain;

namespace hrm.Application.Features.ExamTypes.Handlers.Commands
{
    public class DeleteExamTypeCommandHandler : IRequestHandler<DeleteExamTypeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteExamTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteExamTypeCommand request, CancellationToken cancellationToken)
        {
            var ExamType = await _unitOfWork.Repository<ExamType>().Get(request.ExamTypeId);

            if (ExamType == null)
                throw new NotFoundException(nameof(ExamType), request.ExamTypeId);

            await _unitOfWork.Repository<ExamType>().Delete(ExamType);
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
