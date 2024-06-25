using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Religion;
using Hrm.Application.DTOs.Religion.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Religion.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Religion.Handlers.Commands
{
    public class UpdateReligionCommandHandler : IRequestHandler<UpdateReligionCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.Religion> _religionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateReligionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Religion> religionRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _religionRepository = religionRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateReligionCommand request, CancellationToken cancellationToken)
        {
            var respose = new BaseCommandResponse();
            var validator = new UpdateReligionDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ReligionDto);

            if (validationResult.IsValid == false)
            {
                respose.Success = false;
                respose.Message = "Creation Failed";
                respose.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            var ReligionName = request.ReligionDto.ReligionName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.Religion> maritalStatuses = _religionRepository.Where(x => x.ReligionName.ToLower() == ReligionName);
            if (maritalStatuses.Any())
            {
                respose.Success = false;

                respose.Message = $"Update Failed '{request.ReligionDto.ReligionName}' already exists.";
                respose.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }
            else
            {
                var Religion = await _unitOfWork.Repository<Hrm.Domain.Religion>().Get(request.ReligionDto.ReligionId);

                if (Religion is null)
                {
                    throw new NotFoundException(nameof(Religion), request.ReligionDto.ReligionId);
                }

                _mapper.Map(request.ReligionDto, Religion);

                await _unitOfWork.Repository<Hrm.Domain.Religion>().Update(Religion);
                await _unitOfWork.Save();
                respose.Success = true;
                respose.Message = "Update Successfull";
                respose.Id = Religion.ReligionId;

            }
            return respose;
        }
    }
}
