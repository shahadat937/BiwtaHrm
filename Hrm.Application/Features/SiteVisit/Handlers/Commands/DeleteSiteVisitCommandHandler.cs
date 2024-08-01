using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.SiteVisit.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Domain;
using Hrm.Application.Exceptions;
namespace Hrm.Application.Features.SiteVisit.Handlers.Commands
{
    public class DeleteSiteVisitCommandHandler: IRequestHandler<DeleteSiteVisitCommand,BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteSiteVisitCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteSiteVisitCommand request, CancellationToken cancellationToken)
        {
            //BaseCommandResponse response = new BaseCommandResponse();
            //int SiteVisit = await  _unitOfWork.Repository<Hrm.Domain.SiteVisit>().Get(request.SiteVisitId);

            //if(SiteVisitId == null)
            //{
            //    response.Success = false;
            //    response.Message = "Creation Failed";

            //    return response;
            //}

            //await _unitOfWork.Repository<SiteVisit>().Delete(SiteVisit);
            //await _unitOfWork.Save();

            //response.Success = true;
            //response.Message = "Deleted Successfull";

            //return response;
            var response = new BaseCommandResponse();


            var SiteVisit = await _unitOfWork.Repository<Hrm.Domain.SiteVisit>().Get(request.SiteVisitId);

            if (SiteVisit == null)
            {
                throw new NotFoundException(nameof(SiteVisit), request.SiteVisitId);
            }

            if(SiteVisit.Status=="Approved")
            {
                response.Success = false;
                response.Message = "Approved Site Visit can't be deleted";
                response.Id = SiteVisit.SiteVisitId;
                return response;
            }

            await _unitOfWork.Repository<Hrm.Domain.SiteVisit>().Delete(SiteVisit);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = SiteVisit.SiteVisitId;

            return response;
        }
    }
}
