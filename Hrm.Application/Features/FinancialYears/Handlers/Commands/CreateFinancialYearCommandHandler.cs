using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.FinancialYear;
using Hrm.Application.Features.FinancialYears.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FinancialYears.Handlers.Commands
{
    public class CreateFinancialYearCommandHandler : IRequestHandler<CreateFinancialYearCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateFinancialYearCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateFinancialYearCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var FinancialYears = _mapper.Map<FinancialYear>(request.FinancialYearDto);

            FinancialYears = await _unitOfWork.Repository<FinancialYear>().Add(FinancialYears);
            await _unitOfWork.Save();


            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = FinancialYears.Id;

            return response;
        }
    }
}
