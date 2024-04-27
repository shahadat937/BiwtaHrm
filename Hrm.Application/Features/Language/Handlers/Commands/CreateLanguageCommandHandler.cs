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


        private readonly IHrmRepository<Hrm.Domain.Language> _LanguageRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateLanguageCommandHandler(IHrmRepository<Hrm.Domain.Language> LanguageRepository, IUnitOfWork unitOfWork, IMapper mapper)

        {
            _LanguageRepository = LanguageRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _LanguageRepository = LanguageRepository;
        }

        public async Task<BaseCommandResponse> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLanguageDtoValidator();

            var validatorResult = await validator.ValidateAsync(request.LanguageDto);

            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                var LanguageName = request.LanguageDto.LanguageName.ToLower();

                IQueryable<Hrm.Domain.Language> Languages = _LanguageRepository.Where(x => x.LanguageName.ToLower() == LanguageName);


                if (LanguageNameExists(request))
                {
                    response.Success = false;
                    response.Message = $"Creation Failed '{request.LanguageDto.LanguageName}' already exists.";


                    //response.Message = "Creation Failed, Name already exists";
                    response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();

                }
                else
                {
                    var Language = _mapper.Map<Hrm.Domain.Language>(request.LanguageDto);

                    Language = await _unitOfWork.Repository<Hrm.Domain.Language>().Add(Language);
                    await _unitOfWork.Save();
                    response.Success = true;
                    response.Message = "Creation Successfull";
                    response.Id = Language.LanguageId;
                }
            }

            return response;
        }
        private bool LanguageNameExists(CreateLanguageCommand request)
        {

            var LanguageName = request.LanguageDto.LanguageName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.Language> Languages = _LanguageRepository.Where(x => x.LanguageName.Trim().ToLower().Replace(" ", string.Empty) == LanguageName);

            return Languages.Any();

        }
    }
}
