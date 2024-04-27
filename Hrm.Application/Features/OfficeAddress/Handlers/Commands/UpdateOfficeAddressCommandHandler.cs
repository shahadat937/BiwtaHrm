using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.OfficeAddress.Validators;
using Hrm.Application.DTOs.OfficeAddress.ValidatorsOfficeAddress;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.OfficeAddress.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.OfficeAddress.Handlers.Commands
{
    public class UpdateOfficeAddressCommandHandler : IRequestHandler<UpdateOfficeAddressCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.OfficeAddress> _officeAddressRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateOfficeAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.OfficeAddress> officeAddressRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _officeAddressRepository = officeAddressRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateOfficeAddressCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateOfficeAddressDtoValidator();
            var validationResult = await validator.ValidateAsync(request.OfficeAddressDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var OfficeAddress = await _unitOfWork.Repository<Hrm.Domain.OfficeAddress>().Get(request.OfficeAddressDto.OfficeAddressId);

            if (OfficeAddress is null)
            {
                throw new NotFoundException(nameof(OfficeAddress), request.OfficeAddressDto.OfficeAddressId);
            }

            var officeAddressName = request.OfficeAddressDto.OfficeAddressName.ToLower();

            IQueryable<Hrm.Domain.OfficeAddress> officeAddresss = _officeAddressRepository.Where(x => x.OfficeAddressName.ToLower() == officeAddressName);


            if (officeAddresss.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed '{request.OfficeAddressDto.OfficeAddressName}' already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }
            else
            {

                _mapper.Map(request.OfficeAddressDto, OfficeAddress);

                await _unitOfWork.Repository<Hrm.Domain.OfficeAddress>().Update(OfficeAddress);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successful";
                response.Id = OfficeAddress.OfficeAddressId;

            }
            return response;
        }
    }
}
