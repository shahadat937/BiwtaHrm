using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.BankAccountType;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.BankAccountType.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.BankAccountType.Handlers.Queries
{
    public class GetBankAccountTypeRequestHandler : IRequestHandler<GetBankAccountTypeRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.BankAccountType> _BankAccountTypeRepository;
        private readonly IMapper _mapper;
        public GetBankAccountTypeRequestHandler(IHrmRepository<Hrm.Domain.BankAccountType> BankAccountTypeRepository, IMapper mapper)
        {
            _BankAccountTypeRepository = BankAccountTypeRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetBankAccountTypeRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.BankAccountType> BankAccountTypes = _BankAccountTypeRepository.Where(x => true);
            BankAccountTypes = BankAccountTypes.OrderByDescending(x => x.BankAccountTypeId);
            var BankAccountTypeDtos = _mapper.Map<List<BankAccountTypeDto>>(BankAccountTypes);


            return BankAccountTypeDtos;
        }
    }
}
