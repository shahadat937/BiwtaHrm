using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Gender.Validators;
using Hrm.Application.Features.Gender.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;


namespace Hrm.Application.Features.Gender.Handlers.Commands
{
    public class CreateGenderCommandHandler : IRequestHandler<CreateGenderCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateGenderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateGenderCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateGenderDtoValidator();
            var validationResult = await validator.ValidateAsync(request.GenderDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var Gender = _mapper.Map<Hrm.Domain.Gender>(request.GenderDto);

                Gender = await _unitOfWork.Repository<Hrm.Domain.Gender>().Add(Gender);
                await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = Gender.GenderId;
            }

            return response;
        }

    }
}
