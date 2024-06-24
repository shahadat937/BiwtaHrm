using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.District.Validators;
using Hrm.Application.Features.District.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.District.Handlers.Commands
{
    public class CreateDistrictCommandHandler : IRequestHandler<CreateDistrictCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateDistrictCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateDistrictCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateDistrictDtoValidator();
            var validationResult = await validator.ValidateAsync(request.DistrictDto);
            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Faild";
                response.Errors=validationResult.Errors.Select(q=>q.ErrorMessage).ToList();
            }
            else
            {
                var District = _mapper.Map<Hrm.Domain.District>(request.DistrictDto);
                District = await _unitOfWork.Repository<Hrm.Domain.District>().Add(District);
                await _unitOfWork.Save();
                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = District.DistrictId;
            }
            return response;
        }
    }
}
