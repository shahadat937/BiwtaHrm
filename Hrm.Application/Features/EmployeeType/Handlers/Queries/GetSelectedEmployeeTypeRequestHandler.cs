using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmployeeType.Requests.Queries;
using Hrm.Application.Features.EyesColors.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmployeeType.Handlers.Queries
{
    public class GetSelectedEmployeeTypeRequestHandler : IRequestHandler<GetSelectedEmployeeTypeRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.EmployeeType> _EmployeeTypeRepository;


        public GetSelectedEmployeeTypeRequestHandler(IHrmRepository<Hrm.Domain.EmployeeType> EmployeeTypeRepository)
        {
            _EmployeeTypeRepository = EmployeeTypeRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedEmployeeTypeRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.EmployeeType> employeeTypes = await _EmployeeTypeRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = employeeTypes.Select(x => new SelectedModel
            {
                Name = x.EmployeeTypeName,
                Id = x.EmployeeTypeId
            }).ToList();
            return selectModels;
        }
    }
}