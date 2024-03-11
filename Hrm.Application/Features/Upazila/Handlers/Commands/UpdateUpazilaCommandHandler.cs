using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Upazila.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Upazila.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Upazila.Handlers.Commands
{
    public class UpdateUpazilaCommandHandler : IRequestHandler<UpdateUpazilaCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.Upazila> _UpazilaRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateUpazilaCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Upazila> UpazilaRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _UpazilaRepository = UpazilaRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateUpazilaCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateUpazilaDtoValidator();
            var validationResult = await validator.ValidateAsync(request.UpazilaDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var UpazilaName = request.UpazilaDto.UpazilaName.ToLower();

            IQueryable<Hrm.Domain.Upazila> Upazilaes = _UpazilaRepository.Where(x => x.UpazilaName.ToLower() == UpazilaName);



            if (Upazilaes.Any())
            {
                response.Success = false;
                response.Message = "Creation Failed Name already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }

            else
            {

                var Upazila = await _unitOfWork.Repository<Hrm.Domain.Upazila>().Get(request.UpazilaDto.UpazilaId);

                if (Upazila is null)
                {
                    throw new NotFoundException(nameof(Upazila), request.UpazilaDto.UpazilaId);
                }

                _mapper.Map(request.UpazilaDto, Upazila);

                await _unitOfWork.Repository<Hrm.Domain.Upazila>().Update(Upazila);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = Upazila.UpazilaId;

            }

            return response;
        }
    }
}
