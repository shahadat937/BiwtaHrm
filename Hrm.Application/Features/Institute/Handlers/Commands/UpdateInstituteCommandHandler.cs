using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Institute.Validators; 
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Institute.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Institute.Handlers.Commands
{
    public class UpdateInstituteCommandHandler : IRequestHandler<UpdateInstituteCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.Institute> _InstituteRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateInstituteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Institute> InstituteRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _InstituteRepository = InstituteRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateInstituteCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateInstituteDtoValidators();
            var validationResult = await validator.ValidateAsync(request.InstituteDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var InstituteName = request.InstituteDto.InstituteName.ToLower();

            IQueryable<Hrm.Domain.Institute> Institutees = _InstituteRepository.Where(x => x.InstituteName.ToLower() == InstituteName);



            if (Institutees.Any())
            {
                response.Success = false;
                response.Message = "Creation Failed Name already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }

            else
            {

                var Institute = await _unitOfWork.Repository<Hrm.Domain.Institute>().Get(request.InstituteDto.InstituteId);

                if (Institute is null)
                {
                    throw new NotFoundException(nameof(Institute), request.InstituteDto.InstituteId);
                }

                _mapper.Map(request.InstituteDto, Institute);

                await _unitOfWork.Repository<Hrm.Domain.Institute>().Update(Institute);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = Institute.InstituteId;

            }

            return response;
        }
    }
}
