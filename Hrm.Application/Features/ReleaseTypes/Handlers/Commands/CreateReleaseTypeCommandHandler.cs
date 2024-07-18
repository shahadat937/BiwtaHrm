using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.ReleaseTypes.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;

namespace Hrm.Application.Features.ReleaseTypes.Handlers.Commands
{
    public class CreateReleaseTypeCommandHandler : IRequestHandler<CreateReleaseTypeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateReleaseTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateReleaseTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var ReleaseType = _mapper.Map<Hrm.Domain.ReleaseType>(request.ReleaseTypeDto);

            ReleaseType = await _unitOfWork.Repository<Hrm.Domain.ReleaseType>().Add(ReleaseType);
            await _unitOfWork.Save();


            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = ReleaseType.ReleaseTypeId;

            return response;
        }

    }
}
