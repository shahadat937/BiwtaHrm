using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Language.Validators;
using Hrm.Application.DTOs.LanguageLanguage.Validators;
using Hrm.Application.Features.Language.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Language.Handlers.Commands
{
    public class CreateLanguageCommandHandler : IRequestHandler<CreateLanguageCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateLanguageCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLanguageDtoValidator();
            var validationResult = await validator.ValidateAsync(request.LanguageDto);
            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Faild";
                response.Errors=validationResult.Errors.Select(q=>q.ErrorMessage).ToList();
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
            return response;
        }
    }
}
