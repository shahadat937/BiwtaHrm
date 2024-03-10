using AutoMapper;
using FluentValidation;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.BloodGroup.Validators;
using Hrm.Application.DTOs.Thana_Upazila.Validators;
using Hrm.Application.Features.Thana_Upazila.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Thana_Upazila.Handlers.Commands
{
    public class CreateThana_UpazilaCommandHandler : IRequestHandler<CreateThana_UpazilaCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.Thana_Upazila> _Thana_UpazilaRepository; 
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateThana_UpazilaCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Thana_Upazila> Thana_UpazilaRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _Thana_UpazilaRepository = Thana_UpazilaRepository;
        }


        public async Task<BaseCommandResponse> Handle(CreateThana_UpazilaCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateThana_UpazilaDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.Thana_UpazilaDto);

            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validatorResult.Errors.Select(x=> x.ErrorMessage).ToList();
            }
            else
            {

                var Thana_UpazilaName = request.Thana_UpazilaDto.Thana_UpazilaName.ToLower();

                IQueryable<Hrm.Domain.Thana_Upazila> Thana_Upazilaes = _Thana_UpazilaRepository.Where(x => x.Thana_UpazilaName.ToLower() == Thana_UpazilaName);

                

                if (Thana_Upazilaes.Any())
                {
                    response.Success = false;
                    response.Message = "Creation Failed Name already exists.";
                    response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();

                }

                else
                {
                    var Thana_Upazila = _mapper.Map<Hrm.Domain.Thana_Upazila>(request.Thana_UpazilaDto);

                    Thana_Upazila = await _unitOfWork.Repository<Hrm.Domain.Thana_Upazila>().Add(Thana_Upazila);
                    await _unitOfWork.Save();

                    response.Success = true;
                    response.Message = "Creation Successfull";
                    response.Id = Thana_Upazila.Thana_UpazilaId;
                }
            }

            return response;
        }
    }
}
