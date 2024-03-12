using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Thana.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Thana.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Thana.Handlers.Commands
{
    public class UpdateThanaCommandHandler : IRequestHandler<UpdateThanaCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.Thana> _ThanaRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateThanaCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Thana> ThanaRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _ThanaRepository = ThanaRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateThanaCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateThanaDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ThanaDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var ThanaName = request.ThanaDto.ThanaName.ToLower();

            IQueryable<Hrm.Domain.Thana> Thanaes = _ThanaRepository.Where(x => x.ThanaName.ToLower() == ThanaName);



            if (Thanaes.Any())
            {
                response.Success = false;
                response.Message = "Creation Failed Name already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }

            else
            {

                var Thana = await _unitOfWork.Repository<Hrm.Domain.Thana>().Get(request.ThanaDto.ThanaId);

                if (Thana is null)
                {
                    throw new NotFoundException(nameof(Thana), request.ThanaDto.ThanaId);
                }

                _mapper.Map(request.ThanaDto, Thana);

                await _unitOfWork.Repository<Hrm.Domain.Thana>().Update(Thana);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = Thana.ThanaId;

            }

            return response;
        }
    }
}
