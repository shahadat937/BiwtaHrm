using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Institute.Validators;
using Hrm.Application.Exceptions;
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
    public class UpdateInstituteCommandHandler : IRequestHandler<UpdateInstituteCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.Institute> _instituteRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateInstituteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Institute> instituteRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _instituteRepository = instituteRepository;
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

            var Institute = await _unitOfWork.Repository<Hrm.Domain.Institute>().Get(request.InstituteDto.InstituteId);

            if (Institute is null)
            {
                throw new NotFoundException(nameof(Institute), request.InstituteDto.InstituteId);
            }

            var instituteName = request.InstituteDto.InstituteName.ToLower();

            IQueryable<Hrm.Domain.Institute> institutes = _instituteRepository.Where(x => x.InstituteName.ToLower() == instituteName);


            if (institutes.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed '{request.InstituteDto.InstituteName}' already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }
            else
            {

                _mapper.Map(request.InstituteDto, Institute);

                await _unitOfWork.Repository<Hrm.Domain.Institute>().Update(Institute);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successful";
                response.Id = Institute.InstituteId;

            }
            return response;
        }
    }
}
