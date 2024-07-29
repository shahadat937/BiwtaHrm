using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Shift.Validators;
using Hrm.Application.DTOs.SiteVisit.Validators;
using Hrm.Application.Features.EmpBasicInfos.Handlers.Queries;
using Hrm.Application.Features.SiteVisit.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SiteVisit.Handlers.Commands
{
    public class CreateSiteVisitCommandHandler : IRequestHandler<CreateSiteVisitCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateSiteVisitCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateSiteVisitCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateSiteVisitDtoValidator();
            var validationResult = await validator.ValidateAsync(request.SiteVisitDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                request.SiteVisitDto.Status = "Pending";
                var SiteVisit = _mapper.Map<Hrm.Domain.SiteVisit>(request.SiteVisitDto);

                SiteVisit = await _unitOfWork.Repository<Hrm.Domain.SiteVisit>().Add(SiteVisit);
                await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = SiteVisit.SiteVisitId;
            }

            return response;

        }
    }
}
