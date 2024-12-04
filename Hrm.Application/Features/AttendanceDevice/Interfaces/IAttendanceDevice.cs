using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.DTOs.AttDevice;

namespace Hrm.Application.Features.AttendanceDevice.Interfaces
{
    public interface IAttendanceDevice
    {
        Task<bool> AddUser(string DeviceSN, string Pin, string Name, string Passwd="", int GroupId=1, int Privilage = 0, int Verifty = 0);
        Task<bool> DeleteUser(string DeviceSN, string Pin);
        Task<List<AttPunchDto>> ParseDeviceAttendance(string rawAttendance);
        Task<int> RebootDevice(string DeviceSN);
        Task<bool> CustomCommand(string DeviceSN, string Command);
        Task<bool> UpdateUserPic(string DeviceSN, string Pin, string UserPic);
        Task<int> EnrollFingerPrint(string DeviceSN, string Pin, int FID);
    }
}
