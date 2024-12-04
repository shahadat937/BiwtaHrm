using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpBasicInfo;
using Hrm.Application.Features.EmpBasicInfos.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpBasicInfos.Handlers.Queries
{
    public class GetSelectedFilteredEmpBasicInfoRequestHandler: IRequestHandler<GetSelectedFilteredEmpBasicInfoRequest, object>
    {
        private readonly IHrmRepository<Hrm.Domain.EmpBasicInfo> _EmpBasicInfoRepository;
        private IMapper _mapper;
        public GetSelectedFilteredEmpBasicInfoRequestHandler(IHrmRepository<Hrm.Domain.EmpBasicInfo> EmpBasicInfoRepository, IMapper mapper)
        {
            _EmpBasicInfoRepository = EmpBasicInfoRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetSelectedFilteredEmpBasicInfoRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.EmpBasicInfo> EmpInfos = _EmpBasicInfoRepository.Where(x => true)
                .Include(em => em.EmpJobDetail)
                .AsQueryable();

            if(request.EmpFilterDto.OfficeId.HasValue)
            {
                EmpInfos = EmpInfos.Where(em => em.EmpJobDetail!=null&& em.EmpJobDetail.Any() && em.EmpJobDetail.ToList()[0].OfficeId == request.EmpFilterDto.OfficeId);
            }

            if(request.EmpFilterDto.DepartmentId.HasValue)
            {
                EmpInfos = EmpInfos.Where(em => em.EmpJobDetail!=null && em.EmpJobDetail.Any() && em.EmpJobDetail.ToList()[0].DepartmentId == request.EmpFilterDto.DepartmentId);
            }

            if(request.EmpFilterDto.SectionId.HasValue)
            {
                EmpInfos = EmpInfos.Where(em => em.EmpJobDetail != null && em.EmpJobDetail.Any() && em.EmpJobDetail.FirstOrDefault().SectionId == request.EmpFilterDto.SectionId);
            }

            if(request.EmpFilterDto.DesignationId.HasValue)
            {
                EmpInfos = EmpInfos.Where(em => em.EmpJobDetail != null && em.EmpJobDetail.Any() && em.EmpJobDetail.ToList()[0].DesignationId == request.EmpFilterDto.DesignationId);
            }

            var SelectedEmpInfos = await EmpInfos.Select(x => new SelectedModel
            {
                Id = x.Id,
                Name = x.FirstName + " " + x.LastName
            }).ToListAsync();

            return SelectedEmpInfos;
        }
    }
}
