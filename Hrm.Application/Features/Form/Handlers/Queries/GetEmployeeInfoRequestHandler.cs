using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Form;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Form.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.Form.Handlers.Queries
{
    public class GetEmployeeInfoRequestHandler: IRequestHandler<GetEmployeeInfoRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEmployeeInfoRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetEmployeeInfoRequest request, CancellationToken cancellationToken)
        {
            var employee = await _unitOfWork.Repository<Hrm.Domain.EmpBasicInfo>().Where(x => x.IdCardNo == request.IdCardNo).FirstOrDefaultAsync();

            var employeeInfo = new EmployeeInfoForFormDto();
            if(employee == null)
            {
                employeeInfo.success = false;
                return employeeInfo;
            }

            employeeInfo.success = true;
            var empPersonalInfo = await _unitOfWork.Repository<Hrm.Domain.EmpPersonalInfo>().Where(x=>x.EmpId == employee.Id).FirstOrDefaultAsync();
            var empJobDetail = await _unitOfWork.Repository<Hrm.Domain.EmpJobDetail>().Where(x=>x.EmpId == employee.Id).FirstOrDefaultAsync();
            var designation = await _unitOfWork.Repository<Hrm.Domain.Designation>().Get((int)empJobDetail.DesignationId);


            employeeInfo.EmpId = employee.Id;
            employeeInfo.FirstName = employee.FirstName;
            employeeInfo.LastName = employee.LastName;
            employeeInfo.IdCardNo = employee.IdCardNo;
            employeeInfo.BirthDate = new DateTime(employee.DateOfBirth.Value.Year, employee.DateOfBirth.Value.Month, employee.DateOfBirth.Value.Day);

            if(empPersonalInfo!=null)
            {
                employeeInfo.BirthCertificateNo = empPersonalInfo.BirthRegNo.ToString();
                employeeInfo.FatherName = empPersonalInfo.FatherName;
                employeeInfo.MotherName = empPersonalInfo.MotherName;
            }

            if(empJobDetail!=null)
            {
                employeeInfo.JoiningDate = new DateTime(empJobDetail.JoiningDate.Value.Year, empJobDetail.JoiningDate.Value.Month, empJobDetail.JoiningDate.Value.Month);
            }

            if (designation != null)
            {
                employeeInfo.Designation = designation.DesignationName;

                var empPromotionInc = await _unitOfWork.Repository<Hrm.Domain.EmpPromotionIncrement>().Where(x => x.EmpId == employee.Id && x.UpdateDesignationId == designation.DesignationId && x.ApplicationStatus == true).OrderByDescending(x => x.ApproveDate).FirstOrDefaultAsync();
                if(empPromotionInc!=null)
                {
                    employeeInfo.CurrentDesignationJoiningDate = new DateTime(empPromotionInc.ApproveDate.Value.Year, empPromotionInc.ApproveDate.Value.Month, empPromotionInc.ApproveDate.Value.Day);
                } else
                {
                    employeeInfo.CurrentDesignationJoiningDate = employeeInfo.JoiningDate;
                }
            }


            return employeeInfo;
        }
    }
}
