using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.FinancialYears.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FinancialYears.Handlers.Commands
{
    public class UpdateFinancialYearCommandHandler : IRequestHandler<UpdateFinancialYearCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateFinancialYearCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateFinancialYearCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var FinancialYear = await _unitOfWork.Repository<FinancialYear>().Get(request.FinancialYearDto.Id);


            var FinancialYearsDto = _mapper.Map(request.FinancialYearDto, FinancialYear);


            await _unitOfWork.Repository<FinancialYear>().Update(FinancialYearsDto);
            await _unitOfWork.Save();


            response.Success = true;
            response.Message = "Update Successful";
            response.Id = FinancialYear.Id;

            return response;
        }
    }
}