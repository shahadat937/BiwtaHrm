using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Relation.Validators;
using Hrm.Application.Features.Relation.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.Relation.Handlers.Commands
{
    public class CreateRelationCommandHandler : IRequestHandler<CreateRelationCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.Relation> _RelationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateRelationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Relation> RelationRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _RelationRepository = RelationRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateRelationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateRelationDtoValidator();
            var validationResult = await validator.ValidateAsync(request.RelationDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
             //   var RelationName = request.RelationDto.RelationName.Trim().ToLower().Replace(" ", string.Empty);

              //  IQueryable<Hrm.Domain.Relation> Relations = _RelationRepository.Where(x => x.RelationName.ToLower().Replace(" ", string.Empty) == RelationName);


                if (RelationNameExists(request))
                {
                    response.Success = false;
                    response.Message = "Creation Failed Relation Name already exists.";
                    response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
                    
                }
                else
                {
                    var Relation = _mapper.Map<Hrm.Domain.Relation>(request.RelationDto);

                    Relation = await _unitOfWork.Repository<Hrm.Domain.Relation>().Add(Relation);
                    await _unitOfWork.Save();


                    response.Success = true;
                    response.Message = "Creation Successful";
                    response.Id = Relation.RelationId;
                }
            }

            return response;
        }
        private bool RelationNameExists(CreateRelationCommand request)
        {
            var RelationName = request.RelationDto.RelationName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable <Hrm.Domain.Relation > Relations = _RelationRepository.Where(x => x.RelationName.Trim().ToLower().Replace(" ", string.Empty) == RelationName);

             return Relations.Any();
        }
    }
}
