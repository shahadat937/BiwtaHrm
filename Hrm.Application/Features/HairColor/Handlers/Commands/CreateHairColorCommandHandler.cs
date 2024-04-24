using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.HairColor.Validators;
using Hrm.Application.Features.HairColor.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.HairColor.Handlers.Commands
{
    public class CreateHairColorCommandHandler : IRequestHandler<CreateHairColorCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.HairColor> _HairColorRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateHairColorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.HairColor> HairColorRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _HairColorRepository = HairColorRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateHairColorCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateHairColorDtoValidator();
            var validationResult = await validator.ValidateAsync(request.HairColorDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                //   var HairColorName = request.HairColorDto.HairColorName.Trim().ToLower().Replace(" ", string.Empty);

                //  IQueryable<Hrm.Domain.HairColor> HairColors = _HairColorRepository.Where(x => x.HairColorName.ToLower().Replace(" ", string.Empty) == HairColorName);


                if (HairColorNameExists(request))
                {
                    response.Success = false;
                    response.Message = $"Creation Failed '{request.HairColorDto.HairColorName}' already exists.";
                    response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

                }
                else
                {
                    var HairColor = _mapper.Map<Hrm.Domain.HairColor>(request.HairColorDto);

                    HairColor = await _unitOfWork.Repository<Hrm.Domain.HairColor>().Add(HairColor);
                    await _unitOfWork.Save();


                    response.Success = true;
                    response.Message = "Creation Successful";
                    response.Id = HairColor.HairColorId;
                }
            }

            return response;
        }
        private bool HairColorNameExists(CreateHairColorCommand request)
        {
            var HairColorName = request.HairColorDto.HairColorName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.HairColor> HairColors = _HairColorRepository.Where(x => x.HairColorName.Trim().ToLower().Replace(" ", string.Empty) == HairColorName);

            return HairColors.Any();
        }
    }
}
