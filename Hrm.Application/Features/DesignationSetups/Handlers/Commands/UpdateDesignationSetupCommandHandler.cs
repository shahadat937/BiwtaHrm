using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.DesignationSetups.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.DesignationSetups.Handlers.Commands
{
    public class UpdateDesignationSetupCommandHandler : IRequestHandler<UpdateDesignationSetupCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.DesignationSetup> _DesignationSetupRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateDesignationSetupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.DesignationSetup> DesignationSetupRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _DesignationSetupRepository = DesignationSetupRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateDesignationSetupCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            //var DesignationSetupName = request.DesignationSetupDto.DesignationSetupName.ToLower();
            var DesignationSetupName = request.DesignationSetupDto.Name.Trim().ToLower().Replace(" ", string.Empty);
            IQueryable<Hrm.Domain.DesignationSetup> DesignationSetups = _DesignationSetupRepository.Where(x => x.Name.Trim().ToLower().Replace(" ", string.Empty) == DesignationSetupName);



            if (DesignationSetups.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed '{request.DesignationSetupDto.Name}' already exists.";


            }

            else
            {

                var DesignationSetup = await _unitOfWork.Repository<Hrm.Domain.DesignationSetup>().Get(request.DesignationSetupDto.Id);

                if (DesignationSetup is null)
                {
                    throw new NotFoundException(nameof(DesignationSetup), request.DesignationSetupDto.Id);
                }

                _mapper.Map(request.DesignationSetupDto, DesignationSetup);

                await _unitOfWork.Repository<Hrm.Domain.DesignationSetup>().Update(DesignationSetup);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = DesignationSetup.Id;

            }

            return response;
        }
    }
}
