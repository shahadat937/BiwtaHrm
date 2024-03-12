using AutoMapper;
using FluentValidation;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.BloodGroup.Validators;
using Hrm.Application.DTOs.Union.Validators;
using Hrm.Application.Features.Union.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Union.Handlers.Commands
{
    public class CreateUnionCommandHandler : IRequestHandler<CreateUnionCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.Union> _UnionRepository; 
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateUnionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Union> UnionRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _UnionRepository = UnionRepository;
        }


        public async Task<BaseCommandResponse> Handle(CreateUnionCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateUnionDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.UnionDto);

            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validatorResult.Errors.Select(x=> x.ErrorMessage).ToList();
            }
            else
            {

                var UnionName = request.UnionDto.UnionName.ToLower();

                IQueryable<Hrm.Domain.Union> Uniones = _UnionRepository.Where(x => x.UnionName.ToLower() == UnionName);

                

                if (Uniones.Any())
                {
                    response.Success = false;
                    response.Message = "Creation Failed Name already exists.";
                    response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();

                }

                else
                {
                    var Union = _mapper.Map<Hrm.Domain.Union>(request.UnionDto);

                    Union = await _unitOfWork.Repository<Hrm.Domain.Union>().Add(Union);
                    await _unitOfWork.Save();

                    response.Success = true;
                    response.Message = "Creation Successfull";
                    response.Id = Union.UnionId;
                }
            }

            return response;
        }
    }
}
