using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Relation.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Relation.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Relation.Handlers.Commands
{
    public class UpdateRelationCommandHandler : IRequestHandler<UpdateRelationCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.Relation> _RelationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateRelationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Relation> RelationRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _RelationRepository = RelationRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateRelationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateRelationDtoValidator();
            var validationResult = await validator.ValidateAsync(request.RelationDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var Relation = await _unitOfWork.Repository<Hrm.Domain.Relation>().Get(request.RelationDto.RelationId);

            if (Relation is null)
            {
                throw new NotFoundException(nameof(Relation), request.RelationDto.RelationId);
            }

            var RelationName = request.RelationDto.RelationName.ToLower();

            IQueryable<Hrm.Domain.Relation> Relations = _RelationRepository.Where(x => x.RelationName.ToLower() == RelationName);


            if (Relations.Any())
            {
                response.Success = false;
                response.Message = "Creation Failed Name already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }
            else
            {

                


                if (RelationNameExists(request))
                {
                    response.Success = false;
                    response.Message = "Update Failed Relation Name already exists.";
                    response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

                }
                else
                {
                    _mapper.Map(request.RelationDto, Relation);

                    await _unitOfWork.Repository<Hrm.Domain.Relation>().Update(Relation);
                    await _unitOfWork.Save();

                    response.Success = true;
                    response.Message = "Update Successful";
                    response.Id = Relation.RelationId;
                }

            }
            return response;
        }

        private bool RelationNameExists(UpdateRelationCommand request)
        {
            var RelationName = request.RelationDto.RelationName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.Relation> Relations = _RelationRepository.Where(x => x.RelationName.Trim().ToLower().Replace(" ", string.Empty) == RelationName);

            return Relations.Any();
        }

    }
}
