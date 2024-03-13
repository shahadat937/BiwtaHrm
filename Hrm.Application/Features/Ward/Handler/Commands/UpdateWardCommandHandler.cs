using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.TrainingType.Validators;
using Hrm.Application.DTOs.Ward.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.TrainingType.Requests.Commands;
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
    public class UpdateWardCommandHandler : IRequestHandler<UpdateWardCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.Ward> _wardRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateWardCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Ward> wardRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _wardRepository = wardRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateWardCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateWardDtoValidator();
            var validationResult = await validator.ValidateAsync(request.WardDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var wardName = request.WardDto.WardName.ToLower();

            IQueryable<Hrm.Domain.Ward> wards = _wardRepository.Where(x => x.WardName.ToLower() == wardName);



            if (wards.Any())
            {
                response.Success = false;
                response.Message = "Creation Failed Name already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }

            else
            {

                var Ward = await _unitOfWork.Repository<Hrm.Domain.Ward>().Get(request.WardDto.WardId);

                if (Ward is null)
                {
                    throw new NotFoundException(nameof(TrainingType), request.WardDto.WardId);
                }

                _mapper.Map(request.WardDto, Ward);

                await _unitOfWork.Repository<Hrm.Domain.Ward>().Update(Ward);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = Ward.WardId;

            }

            return response;
        }
    }
}
