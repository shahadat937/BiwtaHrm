using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.RetiredReasons.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.RetiredReasons.Handlers.Commands
{
    public class UpdateRetiredReasonCommandHandler : IRequestHandler<UpdateRetiredReasonCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateRetiredReasonCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateRetiredReasonCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var RetiredReason = await _unitOfWork.Repository<RetiredReason>().Get(request.RetiredReasonDto.Id);


            var RetiredReasonsDto = _mapper.Map(request.RetiredReasonDto, RetiredReason);


            await _unitOfWork.Repository<RetiredReason>().Update(RetiredReasonsDto);
            await _unitOfWork.Save();


            response.Success = true;
            response.Message = "Update Successful";
            response.Id = RetiredReason.Id;

            return response;
        }
    }
}