using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using SendGrid;

namespace hrm.Application.Features.Designations.Handlers.Commands
{
    public class DeleteDesignationCommandHandler : IRequestHandler<DeleteDesignationCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteDesignationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteDesignationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var Designation = await _unitOfWork.Repository<Designation>().Get(request.DesignationId);

            if (Designation == null)
                throw new NotFoundException(nameof(Designation), request.DesignationId);


            await _unitOfWork.Repository<Hrm.Domain.Designation>().Delete(Designation);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = Designation.DesignationId;

            return response;
        }
    }
}
