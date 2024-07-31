using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.SiteVisit.Validators;
using Hrm.Application.Features.appraisalFormType.Requests.Commands;
using Hrm.Application.Features.EmpBasicInfos.Handlers.Queries;
using Hrm.Application.Features.SiteVisit.Requests.Commands;
using Hrm.Application.Helpers;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SiteVisit.Handlers.Commands
{
    public class UpdateSiteVisitCommandHandler: IRequestHandler<UpdateSiteVisitCommand,BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly SiteVisitAtdHelper _siteVisitAtdHelper;

        public UpdateSiteVisitCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _siteVisitAtdHelper = new SiteVisitAtdHelper(unitOfWork, mapper);
        }

        public async Task<BaseCommandResponse> Handle(UpdateSiteVisitCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new BaseCommandResponse();
            var validator = new UpdateSiteVisitDtoValidator();

            var validationResult = await validator.ValidateAsync(request.SiteVisitDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return response;
            }


            var SiteVisit = await _unitOfWork.Repository<Hrm.Domain.SiteVisit>().Get((int)request.SiteVisitDto.SiteVisitId);

            if(SiteVisit == null)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                return response;
            }

            
            _siteVisitAtdHelper.siteVisitId = SiteVisit.SiteVisitId;


            if(SiteVisit.Status=="Approved")
            {
                await _siteVisitAtdHelper.deleteAttendance();
                await _siteVisitAtdHelper.saveAttendance((DateOnly)request.SiteVisitDto.FromDate, (DateOnly)request.SiteVisitDto.ToDate, request.SiteVisitDto.EmpId);
            }
            
            
            request.SiteVisitDto.Status = SiteVisit.Status;

            _mapper.Map(request.SiteVisitDto, SiteVisit);

            await _unitOfWork.Repository<Hrm.Domain.SiteVisit>().Update(SiteVisit);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Update Successful";
            response.Id = SiteVisit.SiteVisitId;

            return response;
        }
    }
}
