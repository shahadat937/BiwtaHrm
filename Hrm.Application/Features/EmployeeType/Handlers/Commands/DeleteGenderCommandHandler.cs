using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using MediatR;

using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Domain;

namespace SchoolManagement.Application.Features.EmployeeTypes.Handlers.Commands
{
    public class DeleteEmployeeTypeCommandHandler : IRequestHandler<DeleteEmployeeTypeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteEmployeeTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteEmployeeTypeCommand request, CancellationToken cancellationToken)
        {
            var EmployeeType = await _unitOfWork.Repository<EmployeeType>().Get(request.EmployeeTypeId);

            if (EmployeeType == null)
                throw new NotFoundException(nameof(EmployeeType), request.EmployeeTypeId);

            await _unitOfWork.Repository<EmployeeType>().Delete(EmployeeType);
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
