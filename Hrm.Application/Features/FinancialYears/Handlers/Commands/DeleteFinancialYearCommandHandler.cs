using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.FinancialYears.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FinancialYears.Handlers.Commands
{
    public class DeleteFinancialYearCommandHandler : IRequestHandler<DeleteFinancialYearCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteFinancialYearCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteFinancialYearCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var FinancialYear = await _unitOfWork.Repository<Hrm.Domain.FinancialYear>().Get(request.Id);

            if (FinancialYear == null)
                throw new NotFoundException(nameof(FinancialYear), request.Id);

            await _unitOfWork.Repository<Hrm.Domain.FinancialYear>().Delete(FinancialYear);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successful";
            response.Id = FinancialYear.Id;

            return response;
        }
    }
}