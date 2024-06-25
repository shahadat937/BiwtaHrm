using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.SubDepartment;
using Hrm.Application.Features.SubDepartment.Requests.Queries;
using Hrm.Domain;
using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SubDepartment.Handlers.Queries
{
    public class GetDiviosionByDepartmentIdRequestHandler : IRequestHandler<GetSubDepartmentByDepartmentIdRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.SubDepartment> _SubDepartmentRepository;
        private readonly IMapper _mapper;
        public GetDiviosionByDepartmentIdRequestHandler(IHrmRepository<Hrm.Domain.SubDepartment> SubDepartmentRepositoy, IMapper mapper)
        {
            _SubDepartmentRepository = SubDepartmentRepositoy;
            _mapper = mapper;

        }
        public async Task<List<SelectedModel>> Handle(GetSubDepartmentByDepartmentIdRequest request, CancellationToken cancellationToken)
        {
            //IQueryable<Hrm.Domain.SubDepartment> SubDepartments = _SubDepartmentRepository.Get(request.CountryId);
            //var SubDepartmentDtos = new List<SubDepartmentDto>();
            //return SubDepartmentDtos;
            ICollection<Hrm.Domain.SubDepartment> SubDepartments = _SubDepartmentRepository.FilterWithInclude(x => x.DepartmentId == request.DepartmentId).ToList();
            List<SelectedModel> SubDepartmentNames = SubDepartments.Select(x => new SelectedModel
            {
                Id = x.SubDepartmentId,
                Name = x.SubDepartmentName
            }).ToList();
         //   var SubDepartmentDtos = _mapper.Map<List<SubDepartmentDto>>(SubDepartmentNames);
            return SubDepartmentNames;
        }
    }
}
