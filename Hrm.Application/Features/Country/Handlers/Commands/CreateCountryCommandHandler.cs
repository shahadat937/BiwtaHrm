using AutoMapper;
using Hrm.Application.Contracts.Persistence;
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

        private readonly IHrmRepository<Hrm.Domain.Country> _countryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCountryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Country> countryRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _countryRepository = countryRepository;
        }


        public async Task<BaseCommandResponse> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateCountryDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.CountryDto);

            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            else
            {

                var countryName = request.CountryDto.CountryName.ToLower();

                IQueryable<Hrm.Domain.Country> countries = _countryRepository.Where(x => x.CountryName.ToLower() == countryName);



                if (countries.Any())
                {
                    response.Success = false;
                    response.Message = "Creation Failed Name already exists.";
                    response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();

                }

                else
                {
                    var Country = _mapper.Map<Hrm.Domain.Country>(request.CountryDto);

                    Country = await _unitOfWork.Repository<Hrm.Domain.Country>().Add(Country);
                    await _unitOfWork.Save();

                    response.Success = true;
                    response.Message = "Creation Successfull";
                    response.Id = Country.CountryId;
                }
            }

            return response;
        }
    }
}
