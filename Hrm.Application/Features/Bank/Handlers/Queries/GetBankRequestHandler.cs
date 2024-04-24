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
    public class GetBankRequestHandler : IRequestHandler<GetBankRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.Bank> _BankRepository;
        private readonly IMapper _mapper;
        public GetBankRequestHandler(IHrmRepository<Hrm.Domain.Bank> BankRepository, IMapper mapper)
        {
            _BankRepository = BankRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetBankRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.Bank> Banks = _BankRepository.Where(x => true);

            var BankDtos = _mapper.Map<List<BankDto>>(Banks);

            return BankDtos;
        }
    }
}
