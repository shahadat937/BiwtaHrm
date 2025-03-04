using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Reporting;
using Hrm.Application.Features.Reportings.AddressReporting.Requests.Queries;
using Hrm.Application.Features.Reportings.EmpInfoReporting.Gender.Requests.Queries;
using Hrm.Application.Models;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Reportings.AddressReporting.Handlers.Queries
{
    public class GetAddressReportingRequestHandler : IRequestHandler<GetAddressReportingRequest, PagedResult<EmpAddressReportingResultDto>>
    {

        private readonly IHrmRepository<EmpBasicInfo> _EmpBasicInfoRepository;
        public GetAddressReportingRequestHandler(IHrmRepository<EmpBasicInfo> EmpBasicInfoRepository)
        {
            _EmpBasicInfoRepository = EmpBasicInfoRepository;
        }

        public async Task<PagedResult<EmpAddressReportingResultDto>> Handle(GetAddressReportingRequest request, CancellationToken cancellationToken)
        {
            if (request.IsPresentAddress)
            {
                IQueryable<EmpBasicInfo> query = _EmpBasicInfoRepository.FilterWithInclude(x =>
                    (request.DepartmentId == 0 || x.EmpJobDetail.FirstOrDefault().DepartmentId == request.DepartmentId) &&
                    (request.SectionId == 0 || x.EmpJobDetail.FirstOrDefault().SectionId == request.SectionId) &&
                    (request.CountryId == 0 || x.EmpPresentAddress.FirstOrDefault().CountryId == request.CountryId) &&
                    (request.DivisionId == 0 || x.EmpPresentAddress.FirstOrDefault().DivisionId == request.DivisionId) &&
                    (request.DistrictId == 0 || x.EmpPresentAddress.FirstOrDefault().DistrictId == request.DistrictId) &&
                    (request.UpazilaId == 0 || x.EmpPresentAddress.FirstOrDefault().UpazilaId == request.UpazilaId))
                    .Include(x => x.EmpJobDetail)
                        .ThenInclude(x => x.Department)
                    .ThenInclude(x => x.EmpJobDetail)
                        .ThenInclude(x => x.Section)
                    .ThenInclude(x => x.EmpJobDetail)
                        .ThenInclude(x => x.Designation)
                            .ThenInclude(ds => ds.DesignationSetup)
                    .Include(x => x.EmpPresentAddress)
                        .ThenInclude(x => x.Country)
                    .Include(x => x.EmpPresentAddress)
                        .ThenInclude(x => x.Division)
                    .Include(x => x.EmpPresentAddress)
                        .ThenInclude(x => x.District)
                    .Include(x => x.EmpPresentAddress)
                        .ThenInclude(x => x.Thana)
                    .OrderByDescending(x => x.EmpPresentAddress.FirstOrDefault().CountryId.HasValue)
                        .ThenBy(x => x.EmpPresentAddress.FirstOrDefault().Country.CountryName)
                        .ThenBy(x => x.EmpPresentAddress.FirstOrDefault().Division.DivisionName)
                        .ThenBy(x => x.EmpPresentAddress.FirstOrDefault().District.DistrictName)
                        .ThenBy(x => x.EmpPresentAddress.FirstOrDefault().Thana.ThanaName);

                var totalCount = await query.CountAsync(cancellationToken);

                query = query
                    .Skip((request.QueryParams.PageIndex - 1) * request.QueryParams.PageSize)
                    .Take(request.QueryParams.PageSize);

                var resultData = await query
                        .Select(x => new EmpAddressReportingResultDto
                        {
                            IdCardNo = x.IdCardNo ?? "",
                            EmpName = (x.FirstName + " " + x.LastName) ?? "",
                            DepartmentName = x.EmpJobDetail.FirstOrDefault().Department.DepartmentName ?? "",
                            SectionName = x.EmpJobDetail.FirstOrDefault().Section.SectionName ?? "",
                            DesignationName = x.EmpJobDetail.FirstOrDefault().Designation.DesignationSetup.Name ?? "",
                            CountryName = x.EmpPresentAddress.FirstOrDefault().Country.CountryName ?? "",
                            DivisionName = x.EmpPresentAddress.FirstOrDefault().Division.DivisionName ?? "",
                            DistricName = x.EmpPresentAddress.FirstOrDefault().District.DistrictName ?? "",
                            UpazilaName = x.EmpPresentAddress.FirstOrDefault().Upazila.UpazilaName ?? "",
                            ContactNumber = x.EmpPersonalInfo.FirstOrDefault().MobileNumber ?? "",
                            Email = x.EmpPersonalInfo.FirstOrDefault().Email ?? "",
                            Status = x.EmpJobDetail.FirstOrDefault().ServiceStatus ?? false,

                        })
                        .ToListAsync(cancellationToken);

                var result = new PagedResult<EmpAddressReportingResultDto>(resultData, totalCount, request.QueryParams.PageIndex, request.QueryParams.PageSize);


                return result;
            }
            else
            {
                IQueryable<EmpBasicInfo> query = _EmpBasicInfoRepository.FilterWithInclude(x =>
                    (request.DepartmentId == 0 || x.EmpJobDetail.FirstOrDefault().DepartmentId == request.DepartmentId) &&
                    (request.SectionId == 0 || x.EmpJobDetail.FirstOrDefault().SectionId == request.SectionId) &&
                    (request.CountryId == 0 || x.EmpPermanentAddress.FirstOrDefault().CountryId == request.CountryId) &&
                    (request.DivisionId == 0 || x.EmpPermanentAddress.FirstOrDefault().DivisionId == request.DivisionId) &&
                    (request.DistrictId == 0 || x.EmpPermanentAddress.FirstOrDefault().DistrictId == request.DistrictId) &&
                    (request.UpazilaId == 0 || x.EmpPermanentAddress.FirstOrDefault().UpazilaId == request.UpazilaId))
                    .Include(x => x.EmpJobDetail)
                        .ThenInclude(x => x.Department)
                    .ThenInclude(x => x.EmpJobDetail)
                        .ThenInclude(x => x.Section)
                    .ThenInclude(x => x.EmpJobDetail)
                        .ThenInclude(x => x.Designation)
                            .ThenInclude(ds => ds.DesignationSetup)
                    .Include(x => x.EmpPermanentAddress)
                        .ThenInclude(x => x.Country)
                    .Include(x => x.EmpPermanentAddress)
                        .ThenInclude(x => x.Division)
                    .Include(x => x.EmpPermanentAddress)
                        .ThenInclude(x => x.District)
                    .Include(x => x.EmpPermanentAddress)
                        .ThenInclude(x => x.Thana)
                    .OrderByDescending(x => x.EmpPermanentAddress.FirstOrDefault().CountryId.HasValue)
                        .ThenBy(x => x.EmpPermanentAddress.FirstOrDefault().Country.CountryName)
                        .ThenBy(x => x.EmpPermanentAddress.FirstOrDefault().Division.DivisionName)
                        .ThenBy(x => x.EmpPermanentAddress.FirstOrDefault().District.DistrictName)
                        .ThenBy(x => x.EmpPermanentAddress.FirstOrDefault().Thana.ThanaName);

                var totalCount = await query.CountAsync(cancellationToken);

                query = query
                    .Skip((request.QueryParams.PageIndex - 1) * request.QueryParams.PageSize)
                    .Take(request.QueryParams.PageSize);

                var resultData = await query
                        .Select(x => new EmpAddressReportingResultDto
                        {
                            IdCardNo = x.IdCardNo ?? "",
                            EmpName = (x.FirstName + " " + x.LastName) ?? "",
                            DepartmentName = x.EmpJobDetail.FirstOrDefault().Department.DepartmentName ?? "",
                            SectionName = x.EmpJobDetail.FirstOrDefault().Section.SectionName ?? "",
                            DesignationName = x.EmpJobDetail.FirstOrDefault().Designation.DesignationSetup.Name ?? "",
                            CountryName = x.EmpPermanentAddress.FirstOrDefault().Country.CountryName ?? "",
                            DivisionName = x.EmpPermanentAddress.FirstOrDefault().Division.DivisionName ?? "",
                            DistricName = x.EmpPermanentAddress.FirstOrDefault().District.DistrictName ?? "",
                            UpazilaName = x.EmpPermanentAddress.FirstOrDefault().Upazila.UpazilaName ?? "",
                            ContactNumber = x.EmpPersonalInfo.FirstOrDefault().MobileNumber ?? "",
                            Email = x.EmpPersonalInfo.FirstOrDefault().Email ?? "",
                            Status = x.EmpJobDetail.FirstOrDefault().ServiceStatus ?? false,

                        })
                        .ToListAsync(cancellationToken);

                var result = new PagedResult<EmpAddressReportingResultDto>(resultData, totalCount, request.QueryParams.PageIndex, request.QueryParams.PageSize);


                return result;
            }
        }
    }
}
