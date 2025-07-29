using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Country
{
    public class CreateCountryDto : ICountryDto
    {
        public int CountryId { get; set; }
        public string? CountryName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
    }
}