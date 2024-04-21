using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Language.Validators;
using Hrm.Application.Features.Language.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Microsoft.EntityFrameworkCore;
using Hrm.Application.DTOs.LanguageLanguage.Validators;

namespace Hrm.Application.Features.Language.Handlers.Commands
{
    public class CreateLanguageCommandHandler : IRequestHandler<CreateLanguageCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.Language> _languageRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateLanguageCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Language> languageRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _languageRepository = languageRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLanguageDtoValidator();
            var validationResult = await validator.ValidateAsync(request.LanguageDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                //   var languageName = request.LanguageDto.LanguageName.Trim().ToLower().Replace(" ", string.Empty);

                //  IQueryable<Hrm.Domain.Language> languages = _languageRepository.Where(x => x.LanguageName.ToLower().Replace(" ", string.Empty) == languageName);


                if (LanguageNameExists(request))
                {
                    response.Success = false;
                    response.Message = $"Creation Failed '{request.LanguageDto.LanguageName}' already exists.";
                    response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

                }
                else
                {
                    var Language = _mapper.Map<Hrm.Domain.Language>(request.LanguageDto);

                    Language = await _unitOfWork.Repository<Hrm.Domain.Language>().Add(Language);
                    await _unitOfWork.Save();


                    response.Success = true;
                    response.Message = "Creation Successful";
                    response.Id = Language.LanguageId;
                }
            }

            return response;
        }
        private bool LanguageNameExists(CreateLanguageCommand request)
        {
            var languageName = request.LanguageDto.LanguageName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.Language> languages = _languageRepository.Where(x => x.LanguageName.Trim().ToLower().Replace(" ", string.Empty) == languageName);

            return languages.Any();
        }
    }
}
