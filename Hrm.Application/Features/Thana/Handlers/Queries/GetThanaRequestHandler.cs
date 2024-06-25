using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Thana;
using Hrm.Application.DTOs.Upazila;
using Hrm.Application.Features.Thana.Requests.Queries;
using Hrm.Application.Features.Upazila.Requests.Queries;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Thana.Handlers.Queries
{
    public class GetThanaRequestHandler : IRequestHandler<GetThanaRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.Thana> _ThanaRepository;
        private readonly IHrmRepository<Hrm.Domain.Upazila> _UpazilaRepository;
        private readonly IMapper _mapper;
        public GetThanaRequestHandler(IHrmRepository<Hrm.Domain.Thana> ThanaRepository, IHrmRepository<Hrm.Domain.Upazila> UpazilaRepository, IMapper mapper)
        {
            _ThanaRepository = ThanaRepository;
            _UpazilaRepository = UpazilaRepository;
            _mapper = mapper;
        }

        //public async Task<object> Handle(GetThanaRequest request, CancellationToken cancellationToken)
        //{
        //    //IQueryable<Hrm.Domain.Thana> Thana = _ThanaRepository.Where(x => true);

        //    //var ThanaDtos = _mapper.Map<List<ThanaDto>>(Thana);

        //    //return ThanaDtos;



        //    IQueryable<Hrm.Domain.Thana> Thana = _ThanaRepository.FilterWithInclude(x => true).OrderByDescending(x=>x.ThanaId);

        //    // Use Task.Run to offload the synchronous operation to a background thread
        //    var ThanaDtos = await Task.Run(() => _mapper.Map<List<ThanaDto>>(Thana));

        //    return ThanaDtos;
        //}
        public async Task<object> Handle(GetThanaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Fetch District groups from repository
                IQueryable<Hrm.Domain.Thana> Thanas = _ThanaRepository.FilterWithInclude(x => true);

                // Order District groups by descending order
                Thanas = Thanas.OrderByDescending(x => x.ThanaId);

                // Map the District blood groups to DistrictDto
                var ThanaDtos = _mapper.Map<List<ThanaDto>>(await Thanas.ToListAsync()); // Materialize the query here

                // Iterate over each DistrictDto and update DivisionName
                foreach (var ThanaDto in ThanaDtos)
                {
                    var UpazilaName = await GetUpazilaName(ThanaDto.UpazilaId);
                    ThanaDto.UpazilaName = UpazilaName;
                }

                return ThanaDtos;
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        private async Task<string?> GetUpazilaName(int? UpazilaId)
        {
            if (UpazilaId.HasValue)
            {
                try
                {
                    var Upazila = await _UpazilaRepository.Get(UpazilaId.Value);
                    return Upazila?.UpazilaName;
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
