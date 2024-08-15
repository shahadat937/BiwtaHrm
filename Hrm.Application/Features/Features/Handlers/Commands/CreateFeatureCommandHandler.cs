using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Features.Validators;
using Hrm.Application.Features.Features.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Features.Handlers.Commands
{
    public class CreateFeatureCommandHandler : IRequestHandler<CreateFeatureCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateFeatureCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateFeatureCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var Feature = _mapper.Map<Hrm.Domain.Feature>(request.FeatureDto);

            Feature = await _unitOfWork.Repository<Hrm.Domain.Feature>().Add(Feature);
            await _unitOfWork.Save();
            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = Feature.FeatureId;

            return response;
        }
    }
}
