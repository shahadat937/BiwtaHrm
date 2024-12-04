using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpBasicInfo;
using Hrm.Application.DTOs.Employee;
using Hrm.Application.Features.Employee.Requests.Queries;
using Hrm.Application.Features.EmpBasicInfos.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Hrm.Application.Models;

namespace Hrm.Application.Features.EmpBasicInfos.Handlers.Queries
{
    public class GetAllEmpBasicInfoRequestHandler : IRequestHandler<GetAllEmpBasicInfoRequest, PagedResult<EmpBasicInfoDto>>
    {

        private readonly IHrmRepository<EmpBasicInfo> _EmpBasicInfoRepository;
        private readonly IMapper _mapper;
        public GetAllEmpBasicInfoRequestHandler(IHrmRepository<EmpBasicInfo> EmpBasicInfoRepository, IMapper mapper)
        {
            _EmpBasicInfoRepository = EmpBasicInfoRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<EmpBasicInfoDto>> Handle(GetAllEmpBasicInfoRequest request, CancellationToken cancellationToken)
        {
            //IQueryable<EmpBasicInfo> EmpBasicInfo = _EmpBasicInfoRepository.Where(x => true)
            //    .Include(x => x.EmployeeType)
            //    .Include(x => x.EmpJobDetail)
            //        .ThenInclude(ejd => ejd.Department)
            //    .Include(x => x.EmpJobDetail)
            //        .ThenInclude(ejd => ejd.Designation)
            //            .ThenInclude(ds => ds.DesignationSetup)
            //    .Include(x => x.EmpJobDetail)
            //        .ThenInclude(ejd => ejd.Section)
            //    .Include(x => x.EmpPhotoSign)
            //    .Include(x => x.EmpPersonalInfo)
            //        .ThenInclude(x => x.Gender);

            IQueryable<EmpBasicInfo> empBasicInfos = _EmpBasicInfoRepository.FilterWithInclude(x => (x.IdCardNo.ToLower().Contains(request.QueryParams.SearchText) || 
                x.FirstName.ToLower().Contains(request.QueryParams.SearchText) || 
                x.LastName.ToLower().Contains(request.QueryParams.SearchText) || 
                x.EmpJobDetail.FirstOrDefault().Department.DepartmentName.ToLower().Contains(request.QueryParams.SearchText) || 
                x.EmpJobDetail.FirstOrDefault().Section.SectionName.ToLower().Contains(request.QueryParams.SearchText) ||
                x.EmpJobDetail.FirstOrDefault().Designation.DesignationSetup.Name.ToLower().Contains(request.QueryParams.SearchText) ||
                String.IsNullOrEmpty(request.QueryParams.SearchText)))
                .Include(x => x.EmployeeType)
                .Include(x => x.EmpJobDetail)
                    .ThenInclude(ejd => ejd.Department)
                .Include(x => x.EmpJobDetail)
                    .ThenInclude(ejd => ejd.Designation)
                        .ThenInclude(ds => ds.DesignationSetup)
                .Include(x => x.EmpJobDetail)
                    .ThenInclude(ejd => ejd.Section)
                .Include(x => x.EmpPhotoSign)
                .Include(x => x.EmpPersonalInfo)
                    .ThenInclude(x => x.Gender);


            var totalCount = empBasicInfos.Count();
            empBasicInfos = empBasicInfos.OrderByDescending(x => x.Id).Skip((request.QueryParams.PageIndex - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var EmpBasicInfoDtos = _mapper.Map<List<EmpBasicInfoDto>>(empBasicInfos);

            var result = new PagedResult<EmpBasicInfoDto>(EmpBasicInfoDtos, totalCount, request.QueryParams.PageIndex, request.QueryParams.PageSize);


            //EmpBasicInfo = EmpBasicInfo.OrderByDescending(x => x.Id);

            //var EmpBasicInfoDtos = _mapper.Map<List<EmpBasicInfoDto>>(EmpBasicInfo);

            return result;
        }
    }
}