using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Occupation.Validators;
using Hrm.Application.Features.Occupation.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.Occupation.Handlers.Commands
{
    public class CreateOccupationCommandHandler : IRequestHandler<CreateBloodCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.Occupation> _OccupationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateOccupationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Occupation> OccupationRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _OccupationRepository = OccupationRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateBloodCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateOccupationDtoValidator();
            var validationResult = await validator.ValidateAsync(request.OccupationDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
             //   var OccupationName = request.OccupationDto.OccupationName.Trim().ToLower().Replace(" ", string.Empty);

              //  IQueryable<Hrm.Domain.Occupation> Occupations = _OccupationRepository.Where(x => x.OccupationName.ToLower().Replace(" ", string.Empty) == OccupationName);


                if (OccupationNameExists(request))
                {
                    response.Success = false;
                    response.Message = $"Creation Failed '{request.OccupationDto.OccupationName}' already exists.";
                    response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
                    
                }
                else
                {
                    var Occupation = _mapper.Map<Hrm.Domain.Occupation>(request.OccupationDto);

                    Occupation = await _unitOfWork.Repository<Hrm.Domain.Occupation>().Add(Occupation);
                    await _unitOfWork.Save();


                    response.Success = true;
                    response.Message = "Creation Successful";
                    response.Id = Occupation.OccupationId;
                }
            }

            return response;
        }
        private bool OccupationNameExists(CreateBloodCommand request)
        {
            var OccupationName = request.OccupationDto.OccupationName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable <Hrm.Domain.Occupation > Occupations = _OccupationRepository.Where(x => x.OccupationName.Trim().ToLower().Replace(" ", string.Empty) == OccupationName);

             return Occupations.Any();
        }
    }
}
