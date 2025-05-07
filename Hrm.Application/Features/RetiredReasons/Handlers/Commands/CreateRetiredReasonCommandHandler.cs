using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.RetiredReason;
using Hrm.Application.Features.RetiredReasons.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.RetiredReasons.Handlers.Commands
{
    public class CreateRetiredReasonCommandHandler : IRequestHandler<CreateRetiredReasonCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateRetiredReasonCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateRetiredReasonCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var RetiredReasons = _mapper.Map<RetiredReason>(request.RetiredReasonDto);

            RetiredReasons = await _unitOfWork.Repository<RetiredReason>().Add(RetiredReasons);
            await _unitOfWork.Save();


            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = RetiredReasons.Id;

            return response;
        }
    }
}
