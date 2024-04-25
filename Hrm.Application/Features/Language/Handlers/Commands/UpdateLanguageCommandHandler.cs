using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Language.Validators;
using Hrm.Application.DTOs.Language.ValidatorsLanguage;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Language.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Language.Handlers.Commands
{
    public class UpdateLanguageCommandHandler : IRequestHandler<UpdateLanguageCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.Language> _languageRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateLanguageCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Language> LanguageRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _languageRepository = languageRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateLanguageDtoValidator();
            var validationResult = await validator.ValidateAsync(request.LanguageDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            //var LanguageName = request.LanguageDto.LanguageName.ToLower();
            var LanguageName = request.LanguageDto.LanguageName.Trim().ToLower().Replace(" ", string.Empty);
            IQueryable<Hrm.Domain.Language> Languages = _LanguageRepository.Where(x => x.LanguageName.ToLower() == LanguageName);



            if (Languages.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed '{request.LanguageDto.LanguageName}' already exists.";  
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }

            else
            {

                var Language = await _unitOfWork.Repository<Hrm.Domain.Language>().Get(request.LanguageDto.LanguageId);

                if (Language is null)
                {
                    throw new NotFoundException(nameof(Language), request.LanguageDto.LanguageId);
                }

                _mapper.Map(request.LanguageDto, Language);

                await _unitOfWork.Repository<Hrm.Domain.Language>().Update(Language);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = Language.LanguageId;

            }

            return response;
        }
    }
}
