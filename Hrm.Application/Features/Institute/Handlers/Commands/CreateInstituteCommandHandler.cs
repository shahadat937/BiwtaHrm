using AutoMapper;
using FluentValidation;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.BloodGroup.Validators;
using Hrm.Application.DTOs.Institute.Validators;
using Hrm.Application.Features.Institute.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Institute.Handlers.Commands
{
    public class CreateInstituteCommandHandler : IRequestHandler<CreateInstituteCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.Institute> _InstituteRepository; 
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateInstituteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Institute> InstituteRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _InstituteRepository = InstituteRepository;
        }


        public async Task<BaseCommandResponse> Handle(CreateInstituteCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateInstituteDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.InstituteDto);

            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validatorResult.Errors.Select(x=> x.ErrorMessage).ToList();
            }
            else
            {

                var InstituteName = request.InstituteDto.InstituteName.ToLower();

                IQueryable<Hrm.Domain.Institute> Institutees = _InstituteRepository.Where(x => x.InstituteName.ToLower() == InstituteName);

                

                if (Institutees.Any())
                {
                    response.Success = false;
                    response.Message = "Creation Failed Name already exists.";
                    response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();

                }

                else
                {
                    var Institute = _mapper.Map<Hrm.Domain.Institute>(request.InstituteDto);

                    Institute = await _unitOfWork.Repository<Hrm.Domain.Institute>().Add(Institute);
                    await _unitOfWork.Save();

                    response.Success = true;
                    response.Message = "Creation Successfull";
                    response.Id = Institute.InstituteId;
                }
            }

            return response;
        }
    }
}
