using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Features.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Features.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Features.Handlers.Commands
{
    public class UpdateFeatureCommandHandler : IRequestHandler<UpdateFeatureCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateFeatureCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateFeatureCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var Feature = await _unitOfWork.Repository<Hrm.Domain.Feature>().Get(request.FeatureDto.FeatureId);

            if (Feature is null)
                throw new NotFoundException(nameof(Feature), request.FeatureDto.FeatureId);

            _mapper.Map(request.FeatureDto, Feature);

            await _unitOfWork.Repository<Hrm.Domain.Feature>().Update(Feature);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Update Successful";
            response.Id = Feature.FeatureId;

            return response;
        }
    }
}