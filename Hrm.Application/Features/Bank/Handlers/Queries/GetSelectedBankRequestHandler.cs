using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Bank.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Bank.Handlers.Queries
{ 
    public class GetSelectedBankRequestHandler : IRequestHandler<GetSelectedBankRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.Bank> _BankRepository;


        public GetSelectedBankRequestHandler(IHrmRepository<Hrm.Domain.Bank> BankRepository)
        {
            _BankRepository = BankRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedBankRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.Bank> Banks = await _BankRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = Banks.Select(x => new SelectedModel 
            {
                Name = x.BankName,
                Id = x.BankId
            }).ToList();
            return selectModels;
        }
    }
}
 