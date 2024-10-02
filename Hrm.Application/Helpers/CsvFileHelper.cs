using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Hrm.Application.DTOs.Attendance;
using Hrm.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Helpers
{
    public class CreateAttendanceDtoCsvMap: ClassMap<CreateAttendanceDto>
    {
        public CreateAttendanceDtoCsvMap()
        {
            Map(at => at.AttendanceDate).Name("Date").TypeConverter<CustomDateConverter>();
            Map(at => at.AttendanceTypeId).Name("attendanceType").Optional();
            Map(at => at.EmpId).Name("EmpId");
            Map(at => at.OfficeId).Name("OfficeId").Optional();
            Map(at => at.OfficeBranchId).Name("OfficeBranchId").Optional();
            Map(at => at.ShiftId).Name("ShiftId").Optional();
            Map(at => at.DayTypeId).Name("DayTypeId").Optional();
            Map(at => at.InTime).Name("InTime").Optional();
            Map(at => at.OutTime).Name("OutTime").Optional();
            Map(at => at.BreakTime).Name("BreakTime").Optional();
            Map(at => at.ResumeTime).Name("ResumeTime").Optional();
            Map(at => at.AttendanceStatusId).Name("StatusId").Optional();
            Map(at => at.Remark).Name("Remark").Optional();
        }
    }


    public class CustomDateConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            // Example: Parsing different date formats
            string[] formats = { "dd-MM-yyyy", "d-M-yyyy","dd/MM/yyyy","d/M/yyyy" };
            if (DateOnly.TryParseExact(text, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly date))
            {
                return date;
            }

            return null;
        }
    }


    public class CsvFileHelper
    {

        public static async Task<List<CreateAttendanceDto>> GetRecords(string filePath)
        {

            List<CreateAttendanceDto> records = new List<CreateAttendanceDto>();

             using (var streamReader = new StreamReader(filePath))
            using (var csv = new CsvReader(streamReader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<CreateAttendanceDtoCsvMap>();

                var asyncRecord = csv.GetRecordsAsync<CreateAttendanceDto>();
                await foreach (var record in asyncRecord)
                {
                    records.Add(record);
                }


            }

            return records;
        }
    }
}
