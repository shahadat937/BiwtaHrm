using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.District;
using Hrm.Application.DTOs.Division;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.District.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.District.Handlers.Queries
{
    public class GetDistrictRequestHandler : IRequestHandler<GetDistrictRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.District> _DistrictRepository;
        private readonly IHrmRepository<Hrm.Domain.Division> _DivisionRepository;
        private readonly IMapper _mapper;
        public GetDistrictRequestHandler(IHrmRepository<Hrm.Domain.District> DistrictRepository, IHrmRepository<Hrm.Domain.Division> DivisionRepository, IMapper mapper)
        {
            _DistrictRepository = DistrictRepository;
            _DivisionRepository = DivisionRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetDistrictRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Fetch District groups from repository
                IQueryable<Hrm.Domain.District> districts = _DistrictRepository.FilterWithInclude(x => true);

                // Order District groups by descending order
                districts = districts.OrderByDescending(x => x.DistrictId);

                // Map the District blood groups to DistrictDto
                var districtDtos = _mapper.Map<List<DistrictDto>>(await districts.ToListAsync()); // Materialize the query here

                // Iterate over each DistrictDto and update DivisionName
                foreach (var districtDto in districtDtos)
                {
                    var divisionName = await GetDivisionName(districtDto.DivisionId);
                    districtDto.DivisionName = divisionName;
                }

                return districtDtos;
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        private async Task<string?> GetDivisionName(int? divisionId)
        {
            if (divisionId.HasValue)
            {
                try
                {
                    var division = await _DivisionRepository.Get(divisionId.Value);
                    return division?.DivisionName;
                }
                catch (Exception ex)
                {
                    // Handle exceptions here
                    Console.WriteLine($"An error occurred while fetching division: {ex.Message}");
                    return null;
                }
            }
            return null;
        }
    }
    }
