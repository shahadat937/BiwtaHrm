using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Gender.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Gender.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Gender.Handlers.Commands
{
    public class UpdateGenderCommandHandler : IRequestHandler<UpdateGenderCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.Gender> _genderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateGenderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Domain.Gender> genderRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _genderRepository = genderRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateGenderCommand request, CancellationToken cancellationToken)
        {
            var respose = new BaseCommandResponse();
            var validator = new UpdateGenderDtoValidator();
            var validationResult = await validator.ValidateAsync(request.GenderDto);

            if (validationResult.IsValid == false)
            {
                respose.Success = false;
                respose.Message = "Creation Failed";
                respose.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            
            var Gender = await _unitOfWork.Repository<Hrm.Domain.Gender>().Get(request.GenderDto.GenderId);
            if (Gender is null)
            {
                throw new NotFoundException(nameof(Gender), request.GenderDto.GenderId);
            }
            var normalizedGenderName = request.GenderDto.GenderName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.Gender> matchingGender = _genderRepository.Where(x => x.GenderName.ToLower() == normalizedGenderName);
            if (matchingGender.Any())
            {
                respose.Success = false;
                respose.Message = $"Update Failed '{request.GenderDto.GenderName}' already exists.";
                respose.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }
            else
            {
                _mapper.Map(request.GenderDto, Gender);

                await _unitOfWork.Repository<Hrm.Domain.Gender>().Update(Gender);
                await _unitOfWork.Save();
                respose.Success = true;
                respose.Message = "Update Successful";
                respose.Id = Gender.GenderId;
            }
                

            return respose;
        }
    }
}
