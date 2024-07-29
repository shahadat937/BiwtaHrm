using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.SiteVisit.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SiteVisit.Handlers.Commands
{
    public class ApproveSiteVisitCommandHandler: IRequestHandler<ApproveSiteVisitCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ApproveSiteVisitCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(ApproveSiteVisitCommand request, CancellationToken cancellationToken)
        {
            if (request==null)
            {
                throw new BadRequestException("Invalid Request");
            }

            var sitevisit = await _unitOfWork.Repository<Hrm.Domain.SiteVisit>().Get(request.SiteVisitId);

            if(sitevisit==null)
            {
                throw new NotFoundException(nameof(sitevisit), request.SiteVisitId);
            }

            sitevisit.Status = "Approved";

            await _unitOfWork.Repository<Hrm.Domain.SiteVisit>().Update(sitevisit);
            await _unitOfWork.Save();

            var response = new BaseCommandResponse();
            response.Success = true;
            response.Message = "Approved";
            response.Id = sitevisit.SiteVisitId;

            return response;
        }
    }
}
