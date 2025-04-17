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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Hrm.Application.DTOs.Result;

namespace Hrm.Application.Features.EmpBasicInfos.Handlers.Queries
{
    public class GetAllEmpBasicInfoRequestHandler : IRequestHandler<GetAllEmpBasicInfoRequest, PagedResult<EmpBasicInfoDto>>
    {

        private readonly IHrmRepository<EmpBasicInfo> _EmpBasicInfoRepository;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<EmpOtherResponsibility> _EmpOtherResponsibilityRepository;
        public GetAllEmpBasicInfoRequestHandler(IHrmRepository<EmpBasicInfo> EmpBasicInfoRepository, IMapper mapper, IHrmRepository<EmpOtherResponsibility> empOtherResponsibilityRepository)
        {
            _EmpBasicInfoRepository = EmpBasicInfoRepository;
            _mapper = mapper;
            _EmpOtherResponsibilityRepository = empOtherResponsibilityRepository;
        }

        public async Task<PagedResult<EmpBasicInfoDto>> Handle(GetAllEmpBasicInfoRequest request, CancellationToken cancellationToken)
        {

            IQueryable<EmpBasicInfo> empBasicInfos = _EmpBasicInfoRepository.FilterWithInclude(x => (x.IdCardNo.ToLower().Contains(request.QueryParams.SearchText) || 
                x.FirstName.ToLower().Contains(request.QueryParams.SearchText) || 
                x.LastName.ToLower().Contains(request.QueryParams.SearchText) || 
                x.EmpJobDetail.FirstOrDefault().Department.DepartmentName.ToLower().Contains(request.QueryParams.SearchText) || 
                x.EmpJobDetail.FirstOrDefault().Section.SectionName.ToLower().Contains(request.QueryParams.SearchText) ||
                x.EmpJobDetail.FirstOrDefault().Designation.DesignationSetup.Name.ToLower().Contains(request.QueryParams.SearchText) ||
                String.IsNullOrEmpty(request.QueryParams.SearchText)) &&
                (request.DepartmentId == 0 || x.EmpJobDetail.Any(x => x.DepartmentId == request.DepartmentId)) &&
                (request.SectionId == 0 || x.EmpJobDetail.Any(x => x.SectionId == request.SectionId)))
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


            IQueryable<EmpOtherResponsibility> empOtherResponsibility = _EmpOtherResponsibilityRepository.FilterWithInclude(x =>
                (x.EmpBasicInfo.IdCardNo.ToLower().Contains(request.QueryParams.SearchText) ||
                x.EmpBasicInfo.FirstName.ToLower().Contains(request.QueryParams.SearchText) ||
                x.EmpBasicInfo.LastName.ToLower().Contains(request.QueryParams.SearchText) ||
                x.EmpBasicInfo.EmpJobDetail.FirstOrDefault().Department.DepartmentName.ToLower().Contains(request.QueryParams.SearchText) ||
                x.EmpBasicInfo.EmpJobDetail.FirstOrDefault().Section.SectionName.ToLower().Contains(request.QueryParams.SearchText) ||
                x.EmpBasicInfo.EmpJobDetail.FirstOrDefault().Designation.DesignationSetup.Name.ToLower().Contains(request.QueryParams.SearchText) ||
                x.ResponsibilityType.Name.ToLower().Contains(request.QueryParams.SearchText) ||
                String.IsNullOrEmpty(request.QueryParams.SearchText)) &&
                    (request.DepartmentId == 0 || x.DepartmentId == request.DepartmentId) &&
                    (request.SectionId == 0 || x.SectionId == request.SectionId) && x.ServiceStatus == true)
                    .Include(x => x.Department)
                    .Include(x => x.Section)
                    .Include(x => x.Designation)
                        .ThenInclude(x => x.DesignationSetup)
                    .Include(x => x.EmpBasicInfo)
                    .Include(x => x.EmpBasicInfo)
                        .ThenInclude(x => x.EmpPersonalInfo);


            var empBasicInfoResultData = _mapper.Map<List<EmpBasicInfoDto>>(empBasicInfos);

            var empOtherResponsibilityResultData = await empOtherResponsibility.Select(x => new EmpBasicInfoDto
            {
                Id = x.EmpId ?? 0,
                IdCardNo = x.EmpBasicInfo.IdCardNo,
                FirstName = x.EmpBasicInfo.FirstName,
                LastName = x.EmpBasicInfo.LastName,
                DepartmentName = x.Department.DepartmentName,
                SectionName = x.Section.SectionName,
                DesignationName = x.ResponsibilityType != null && !string.IsNullOrEmpty(x.ResponsibilityType.Name)
                        ? $"{x.Designation.DesignationSetup.Name} ({x.ResponsibilityType.Name})"
                        : x.Designation.DesignationSetup.Name,
                EmpPhotoName = x.EmpBasicInfo.EmpPhotoSign.FirstOrDefault().PhotoUrl,
                UserStatus = true,
            }).ToListAsync(cancellationToken);

            var combinedResult = empBasicInfoResultData
                .Concat(empOtherResponsibilityResultData)
                .OrderBy(x => x.DepartmentName)
                    .ThenBy(x => x.SectionName)
                        .ThenBy(x => x.DesignationName)
                .ToList();


            var pagedResult = combinedResult
               .Skip((request.QueryParams.PageIndex - 1) * request.QueryParams.PageSize)
               .Take(request.QueryParams.PageSize)
               .ToList();

            var totalCount = await empBasicInfos.CountAsync(cancellationToken) + await empOtherResponsibility.CountAsync(cancellationToken);


            var result = new PagedResult<EmpBasicInfoDto>(pagedResult, totalCount, request.QueryParams.PageIndex, request.QueryParams.PageSize);



            return result;
        }
    }
}