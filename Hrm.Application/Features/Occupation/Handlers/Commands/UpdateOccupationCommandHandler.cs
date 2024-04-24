using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Occupation.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Occupation.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Occupation.Handlers.Commands
{
    public class UpdateOccupationCommandHandler : IRequestHandler<UpdateOccupationCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.Occupation> _OccupationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateOccupationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Occupation> OccupationRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _OccupationRepository = OccupationRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateOccupationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateOccupationDtoValidators();
            var validationResult = await validator.ValidateAsync(request.OccupationDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var Occupation = await _unitOfWork.Repository<Hrm.Domain.Occupation>().Get(request.OccupationDto.OccupationId);

            if (Occupation is null)
            {
                throw new NotFoundException(nameof(Occupation), request.OccupationDto.OccupationId);
            }

            //var OccupationName = request.OccupationDto.OccupationName.ToLower();
            var OccupationName = request.OccupationDto.OccupationName.Trim().ToLower().Replace(" ", string.Empty);
            IQueryable<Hrm.Domain.Occupation> Occupations = _OccupationRepository.Where(x => x.OccupationName.ToLower() == OccupationName);


            if (Occupations.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed '{request.OccupationDto.OccupationName}' already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }
            else
            {

                _mapper.Map(request.OccupationDto, Occupation);

                await _unitOfWork.Repository<Hrm.Domain.Occupation>().Update(Occupation);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successful";
                response.Id = Occupation.OccupationId;

            }
            return response;
        }
    }
}
