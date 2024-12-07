using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.AttendanceDevice.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using Hrm.Application.DTOs.AttDevice;

namespace Hrm.Application.Features.AttendanceDevice.Services
{
    public class AttendanceDevice : IAttendanceDevice
    {
        private readonly IUnitOfWork _unitOfWork;
        public AttendanceDevice(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddUser(string DeviceSN, string Pin, string Name, string Passwd = "", int GroupId = 1, int Privilage = 0, int Verify = 0)
        {
            bool IsAuthorizedDevice = await _unitOfWork.Repository<Hrm.Domain.AttDevices>().Where(x => x.SN == DeviceSN && x.Status == true).AnyAsync();

            if(!IsAuthorizedDevice)
            {
                return false;
            }

            var deviceCommand = new Domain.AttDeviceCommands();
            deviceCommand.SN = DeviceSN;
            deviceCommand.Command = $"DATA UPDATE USERINFO PIN={Pin}\tName={Name}\tPri={Privilage}\tPasswd={Passwd}\tGrp={GroupId}\tVerify={Verify}";
            
            await _unitOfWork.Repository<Hrm.Domain.AttDeviceCommands>().Add(deviceCommand);
            await _unitOfWork.Save();

            return true;

        }
        public async Task<bool> DeleteUser(string DeviceSN, string Pin)
        {
            bool IsAuthorizedDevice = await _unitOfWork.Repository<Hrm.Domain.AttDevices>().Where(x => x.SN == DeviceSN).AnyAsync();
            if(!IsAuthorizedDevice)
            {
                return false;
            }

            var deviceCommand = new Domain.AttDeviceCommands();
            deviceCommand.SN = DeviceSN;
            deviceCommand.Command = $"DATA DELETE USERINFO PIN={Pin}";
            await _unitOfWork.Repository<Hrm.Domain.AttDeviceCommands>().Add(deviceCommand);
            await _unitOfWork.Save();

            return true;
        }

        public async Task<int> RebootDevice(string DeviceSN)
        {
            bool IsAuthorizedDevice = await _unitOfWork.Repository<Hrm.Domain.AttDevices>().Where(x => x.SN == DeviceSN && x.Status == true).AnyAsync();

            if(!IsAuthorizedDevice)
            {
                return -1;
            }

            var deviceCommand = new Domain.AttDeviceCommands();
            deviceCommand.SN = DeviceSN;
            deviceCommand.Command = "REBOOT";

            await _unitOfWork.Repository<Hrm.Domain.AttDeviceCommands>().Add(deviceCommand);
            await _unitOfWork.Save();
            return deviceCommand.Id;
        }

        public async Task<List<AttPunchDto>> ParseDeviceAttendance(string rawAttendance)
        {

            List<string> records = rawAttendance.Split(new[] {"\r\n", "\n"}, StringSplitOptions.None).ToList();

            List<AttPunchDto> result = new List<AttPunchDto>();

            foreach (var record in records)
            {
                var punch = new AttPunchDto();
                List<string> fields = record.Split(new[] {"\t"}, StringSplitOptions.None).ToList();
                punch.IdCardNo = fields[0];

                if (fields.Count < 4)
                    continue;

                // separate datetime
                DateTime tempDateTime = DateTime.Parse(fields[1]);
                punch.PunchDate = DateOnly.FromDateTime(tempDateTime);
                punch.PunchTime = TimeOnly.FromDateTime(tempDateTime);
                punch.PunchState = Int32.Parse(fields[2]);
                punch.PunchType = Int32.Parse(fields[3]);
                result.Add(punch);

            }

            return result;
        }

        public async Task<bool> CustomCommand(string DeviceSN, string Command)
        {
            bool IsAuthorizedDevice = await _unitOfWork.Repository<Hrm.Domain.AttDevices>().Where(x => x.SN == DeviceSN && x.Status == true).AnyAsync();

            if(!IsAuthorizedDevice)
            {
                return false;
            }

            var deviceCommand = new Domain.AttDeviceCommands();
            deviceCommand.SN = DeviceSN;
            deviceCommand.Command = Command;

            await _unitOfWork.Repository<Hrm.Domain.AttDeviceCommands>().Add(deviceCommand);
            await _unitOfWork.Save();

            return true;
        }

        public async Task<bool> UpdateUserPic(string DeviceSN, string Pin, string UserPic)
        {
            bool IsAuthorizedDevice = await _unitOfWork.Repository<Hrm.Domain.AttDevices>().Where(x => x.SN == DeviceSN && x.Status == true).AnyAsync();

            if(!IsAuthorizedDevice)
            {
                return false;
            }

            var deviceCommand = new Domain.AttDeviceCommands();
            deviceCommand.SN = DeviceSN;
            deviceCommand.Command = $"DATA UPDATE USERPIC PIN={Pin}\tContent={UserPic}";

            await _unitOfWork.Repository<Hrm.Domain.AttDeviceCommands>().Add(deviceCommand);
            await _unitOfWork.Save();

            return true;
        }

        public async Task<int> EnrollFingerPrint(string DeviceSN, string Pin,int FID=5)
        {
            bool IsAuthorizedDevice = await _unitOfWork.Repository<Hrm.Domain.AttDevices>().Where(x => x.SN == DeviceSN && x.Status == true).AnyAsync();

            if (!IsAuthorizedDevice)
            {
                return -1;
            }

            var deviceCommand = new Domain.AttDeviceCommands();
            deviceCommand.SN = DeviceSN;
            deviceCommand.Command = $"ENROLL_FP PIN={Pin}\tFID={FID}\tRETRY=3\tOVERWRITE=1";

            await _unitOfWork.Repository<Hrm.Domain.AttDeviceCommands>().Add(deviceCommand);
            await _unitOfWork.Save();

            return deviceCommand.Id;
        }
    }
}
