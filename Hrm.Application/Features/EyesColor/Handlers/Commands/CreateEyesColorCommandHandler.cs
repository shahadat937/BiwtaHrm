using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EyesColor.Validators;
using Hrm.Application.Features.EyesColor.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.EyesColor.Handlers.Commands
{
    public class CreateEyesColorCommandHandler : IRequestHandler<CreateEyesColorCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.EyesColor> _EyesColorRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateEyesColorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.EyesColor> EyesColorRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _EyesColorRepository = EyesColorRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateEyesColorCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateEyesColorDtoValidator();
            var validationResult = await validator.ValidateAsync(request.EyesColorDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                //   var EyesColorName = request.EyesColorDto.EyesColorName.Trim().ToLower().Replace(" ", string.Empty);

                //  IQueryable<Hrm.Domain.EyesColor> EyesColors = _EyesColorRepository.Where(x => x.EyesColorName.ToLower().Replace(" ", string.Empty) == EyesColorName);


                if (EyesColorNameExists(request))
                {
                    response.Success = false;
                    response.Message = $"Creation Failed '{request.EyesColorDto.EyesColorName}' already exists.";
                    response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

                }
                else
                {
                    var EyesColor = _mapper.Map<Hrm.Domain.EyesColor>(request.EyesColorDto);

                    EyesColor = await _unitOfWork.Repository<Hrm.Domain.EyesColor>().Add(EyesColor);
                    await _unitOfWork.Save();


                    response.Success = true;
                    response.Message = "Creation Successful";
                    response.Id = EyesColor.EyesColorId;
                }
            }

            return response;
        }
        private bool EyesColorNameExists(CreateEyesColorCommand request)
        {
            var EyesColorName = request.EyesColorDto.EyesColorName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.EyesColor> EyesColors = _EyesColorRepository.Where(x => x.EyesColorName.Trim().ToLower().Replace(" ", string.Empty) == EyesColorName);

            return EyesColors.Any();
        }
    }
}
