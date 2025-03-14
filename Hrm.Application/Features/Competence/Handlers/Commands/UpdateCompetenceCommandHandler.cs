﻿using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Competence.Validators;
using Hrm.Application.DTOs.Competence.ValidatorsCompetence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Competence.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Competence.Handlers.Commands
{
    public class UpdateCompetenceCommandHandler : IRequestHandler<UpdateCompetenceCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.Competence> _competenceRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateCompetenceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Competence> competenceRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _competenceRepository = competenceRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateCompetenceCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateCompetenceDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CompetenceDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var Competence = await _unitOfWork.Repository<Hrm.Domain.Competence>().Get(request.CompetenceDto.CompetenceId);

            if (Competence is null)
            {
                throw new NotFoundException(nameof(Competence), request.CompetenceDto.CompetenceId);
            }

            var competenceName = request.CompetenceDto.CompetenceName.ToLower();

            IQueryable<Hrm.Domain.Competence> competences = _competenceRepository.Where(x => x.CompetenceName.ToLower() == competenceName);


            if (competences.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed '{request.CompetenceDto.CompetenceName}' already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }
            else
            {

                _mapper.Map(request.CompetenceDto, Competence);

                await _unitOfWork.Repository<Hrm.Domain.Competence>().Update(Competence);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successful";
                response.Id = Competence.CompetenceId;

            }
            return response;
        }
    }
}
