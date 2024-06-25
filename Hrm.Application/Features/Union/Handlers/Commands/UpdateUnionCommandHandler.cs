using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Union.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Union.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Union.Handlers.Commands
{
    public class UpdateUnionCommandHandler : IRequestHandler<UpdateUnionCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.Union> _UnionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateUnionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Union> UnionRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _UnionRepository = UnionRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateUnionCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateUnionDtoValidator();
            var validationResult = await validator.ValidateAsync(request.UnionDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var UnionName = request.UnionDto.UnionName.ToLower();

            IQueryable<Hrm.Domain.Union> Uniones = _UnionRepository.Where(x => x.UnionName.ToLower() == UnionName);



            if (Uniones.Any())
            {
                response.Success = false;
                response.Message = "Creation Failed Name already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }

            else
            {

                var Union = await _unitOfWork.Repository<Hrm.Domain.Union>().Get(request.UnionDto.UnionId);

                if (Union is null)
                {
                    throw new NotFoundException(nameof(Union), request.UnionDto.UnionId);
                }

                _mapper.Map(request.UnionDto, Union);

                await _unitOfWork.Repository<Hrm.Domain.Union>().Update(Union);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = Union.UnionId;

            }

            return response;
        }
    }
}
