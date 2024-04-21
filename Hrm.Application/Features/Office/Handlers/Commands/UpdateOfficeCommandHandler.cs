using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Office.Validators;
using Hrm.Application.DTOs.Office.ValidatorsOffice;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Office.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Office.Handlers.Commands
{
    public class UpdateOfficeCommandHandler : IRequestHandler<UpdateOfficeCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.Office> _officeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateOfficeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Office> officeRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _officeRepository = officeRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateOfficeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateOfficeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.OfficeDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var Office = await _unitOfWork.Repository<Hrm.Domain.Office>().Get(request.OfficeDto.OfficeId);

            if (Office is null)
            {
                throw new NotFoundException(nameof(Office), request.OfficeDto.OfficeId);
            }

            var officeName = request.OfficeDto.OfficeName.ToLower();

            IQueryable<Hrm.Domain.Office> offices = _officeRepository.Where(x => x.OfficeName.ToLower() == officeName);


            if (offices.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed '{request.OfficeDto.OfficeName}' already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }
            else
            {

                _mapper.Map(request.OfficeDto, Office);

                await _unitOfWork.Repository<Hrm.Domain.Office>().Update(Office);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successful";
                response.Id = Office.OfficeId;

            }
            return response;
        }
    }
}
