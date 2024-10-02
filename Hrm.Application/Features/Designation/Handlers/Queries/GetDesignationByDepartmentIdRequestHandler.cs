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
    public class GetDesignationByDepartmentIdRequestHandler : IRequestHandler<GetDesignationByDepartmentIdRequest, List<SelectedModel>>
    {

        private readonly IHrmRepository<Hrm.Domain.Designation> _DesignationRepository;
        private readonly IMapper _mapper;
        public GetDesignationByDepartmentIdRequestHandler(IHrmRepository<Hrm.Domain.Designation> DesignationRepository, IMapper mapper)
        {
            _DesignationRepository = DesignationRepository;
            _mapper = mapper;

        }

        public async Task<List<SelectedModel>> Handle(GetDesignationByDepartmentIdRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.Designation> Designations = _DesignationRepository.FilterWithInclude(x => x.DepartmentId == request.DepartmentId && x.SectionId == null).ToList();
            List<SelectedModel> SelectedModel = Designations.Select(x => new SelectedModel
            {
                Id = x.DesignationId,
                Name = x.DesignationName
            }).ToList();
            return SelectedModel;
        }
    }
}
