using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Workday.Validations;
using Hrm.Application.Features.Workday.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Workday.Handlers.Commands
{
    public class CreateWorkdayCommandHandler : IRequestHandler<CreateWorkdayCommand, BaseCommandResponse>
    {
        IUnitOfWork _unitOfWork;
        IMapper _mapper;

        public CreateWorkdayCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle (CreateWorkdayCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateWorkdayDtoValidator();
            var validationResult = await validator.ValidateAsync(request.WorkdayDto);

            if (validationResult.IsValid==false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            } else
            {
                var Workday = _mapper.Map<Hrm.Domain.Workday>(request.WorkdayDto);
                Workday = await _unitOfWork.Repository<Hrm.Domain.Workday>().Add(Workday);
                await _unitOfWork.Save();

                response.Success=true;
                response.Message = "Creation Successfull";
                response.Id = Workday.WorkdayId;
            }

            return response;
        }
    }
}
