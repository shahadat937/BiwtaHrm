using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using MediatR;

using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Domain;

namespace SchoolManagement.Application.Features.SubDepartments.Handlers.Commands
{
    public class DeleteSubDepartmentCommandHandler : IRequestHandler<DeleteSubDepartmentCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteSubDepartmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteSubDepartmentCommand request, CancellationToken cancellationToken)
        {
            var SubDepartment = await _unitOfWork.Repository<SubDepartment>().Get(request.SubDepartmentId);

            if (SubDepartment == null)
                throw new NotFoundException(nameof(SubDepartment), request.SubDepartmentId);

            await _unitOfWork.Repository<SubDepartment>().Delete(SubDepartment);
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
