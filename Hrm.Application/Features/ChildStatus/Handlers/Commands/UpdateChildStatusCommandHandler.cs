using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.ChildStatus.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.ChildStatus.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.ChildStatus.Handlers.Commands
{
    public class UpdateChildStatusCommandHandler : IRequestHandler<UpdateChildStatusCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.ChildStatus> _ChildStatusRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateChildStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.ChildStatus> ChildStatusRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _ChildStatusRepository =ChildStatusRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateChildStatusCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateChildStatusDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ChildStatusDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var ChildStatus = await _unitOfWork.Repository<Hrm.Domain.ChildStatus>().Get(request.ChildStatusDto.ChildStatusId);

            if (ChildStatus is null)
            {
                throw new NotFoundException(nameof(ChildStatus), request.ChildStatusDto.ChildStatusId);
            }


            var ChildStatusName = request.ChildStatusDto.ChildStatusName.ToLower();

            IQueryable<Hrm.Domain.ChildStatus> isCheckChildStatusName = _ChildStatusRepository.Where(x => x.ChildStatusName.ToLower() == ChildStatusName);


            if (isCheckChildStatusName.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed '{request.ChildStatusDto.ChildStatusName}' already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }
            else
            {

                _mapper.Map(request.ChildStatusDto, ChildStatus);

                await _unitOfWork.Repository<Hrm.Domain.ChildStatus>().Update(ChildStatus);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successful";
                response.Id = ChildStatus.ChildStatusId;



            }
            return response;
        }
    }
}
