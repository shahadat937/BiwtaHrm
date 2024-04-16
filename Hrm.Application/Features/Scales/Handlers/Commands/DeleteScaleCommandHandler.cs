using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Scales.Requests.Commands;
using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Scales.Handlers.Commands
{
    public class DeleteScaleCommandHandler : IRequestHandler<DeleteScaleCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteScaleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteScaleCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var Scale = await _unitOfWork.Repository<Hrm.Domain.Scale>().Get(request.ScaleId);

            if (Scale == null)
            {
                throw new NotFoundException(nameof(Scale), request.ScaleId);
            }

            await _unitOfWork.Repository<Hrm.Domain.Scale>().Delete(Scale);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = Scale.ScaleId;

            return response;
        }
    }
}
