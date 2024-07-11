using CsvHelper;
using CsvHelper.Configuration;
using Hrm.Application.DTOs.Attendance;
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
            Map(at => at.EmpId).Name("EmpId");
            Map(at => at.AttendanceDate).Name("AttendanceDate");
            Map(at => at.InTime).Name("InTime");
            Map(at => at.OutTime).Name("OutTime");
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

                await foreach(var record in asyncRecord)
                {
                    records.Add(record);
                }
            }

            return records;
        }
    }
}
