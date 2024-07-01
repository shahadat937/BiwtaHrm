using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.OfficeBranch.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.OfficeBranch.Handlers.Queries
{
    public class GetSelectedDepartmentByOfficeIdRequestHandler : IRequestHandler<GetSelectedDepartmentByOfficeIdRequest, List<SelectedModel>>
    {

        private readonly IHrmRepository<Hrm.Domain.Department> _DepartmentRepository;
        private readonly IMapper _mapper;
        public GetSelectedDepartmentByOfficeIdRequestHandler(IHrmRepository<Hrm.Domain.Department> DepartmentRepository, IMapper mapper)
        {
            _DepartmentRepository = DepartmentRepository;
            _mapper = mapper;

        }

        public async Task<List<SelectedModel>> Handle(GetSelectedDepartmentByOfficeIdRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.Department> departments = _DepartmentRepository.FilterWithInclude(x => x.OfficeId == request.OfficeId).ToList();
            List<SelectedModel> SelectedModel = departments.Select(x => new SelectedModel
            {
                Id = x.DepartmentId,
                Name = x.DepartmentNameBangla
            }).ToList();
            return SelectedModel;
        }
    }
}
