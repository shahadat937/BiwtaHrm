using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.District;
using Hrm.Application.DTOs.Upazila;
using Hrm.Application.Features.District.Requests.Queries;
using Hrm.Application.Features.Upazila.Requests.Queries;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Upazila.Handlers.Queries
{
    public class GetUpazilaRequestHandler : IRequestHandler<GetUpazilaRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.Upazila> _UpazilaRepository;
        private readonly IHrmRepository<Hrm.Domain.District> _DistrictRepository;
        private readonly IMapper _mapper;
        public GetUpazilaRequestHandler(IHrmRepository<Hrm.Domain.Upazila> UpazilaRepository, IHrmRepository<Hrm.Domain.District> DistrictRepository, IMapper mapper)
        {
            _UpazilaRepository = UpazilaRepository;
            _DistrictRepository = DistrictRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetUpazilaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Fetch District groups from repository
                IQueryable<Hrm.Domain.Upazila> Upazilas = _UpazilaRepository.FilterWithInclude(x => true);

                // Order District groups by descending order
                Upazilas = Upazilas.OrderByDescending(x => x.DistrictId);

                // Map the District blood groups to DistrictDto
                var UpazilaDtos = _mapper.Map<List<UpazilaDto>>(await Upazilas.ToListAsync()); // Materialize the query here

                // Iterate over each DistrictDto and update DivisionName
                foreach (var UpazilaDto in UpazilaDtos)
                {
                    var DistrictName = await GetDistrictName(UpazilaDto.DistrictId);
                    UpazilaDto.DistrictName = DistrictName;
                }

                return UpazilaDtos;
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        private async Task<string?> GetDistrictName(int? DistrictId)
        {
            if (DistrictId.HasValue)
            {
                try
                {
                    var division = await _DistrictRepository.Get(DistrictId.Value);
                    return division?.DistrictName;
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
