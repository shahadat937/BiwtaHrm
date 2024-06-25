using AutoMapper;
using FluentValidation;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.BloodGroup.Validators;
using Hrm.Application.DTOs.Thana.Validators;
using Hrm.Application.Features.Thana.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Thana.Handlers.Commands
{
    public class CreateThanaCommandHandler : IRequestHandler<CreateThanaCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.Thana> _ThanaRepository; 
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateThanaCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Thana> ThanaRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _ThanaRepository = ThanaRepository;
        }


        public async Task<BaseCommandResponse> Handle(CreateThanaCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateThanaDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.ThanaDto);

            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validatorResult.Errors.Select(x=> x.ErrorMessage).ToList();
            }
            else
            {

                var ThanaName = request.ThanaDto.ThanaName.ToLower();

                IQueryable<Hrm.Domain.Thana> Thanaes = _ThanaRepository.Where(x => x.ThanaName.ToLower() == ThanaName);

                

                if (Thanaes.Any())
                {
                    response.Success = false;
                    response.Message = "Creation Failed Name already exists.";
                    response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();

                }

                else
                {
                    var Thana = _mapper.Map<Hrm.Domain.Thana>(request.ThanaDto);

                    Thana = await _unitOfWork.Repository<Hrm.Domain.Thana>().Add(Thana);
                    await _unitOfWork.Save();

                    response.Success = true;
                    response.Message = "Creation Successfull";
                    response.Id = Thana.ThanaId;
                }
            }

            return response;
        }
    }
}
