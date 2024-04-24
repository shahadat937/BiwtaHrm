using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EyesColor.Validators;
using Hrm.Application.DTOs.GradeType.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.EyesColor.Requests.Commands;
using Hrm.Application.Features.GradeType.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EyesColor.Handlers.Commands
{
    public class UpdateEyesColorCommandHandler : IRequestHandler<UpdateEyesColorCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.EyesColor> _EyesColorRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEyesColorCommandHandler(IHrmRepository<Hrm.Domain.EyesColor> EyesColorRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _EyesColorRepository = EyesColorRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateEyesColorCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateEyesColorDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.EyesColorDto);

            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            //var EyesColorName = request.EyesColorDto.EyesColorName.ToLower();

            var EyesColorName = request.EyesColorDto.EyesColorName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.EyesColor> EyesColores = _EyesColorRepository.Where(x => x.EyesColorName.ToLower() == EyesColorName);

            if (EyesColores.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed '{request.EyesColorDto.EyesColorName}' already exists.";

                //response.Message = "Creation Failed, Name already exists";
                response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            else
            {

                var EyesColor = await _unitOfWork.Repository<Hrm.Domain.EyesColor>().Get(request.EyesColorDto.EyesColorId);

                if (EyesColor is null)
                {
                    throw new NotFoundException(nameof(EyesColor), request.EyesColorDto.EyesColorId);
                }

                _mapper.Map(request.EyesColorDto, EyesColor);

                await _unitOfWork.Repository<Hrm.Domain.EyesColor>().Update(EyesColor);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = EyesColor.EyesColorId;

            }

            return response;
        }
    }
}

