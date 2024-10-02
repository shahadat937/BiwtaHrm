using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.ResponsibilityType.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.ResponsibilityType.Handlers.Commands
{
    public class UpdateResponsibilityTypeCommandHandler : IRequestHandler<UpdateResponsibilityTypeCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.ResponsibilityType> _ResponsibilityTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateResponsibilityTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.ResponsibilityType> ResponsibilityTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _ResponsibilityTypeRepository = ResponsibilityTypeRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateResponsibilityTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            //var ResponsibilityTypeName = request.ResponsibilityTypeDto.ResponsibilityTypeName.ToLower();
            var ResponsibilityTypeName = request.ResponsibilityTypeDto.Name.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.ResponsibilityType> ResponsibilityTypes = _ResponsibilityTypeRepository.Where(x => x.Name.Trim().ToLower().Replace(" ", string.Empty) == ResponsibilityTypeName && x.Id != request.ResponsibilityTypeDto.Id);



            if (ResponsibilityTypes.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed '{request.ResponsibilityTypeDto.Name}' already exists.";
            }

            else
            {

                var ResponsibilityType = await _unitOfWork.Repository<Hrm.Domain.ResponsibilityType>().Get(request.ResponsibilityTypeDto.Id);

                if (ResponsibilityType is null)
                {
                    throw new NotFoundException(nameof(ResponsibilityType), request.ResponsibilityTypeDto.Id);
                }

                _mapper.Map(request.ResponsibilityTypeDto, ResponsibilityType);

                await _unitOfWork.Repository<Hrm.Domain.ResponsibilityType>().Update(ResponsibilityType);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = ResponsibilityType.Id;

            }

            return response;
        }
    }
}
