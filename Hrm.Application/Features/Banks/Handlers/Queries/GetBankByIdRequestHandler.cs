using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Bank;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Bank.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Bank.Handlers.Queries
{
    public class GetExamTypeByIdRequestHandler : IRequestHandler<GetBankByIdRequest, BankDto>
    {

        private readonly IHrmRepository<Hrm.Domain.Bank> _BankRepository;
        private readonly IMapper _mapper;
        public GetExamTypeByIdRequestHandler(IHrmRepository<Hrm.Domain.Bank> BankRepositoy, IMapper mapper)
        {
            _BankRepository = BankRepositoy;
            _mapper = mapper;
        }

        public async Task<BankDto> Handle(GetBankByIdRequest request, CancellationToken cancellationToken)
        {
            var Bank = await _BankRepository.Get(request.BankId);
            return _mapper.Map<BankDto>(Bank);
        }
    }
}
