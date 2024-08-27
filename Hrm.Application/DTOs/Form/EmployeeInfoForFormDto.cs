using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Form
{
    public class EmployeeInfoForFormDto
    {
        public int EmpId { get; set; }
        public string? IdCardNo { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        public string? BirthCertificateNo { get; set; }
        public string? Designation {  get; set; }
        public string? WorkPlace { get; set; }
        public DateTime? JoiningDate { get; set; }
        public DateTime? CurrentDesignationJoiningDate { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? EducationalQualification { get; set; }
        public string? SpecialTraining { get; set; }

    }
}
