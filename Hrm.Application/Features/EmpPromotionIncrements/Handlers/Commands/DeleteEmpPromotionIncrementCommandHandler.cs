using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.EmpPromotionIncrements.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpPromotionIncrements.Handlers.Commands
{
    public class DeleteEmpPromotionIncrementCommandHandler : IRequestHandler<DeleteEmpPromotionIncrementCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEmpPromotionIncrementCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseCommandResponse> Handle(DeleteEmpPromotionIncrementCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var EmpPromotionIncrement = await _unitOfWork.Repository<EmpPromotionIncrement>().Get(request.Id);


            if (EmpPromotionIncrement == null)
            {
                throw new NotFoundException(nameof(EmpPromotionIncrement), request.Id);
            }

            await _unitOfWork.Repository<Hrm.Domain.EmpPromotionIncrement>().Delete(EmpPromotionIncrement);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = EmpPromotionIncrement.Id;

            return response;

        }
    }
}
