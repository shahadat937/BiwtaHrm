using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.BankAccountType;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.BankAccountType.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.BankAccountType.Handlers.Queries
{
    public class GetBankAccountTypeByIdRequestHandler : IRequestHandler<GetBankAccountTypeByIdRequest, BankAccountTypeDto>
    {

        private readonly IHrmRepository<Hrm.Domain.BankAccountType> _BankAccountTypeRepository;
        private readonly IMapper _mapper;
        public GetBankAccountTypeByIdRequestHandler(IHrmRepository<Hrm.Domain.BankAccountType> BankAccountTypeRepositoy, IMapper mapper)
        {
            _BankAccountTypeRepository = BankAccountTypeRepositoy;
            _mapper = mapper;
        }

        public async Task<BankAccountTypeDto> Handle(GetBankAccountTypeByIdRequest request, CancellationToken cancellationToken)
        {
            var BankAccountType = await _BankAccountTypeRepository.Get(request.BankAccountTypeId);
            return _mapper.Map<BankAccountTypeDto>(BankAccountType);
        }
    }
}
