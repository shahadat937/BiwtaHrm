using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Competence.Validators;
using Hrm.Application.Features.Competence.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Microsoft.EntityFrameworkCore;
using Hrm.Application.DTOs.CompetenceCompetence.Validators;

namespace Hrm.Application.Features.Competence.Handlers.Commands
{
    public class CreateCompetenceCommandHandler : IRequestHandler<CreateCompetenceCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.Competence> _competenceRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateCompetenceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Competence> competenceRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _competenceRepository = competenceRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateCompetenceCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateCompetenceDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CompetenceDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                //   var competenceName = request.CompetenceDto.CompetenceName.Trim().ToLower().Replace(" ", string.Empty);

                //  IQueryable<Hrm.Domain.Competence> competences = _competenceRepository.Where(x => x.CompetenceName.ToLower().Replace(" ", string.Empty) == competenceName);


                if (CompetenceNameExists(request))
                {
                    response.Success = false;
                    response.Message = $"Creation Failed '{request.CompetenceDto.CompetenceName}' already exists.";
                    response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

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
            }

            return response;
        }
        private bool CompetenceNameExists(CreateCompetenceCommand request)
        {
            var competenceName = request.CompetenceDto.CompetenceName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.Competence> competences = _competenceRepository.Where(x => x.CompetenceName.Trim().ToLower().Replace(" ", string.Empty) == competenceName);

            return competences.Any();
        }
    }
}
