using AutoMapper;
using FluentValidation;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.BloodGroup.Validators;
using Hrm.Application.DTOs.Upazila.Validators;
using Hrm.Application.Features.Upazila.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Upazila.Handlers.Commands
{
    public class CreateUpazilaCommandHandler : IRequestHandler<CreateUpazilaCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.Upazila> _UpazilaRepository; 
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateUpazilaCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Upazila> UpazilaRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _UpazilaRepository = UpazilaRepository;
        }


        public async Task<BaseCommandResponse> Handle(CreateUpazilaCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateUpazilaDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.UpazilaDto);

            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validatorResult.Errors.Select(x=> x.ErrorMessage).ToList();
            }
            else
            {

                var UpazilaName = request.UpazilaDto.UpazilaName.ToLower();

                IQueryable<Hrm.Domain.Upazila> Upazilaes = _UpazilaRepository.Where(x => x.UpazilaName.ToLower() == UpazilaName);

                

                if (Upazilaes.Any())
                {
                    response.Success = false;
                    response.Message = "Creation Failed Name already exists.";
                    response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();

                }

                else
                {
                    var Upazila = _mapper.Map<Hrm.Domain.Upazila>(request.UpazilaDto);

                    Upazila = await _unitOfWork.Repository<Hrm.Domain.Upazila>().Add(Upazila);
                    await _unitOfWork.Save();

                    response.Success = true;
                    response.Message = "Creation Successfull";
                    response.Id = Upazila.UpazilaId;
                }
            }

            return response;
        }
    }
}
