using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.HairColor.Validators;
using Hrm.Application.DTOs.GradeType.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.HairColor.Requests.Commands;
using Hrm.Application.Features.GradeType.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.HairColor.Handlers.Commands
{
    public class UpdateHairColorCommandHandler : IRequestHandler<UpdateHairColorCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.HairColor> _HairColorRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateHairColorCommandHandler(IHrmRepository<Hrm.Domain.HairColor> HairColorRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _HairColorRepository = HairColorRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateHairColorCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateHairColorDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.HairColorDto);

            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            // var HairColorName = request.HairColorDto.HairColorName.ToLower();
            var HairColorName = request.HairColorDto.HairColorName.Trim().ToLower().Replace(" ", string.Empty);
            IQueryable<Hrm.Domain.HairColor> HairColores = _HairColorRepository.Where(x => x.HairColorName.ToLower() == HairColorName);

            if (HairColores.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed '{request.HairColorDto.HairColorName}' already exists.";

                //response.Message = "Creation Failed, Name already exists";
                response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            else
            {

                var HairColor = await _unitOfWork.Repository<Hrm.Domain.HairColor>().Get(request.HairColorDto.HairColorId);

                if (HairColor is null)
                {
                    throw new NotFoundException(nameof(HairColor), request.HairColorDto.HairColorId);
                }

                _mapper.Map(request.HairColorDto, HairColor);

                await _unitOfWork.Repository<Hrm.Domain.HairColor>().Update(HairColor);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = HairColor.HairColorId;

            }

            return response;
        }
    }
}

