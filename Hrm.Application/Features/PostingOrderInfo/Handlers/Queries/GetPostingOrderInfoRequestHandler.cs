using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.PostingOrderInfo;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.PostingOrderInfo.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.PostingOrderInfo.Handlers.Queries
{
    public class GetPostingOrderInfoRequestHandler : IRequestHandler<GetPostingOrderInfoRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.PostingOrderInfo> _PostingOrderInfoRepository;
       // private readonly IHrmRepository<Hrm.Domain.Country> _countryRepository;
       private readonly IHrmRepository<Hrm.Domain.Department> _departmentRepository;
        private readonly IHrmRepository<Hrm.Domain.SubBranch> _subBranch;
        private readonly IHrmRepository<Hrm.Domain.SubDepartment> _subDepartment;
        private readonly IHrmRepository<Hrm.Domain.OfficeBranch> _officeBranch;

        private readonly IMapper _mapper;
        public GetPostingOrderInfoRequestHandler(IHrmRepository<Hrm.Domain.PostingOrderInfo> PostingOrderInfoRepository, IMapper mapper, IHrmRepository<Domain.Country> countryRepository, IHrmRepository<Domain.Department> departmentRepository, IHrmRepository<Domain.SubBranch> subBranch, IHrmRepository<Domain.SubDepartment> subDepartment, IHrmRepository<Domain.OfficeBranch> officeBranch)
        {
            _PostingOrderInfoRepository = PostingOrderInfoRepository;
            _mapper = mapper;
            _departmentRepository = departmentRepository;
            _subBranch = subBranch;
            _subDepartment = subDepartment;
            _officeBranch = officeBranch;
            //  _countryRepository = countryRepository;
        }

        public async Task<object> Handle(GetPostingOrderInfoRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.PostingOrderInfo> PostingOrderInfos = _PostingOrderInfoRepository.FilterWithInclude(x => true);
            PostingOrderInfos = PostingOrderInfos.OrderByDescending(x => x.PostingOrderInfoId);

            var PostingOrderInfoDtos = new List<PostingOrderInfoDto>();

            foreach (var PostingOrderInfo in PostingOrderInfos)
            {
                var PostingOrderInfoDto = _mapper.Map<PostingOrderInfoDto>(PostingOrderInfo);
                // var countryName = await GetCountryName(PostingOrderInfo.CountryId);
                var departmentName = await GetDepartmentName(PostingOrderInfo.DepartmentId);
                var subBranchName = await GetSubBranchName(PostingOrderInfo.SubBranchId);
                var subDepartmentName = await GetSubDepartmentName(PostingOrderInfo.SubDepartmentId);
                var officeBranchName = await GetSOfficeBranchName(PostingOrderInfo.OfficeBranchId);
                
              //  PostingOrderInfoDto.CountryName = countryName;
                PostingOrderInfoDto.DepartmentName = departmentName;
                PostingOrderInfoDto.SubBranchName = subBranchName;

                PostingOrderInfoDto.SubBranchName = subDepartmentName;
                PostingOrderInfoDto.OfficeBranchName = officeBranchName;

                PostingOrderInfoDtos.Add(PostingOrderInfoDto);
            }

            return PostingOrderInfoDtos;
        }

        //private async Task<string?> GetCountryName(int? countryId)
        //{
        //    if (countryId.HasValue)
        //    {
        //        //var country = await _countryRepository.Get(countryId.Value);
        //        //return country?.CountryName;
        //    }
        //    return null;
        //}
        private async Task<string?> GetDepartmentName(int? departmentId)
        {
            if (departmentId.HasValue)
            {
                var department = await _departmentRepository.Get(departmentId.Value);
                return department?.DepartmentName;
            }
            return null;
        }
        private async Task<string?> GetSubBranchName(int? subBranchId)
        {
            if (subBranchId.HasValue)
            {
                var subBranch = await _subBranch.Get(subBranchId.Value);
                return subBranch?.SubBranchName;
            }
            return null;
        }
        private async Task<string?> GetSubDepartmentName(int? subDepartmentId)
        {
            if (subDepartmentId.HasValue)
            {
                var subDepartment = await _subDepartment.Get(subDepartmentId.Value);
                return subDepartment?.SubDepartmentName;
            }
            return null;
        }
        private async Task<string?> GetSOfficeBranchName(int? officeBranchId)
        {
            if (officeBranchId.HasValue)
            {
                var officeBranch = await _officeBranch.Get(officeBranchId.Value);
                return officeBranch?.BranchName;
            }
            return null;
        }
    }
}
