using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Features.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Features.Handlers.Commands
{
    public class DeleteFeatureCommandHandler : IRequestHandler<DeleteFeatureCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteFeatureCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteFeatureCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var Feature = await _unitOfWork.Repository<Hrm.Domain.Feature>().Get(request.Id);

            if (Feature == null)
                throw new NotFoundException(nameof(Feature), request.Id);

            await _unitOfWork.Repository<Hrm.Domain.Feature>().Delete(Feature);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successful";
            response.Id = Feature.FeatureId;

            return response;
        }
    }
}