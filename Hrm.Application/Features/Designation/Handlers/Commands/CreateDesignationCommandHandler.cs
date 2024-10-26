using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Designation.Validators;
using Hrm.Application.Features.Designation.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Hrm.Application.DTOs.Designation.Validators;
using Hrm.Application.Features.Designation.Requests.Commands;
using Hrm.Application.DTOs.Designation.Validators;

namespace Hrm.Application.Features.Designation.Handlers.Commands
{
    public class CreateDesignationCommandHandler : IRequestHandler<CreateDesignationCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.Designation> _DesignationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateDesignationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Designation> DesignationRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _DesignationRepository = DesignationRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateDesignationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var successCount = 0;
            var errorCount = 0;
            var position = request.DesignationDto.MenuPosition;


            for (int i = 0; i < request.DesignationDto.CreateCount; i++)
            {
                var Designation = _mapper.Map<Hrm.Domain.Designation>(request.DesignationDto);
                Designation.MenuPosition = position;
                await _unitOfWork.Repository<Hrm.Domain.Designation>().Add(Designation);
                await _unitOfWork.Save();
                successCount++;
                position++;
            }
            if (successCount == 0)
            {
                errorCount = request.DesignationDto.CreateCount ?? 0;
            }

            response.Success = true;
            response.Message = $"'{successCount}' Creation Successful, '{errorCount}' Failed";


            return response;
        }
    }
}
