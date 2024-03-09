using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.ChildStatus.Validators;
using Hrm.Application.Features.ChildStatus.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;


namespace Hrm.Application.Features.ChildStatus.Handlers.Commands
{
    public class CreateChildStatusCommandHandler : IRequestHandler<CreateChildStatusCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateChildStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateChildStatusCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateChildStatusDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ChildStatusDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var ChildStatus = _mapper.Map<Hrm.Domain.ChildStatus>(request.ChildStatusDto);

                ChildStatus = await _unitOfWork.Repository<Hrm.Domain.ChildStatus>().Add(ChildStatus);
                await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = ChildStatus.ChildStatusId;
            }

            return response;
        }

    }
}
