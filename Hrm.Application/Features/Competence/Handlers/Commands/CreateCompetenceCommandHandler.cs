using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Competence.Validators;
using Hrm.Application.DTOs.CompetenceCompetence.Validators;
using Hrm.Application.Features.Competence.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Competence.Handlers.Commands
{
    public class CreateCompetenceCommandHandler : IRequestHandler<CreateCompetenceCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateCompetenceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateCompetenceCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateCompetenceDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CompetenceDto);
            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Faild";
                response.Errors=validationResult.Errors.Select(q=>q.ErrorMessage).ToList();
            }
            else
            {
                var Competence = _mapper.Map<Hrm.Domain.Competence>(request.CompetenceDto);
                Competence = await _unitOfWork.Repository<Hrm.Domain.Competence>().Add(Competence);
                await _unitOfWork.Save();
                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = Competence.CompetenceId;
            }
            return response;
        }
    }
}
