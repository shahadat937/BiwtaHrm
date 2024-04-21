using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Institute.Validators;
using Hrm.Application.Features.Institute.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.Institute.Handlers.Commands
{
    public class CreateInstituteCommandHandler : IRequestHandler<CreateInstituteCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.Institute> _instituteRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateInstituteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Institute> instituteRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _instituteRepository = instituteRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateInstituteCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateInstituteDtoValidator();
            var validationResult = await validator.ValidateAsync(request.InstituteDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                //   var instituteName = request.InstituteDto.InstituteName.Trim().ToLower().Replace(" ", string.Empty);

                //  IQueryable<Hrm.Domain.Institute> institutes = _instituteRepository.Where(x => x.InstituteName.ToLower().Replace(" ", string.Empty) == instituteName);


                if (InstituteNameExists(request))
                {
                    response.Success = false;
                    response.Message = $"Creation Failed '{request.InstituteDto.InstituteName}' already exists.";
                    response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

                }
                else
                {
                    var Institute = _mapper.Map<Hrm.Domain.Institute>(request.InstituteDto);

                    Institute = await _unitOfWork.Repository<Hrm.Domain.Institute>().Add(Institute);
                    await _unitOfWork.Save();


                    response.Success = true;
                    response.Message = "Creation Successful";
                    response.Id = Institute.InstituteId;
                }
            }

            return response;
        }
        private bool InstituteNameExists(CreateInstituteCommand request)
        {
            var instituteName = request.InstituteDto.InstituteName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.Institute> institutes = _instituteRepository.Where(x => x.InstituteName.Trim().ToLower().Replace(" ", string.Empty) == instituteName);

            return institutes.Any();
        }
    }
}
