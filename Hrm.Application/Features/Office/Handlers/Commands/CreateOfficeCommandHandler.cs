using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Office.Validators;
using Hrm.Application.DTOs.OfficeOffice.Validators;
using Hrm.Application.Features.Office.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Office.Handlers.Commands
{
    public class CreateOfficeCommandHandler : IRequestHandler<CreateOfficeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateOfficeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateOfficeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateOfficeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.OfficeDto);
            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Faild";
                response.Errors=validationResult.Errors.Select(q=>q.ErrorMessage).ToList();
            }
            else
            {
                var Office = _mapper.Map<Hrm.Domain.Office>(request.OfficeDto);
                Office = await _unitOfWork.Repository<Hrm.Domain.Office>().Add(Office);
                await _unitOfWork.Save();
                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = Office.OfficeId;
            }
            return response;
        }
    }
}
