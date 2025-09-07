using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpBasicInfo;
using Hrm.Application.Features.EmpBasicInfos.Requests.Queries;
using Hrm.Application.Models;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Hrm.Application.Features.EmpBasicInfos.Handlers.Queries
{
    public class GetAllEmpBasicInfoNewRequestHandler : IRequestHandler<GetAllEmpBasicInfoNewRequest, PagedResult<EmpBasicInfoDto>>
    {

        private readonly IHrmRepository<EmpBasicInfo> _EmpBasicInfoRepository;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<EmpOtherResponsibility> _EmpOtherResponsibilityRepository;
        private readonly IHrmRepository<Hrm.Domain.Department> _departmentRepository;



        public GetAllEmpBasicInfoNewRequestHandler(IHrmRepository<EmpBasicInfo> EmpBasicInfoRepository, IMapper mapper, IHrmRepository<EmpOtherResponsibility> empOtherResponsibilityRepository, IHrmRepository<Hrm.Domain.Department> departmentRepository)
        {
            _EmpBasicInfoRepository = EmpBasicInfoRepository;
            _mapper = mapper;
            _EmpOtherResponsibilityRepository = empOtherResponsibilityRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task<PagedResult<EmpBasicInfoDto>> Handle(GetAllEmpBasicInfoNewRequest request, CancellationToken cancellationToken)
        {
            // Get all department IDs (including children recursively)
            HashSet<int> departmentIds = new HashSet<int>();
            if (request.DepartmentId > 0)
            {
                var allDepartments = (await _departmentRepository.GetAll()).ToList();
                departmentIds = GetAllChildDepartmentIds(request.DepartmentId, allDepartments).ToHashSet();
            }

            IQueryable<EmpBasicInfo> empBasicInfos = _EmpBasicInfoRepository.FilterWithInclude(x =>
                (x.IdCardNo.ToLower().Contains(request.QueryParams.SearchText) ||
                 x.FirstName.ToLower().Contains(request.QueryParams.SearchText) ||
                 x.LastName.ToLower().Contains(request.QueryParams.SearchText) ||
                 x.EmpJobDetail.FirstOrDefault().Department.DepartmentName.ToLower().Contains(request.QueryParams.SearchText) ||
                 x.EmpJobDetail.FirstOrDefault().Section.SectionName.ToLower().Contains(request.QueryParams.SearchText) ||
                 x.EmpJobDetail.FirstOrDefault().Designation.DesignationSetup.Name.ToLower().Contains(request.QueryParams.SearchText) ||
                 String.IsNullOrEmpty(request.QueryParams.SearchText)) &&
                (request.DepartmentId == 0 || x.EmpJobDetail.Any(ejd => departmentIds.Contains(ejd.DepartmentId ?? 0))) &&
                (request.SectionId == 0 || x.EmpJobDetail.Any(ejd => ejd.SectionId == request.SectionId)))
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


            //  EmpOtherResponsibility Query
            IQueryable<EmpOtherResponsibility> empOtherResponsibility = _EmpOtherResponsibilityRepository.FilterWithInclude(x =>
                (x.EmpBasicInfo.IdCardNo.ToLower().Contains(request.QueryParams.SearchText) ||
                 x.EmpBasicInfo.FirstName.ToLower().Contains(request.QueryParams.SearchText) ||
                 x.EmpBasicInfo.LastName.ToLower().Contains(request.QueryParams.SearchText) ||
                 x.EmpBasicInfo.EmpJobDetail.FirstOrDefault().Department.DepartmentName.ToLower().Contains(request.QueryParams.SearchText) ||
                 x.EmpBasicInfo.EmpJobDetail.FirstOrDefault().Section.SectionName.ToLower().Contains(request.QueryParams.SearchText) ||
                 x.EmpBasicInfo.EmpJobDetail.FirstOrDefault().Designation.DesignationSetup.Name.ToLower().Contains(request.QueryParams.SearchText) ||
                 x.ResponsibilityType.Name.ToLower().Contains(request.QueryParams.SearchText) ||
                 String.IsNullOrEmpty(request.QueryParams.SearchText)) &&
                (request.DepartmentId == 0 || departmentIds.Contains(x.DepartmentId ?? 0)) &&
                (request.SectionId == 0 || x.SectionId == request.SectionId) && x.ServiceStatus == true)
                .Include(x => x.Department)
                .Include(x => x.Section)
                .Include(x => x.Designation)
                    .ThenInclude(x => x.DesignationSetup)
                .Include(x => x.EmpBasicInfo)
                .Include(x => x.EmpBasicInfo)
                    .ThenInclude(x => x.EmpPersonalInfo);

            // Map & Combine
            var empBasicInfoResultData = _mapper.Map<List<EmpBasicInfoDto>>(empBasicInfos);

            var empOtherResponsibilityResultData = await empOtherResponsibility.Select(x => new EmpBasicInfoDto
            {
                Id = x.EmpId ?? 0,
                IdCardNo = x.EmpBasicInfo.IdCardNo,
                FirstName = x.EmpBasicInfo.FirstName,
                LastName = x.EmpBasicInfo.LastName,
                DepartmentName = x.Department.DepartmentName,
                DepartmentId = x.DepartmentId,
                SectionName = x.Section.SectionName,
                SectionId = x.SectionId,
                DesignationName = x.ResponsibilityType != null && !string.IsNullOrEmpty(x.ResponsibilityType.Name)
                        ? $"{x.Designation.DesignationSetup.Name} ({x.ResponsibilityType.Name})"
                        : x.Designation.DesignationSetup.Name,
                DesignationId = x.DesignationId,
                AdditionalResponsibilityId = x.ResponsibilityTypeId,
                AdditionalResponsibilityName = x.ResponsibilityType.Name,
                EmpPhotoName = x.EmpBasicInfo.EmpPhotoSign.FirstOrDefault().PhotoUrl,
                UserStatus = true,
                IsAdditionalDesignation = true,
                JoiningDate = x.StartDate
            }).ToListAsync(cancellationToken);


            var combinedResult = empBasicInfoResultData
                .Concat(empOtherResponsibilityResultData)
                .OrderByDescending(x => x.DepartmentId.HasValue)
                    .ThenBy(x => x.DepartmentName)
                        .ThenBy(x => x.SectionName)
                            .ThenBy(x => x.DesignationName)
                .ToList();

            //  Pagination
            var totalCount = combinedResult.Count;

            var pagedResult = combinedResult
                .Skip((request.QueryParams.PageIndex - 1) * request.QueryParams.PageSize)
                .Take(request.QueryParams.PageSize)
                .ToList();

            return new PagedResult<EmpBasicInfoDto>(pagedResult, totalCount, request.QueryParams.PageIndex, request.QueryParams.PageSize);
        }

        // 🔹 Helper Method for Recursive Department IDs
        private List<int> GetAllChildDepartmentIds(int departmentId, List<Hrm.Domain.Department> departments)
        {
            var result = new HashSet<int> { departmentId };

            void Traverse(int parentId)
            {
                foreach (var child in departments.Where(d => d.UpperDepartmentId == parentId))
                {
                    if (result.Add(child.DepartmentId)) // only adds if not already present
                    {
                        Traverse(child.DepartmentId);
                    }
                }
            }

            Traverse(departmentId);

            return result.ToList();
        }

    }
}