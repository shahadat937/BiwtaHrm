using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Country.Validators;
using Hrm.Application.DTOs.Country.Validators;
using Hrm.Application.DTOs.TrainingType.Validators;
using Hrm.Application.Features.Country.Requests.Commands;
using Hrm.Application.Features.TrainingType.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Country.Handlers.Commands
{
    public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.Country> _CountryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateCountryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Country> CountryRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _CountryRepository = CountryRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateCountryDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CountryDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                //   var CountryName = request.CountryDto.CountryName.Trim().ToLower().Replace(" ", string.Empty);

                //  IQueryable<Hrm.Domain.Country> Countrys = _CountryRepository.Where(x => x.CountryName.ToLower().Replace(" ", string.Empty) == CountryName);


                if (CountryNameExists(request))
                {
                    response.Success = false;
                    response.Message = $"Creation Failed '{request.CountryDto.CountryName}' already exists.";
                    response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

                }
                else
                {
                    var Country = _mapper.Map<Hrm.Domain.Country>(request.CountryDto);

                    Country = await _unitOfWork.Repository<Hrm.Domain.Country>().Add(Country);
                    await _unitOfWork.Save();


                    response.Success = true;
                    response.Message = "Creation Successful";
                    response.Id = Country.CountryId;
                }
            }

            return response;
        }
        private bool CountryNameExists(CreateCountryCommand request)
        {
            var CountryName = request.CountryDto.CountryName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.Country> Countrys = _CountryRepository.Where(x => x.CountryName.Trim().ToLower().Replace(" ", string.Empty) == CountryName);

            return Countrys.Any();
        }
    }
}
