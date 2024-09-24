using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.ResponsibilityType.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.ResponsibilityType.Handlers.Commands
{
    public class CreateResponsibilityTypeCommandHandler : IRequestHandler<CreateResponsibilityTypeCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.ResponsibilityType> _ResponsibilityTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateResponsibilityTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.ResponsibilityType> ResponsibilityTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _ResponsibilityTypeRepository = ResponsibilityTypeRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateResponsibilityTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();



            if (ResponsibilityTypeNameExists(request))
            {
                response.Success = false;
                response.Message = $"Creation Failed '{request.ResponsibilityTypeDto.Name}' already exists.";

            }
            else
            {
                var ResponsibilityType = _mapper.Map<Hrm.Domain.ResponsibilityType>(request.ResponsibilityTypeDto);

                ResponsibilityType = await _unitOfWork.Repository<Hrm.Domain.ResponsibilityType>().Add(ResponsibilityType);
                await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = ResponsibilityType.Id;
            }

            return response;
        }
        private bool ResponsibilityTypeNameExists(CreateResponsibilityTypeCommand request)
        {
            var ResponsibilityTypeName = request.ResponsibilityTypeDto.Name.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.ResponsibilityType> ResponsibilityTypes = _ResponsibilityTypeRepository.Where(x => x.Name.Trim().ToLower().Replace(" ", string.Empty) == ResponsibilityTypeName);

            return ResponsibilityTypes.Any();
        }
    }
}
