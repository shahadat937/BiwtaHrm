using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Thana_Upazila.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Thana_Upazila.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Thana_Upazila.Handlers.Commands
{
    public class UpdateThana_UpazilaCommandHandler : IRequestHandler<UpdateThana_UpazilaCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.Thana_Upazila> _Thana_UpazilaRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateThana_UpazilaCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Thana_Upazila> Thana_UpazilaRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _Thana_UpazilaRepository = Thana_UpazilaRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateThana_UpazilaCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateThana_UpazilaDtoValidator();
            var validationResult = await validator.ValidateAsync(request.Thana_UpazilaDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var Thana_UpazilaName = request.Thana_UpazilaDto.Thana_UpazilaName.ToLower();

            IQueryable<Hrm.Domain.Thana_Upazila> Thana_Upazilaes = _Thana_UpazilaRepository.Where(x => x.Thana_UpazilaName.ToLower() == Thana_UpazilaName);



            if (Thana_Upazilaes.Any())
            {
                response.Success = false;
                response.Message = "Creation Failed Name already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }

            else
            {

                var Thana_Upazila = await _unitOfWork.Repository<Hrm.Domain.Thana_Upazila>().Get(request.Thana_UpazilaDto.Thana_UpazilaId);

                if (Thana_Upazila is null)
                {
                    throw new NotFoundException(nameof(Thana_Upazila), request.Thana_UpazilaDto.Thana_UpazilaId);
                }

                _mapper.Map(request.Thana_UpazilaDto, Thana_Upazila);

                await _unitOfWork.Repository<Hrm.Domain.Thana_Upazila>().Update(Thana_Upazila);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = Thana_Upazila.Thana_UpazilaId;

            }

            return response;
        }
    }
}
