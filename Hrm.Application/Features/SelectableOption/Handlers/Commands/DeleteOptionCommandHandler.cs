using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.SelectableOption.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SelectableOption.Handlers.Commands
{
    public class DeleteOptionCommandHandler: IRequestHandler<DeleteOptionCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteOptionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteOptionCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var Option =await _unitOfWork.Repository<Hrm.Domain.SelectableOption>().Get(request.OptionId);

            if(Option == null)
            {
                throw new NotFoundException(nameof(Option),request.OptionId);
            }

            await _unitOfWork.Repository<Hrm.Domain.SelectableOption>().Delete(Option);

            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successful";
            response.Id = request.OptionId;

            return response;
        }
    }
}
