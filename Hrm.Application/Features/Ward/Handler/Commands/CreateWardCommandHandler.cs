using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.DTOs.Ward.Validators;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.Ward.Request.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Ward.Handler.Commands
{
    public class CreateWardCommandHandler : IRequestHandler<CreateWardCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.Ward> _wardRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateWardCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Ward> wardRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _wardRepository = wardRepository;
        }


        public async Task<BaseCommandResponse> Handle(CreateWardCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateWardDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.WardDto);

            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            else
            {

                var wardName = request.WardDto.WardName.ToLower();

                IQueryable<Hrm.Domain.Ward> wards = _wardRepository.Where(x => x.WardName.ToLower() == wardName && x.UnionId == request.WardDto.UnionId);



                if (wards.Any())
                {
                    response.Success = false;
                    response.Message = "Creation Failed Name already exists.";
                    response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();

                }

                else
                {
                    var Ward = _mapper.Map<Hrm.Domain.Ward>(request.WardDto);

                    Ward = await _unitOfWork.Repository<Hrm.Domain.Ward>().Add(Ward);
                    await _unitOfWork.Save();

                    response.Success = true;
                    response.Message = "Creation Successfull";
                    response.Id = Ward.WardId;
                }
            }

            return response;
        }
    }
}
