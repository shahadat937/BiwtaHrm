using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Country.Validators;
using Hrm.Application.DTOs.TrainingType.Validators;
using Hrm.Application.Exceptions;
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
    public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.Country> _countryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCountryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Country> countryRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _countryRepository = countryRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateCountryDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CountryDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var countryName = request.CountryDto.CountryName.ToLower();

            IQueryable<Hrm.Domain.Country> countries = _countryRepository.Where(x => x.CountryName.ToLower() == countryName);



            if (countries.Any())
            {
                response.Success = false;
                response.Message = "Creation Failed Name already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }

            else
            {

                var Country = await _unitOfWork.Repository<Hrm.Domain.Country>().Get(request.CountryDto.CountrytId);

                if (Country is null)
                {
                    throw new NotFoundException(nameof(Country), request.CountryDto.CountrytId);
                }

                _mapper.Map(request.CountryDto, Country);

                await _unitOfWork.Repository<Hrm.Domain.Country>().Update(Country);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = Country.CountryId;

            }

            return response;
        }
    }
}
