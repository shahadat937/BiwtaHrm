using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using MediatR;

using Hrm.Application.Features.ReleaseTypes.Requests.Commands;
using Hrm.Domain;
using Hrm.Application.Responses;

namespace hrm.Application.Features.ReleaseTypes.Handlers.Commands
{
    public class DeleteReleaseTypeCommandHandler : IRequestHandler<DeleteReleaseTypeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteReleaseTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteReleaseTypeCommand request, CancellationToken cancellationToken)
        {

            var response = new BaseCommandResponse();

            var ReleaseType = await _unitOfWork.Repository<ReleaseType>().Get(request.ReleaseTypeId);

            if (ReleaseType == null)
            {
                response.Success = false;
                response.Message = "Creation Failed";
            }

            await _unitOfWork.Repository<ReleaseType>().Delete(ReleaseType);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Deleted Successfull";


            return response;
        }
    }
}
