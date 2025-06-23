using AutoMapper;
using Hrm.Application.Contracts.Identity;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpShiftAssign;
using Hrm.Application.Features.EmpBasicInfos.Requests.Commands;
using Hrm.Application.Features.EmpBasicInfos.Requests.Commands;
using Hrm.Application.Features.SiteSettings.Requests.Queries;
using Hrm.Application.Models.Identity;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpBasicInfos.Handlers.Commands
{
    public class CreateImportedEmpBasicInfoCommandHandler : IRequestHandler<CreateImportedEmpBasicInfoCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<EmpBasicInfo> _EmpBasicInfoRepository;
        private readonly IHrmRepository<EmpShiftAssign> _EmpShiftAssignRepository;
        private readonly IAuthService _authenticationService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<SiteSetting> _SiteSettingRepository;

        public CreateImportedEmpBasicInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<EmpBasicInfo> EmpBasicInfoRepository, IHrmRepository<EmpShiftAssign> empShiftAssignRepository, IAuthService authenticationService, IHrmRepository<SiteSetting> siteSettingRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _EmpBasicInfoRepository = EmpBasicInfoRepository;
            _EmpShiftAssignRepository = empShiftAssignRepository;
            _authenticationService = authenticationService;
            _SiteSettingRepository = siteSettingRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateImportedEmpBasicInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var successCount = 0;
            var errorCount = 0;

            foreach (var item in request.EmpBasicInfoDtos)
            {

                var IdCardNo = await _EmpBasicInfoRepository.FindOneAsync(x => x.IdCardNo == item.IdCardNo);

                if (IdCardNo != null)
                {
                    errorCount++;
                }

                else
                {
                    var EmpBasicInfoDto = _mapper.Map<EmpBasicInfo>(item);
                    EmpBasicInfoDto.UserStatus = true;

                    await _unitOfWork.Repository<EmpBasicInfo>().Add(EmpBasicInfoDto);

                    await _unitOfWork.Save();

                    successCount++;

                    var empShiftAssignDto = new CreateEmpShiftAssignDto
                    {
                        Id = 0,
                        EmpId = EmpBasicInfoDto.Id,
                        ShiftId = item.ShiftId,
                        IsActive = true
                    };
                    var empShiftAssigned = _mapper.Map<EmpShiftAssign>(empShiftAssignDto);
                    await _unitOfWork.Repository<EmpShiftAssign>().Add(empShiftAssigned);

                    var activeSiteSetting = await _SiteSettingRepository.FindOneAsync(x => x.IsActive == true);


                    var userRegistration = new RegistrationRequest
                    {
                        FirstName = item.FirstName,
                        LastName = item.LastName ?? "",
                        UserName = item.IdCardNo,
                        Password = activeSiteSetting.DefaultPassword ?? "Admin@123",
                        EmpId = EmpBasicInfoDto.Id,
                        Email = "",
                        PhoneNumber = "",
                        CanEditProfile = false
                    };
                    await _authenticationService.Register(userRegistration);
            
                    await _unitOfWork.Save();
                }

            }


            if(successCount > 0)
            {
                response.Success = true;
                response.Message = $"{successCount} Employee Created Successful and Failed {errorCount} Failed";
            }
            else
            {
                response.Success = false;
                response.Message = $"{successCount} Employee Created Successful and Failed {errorCount} Failed";
            }
            
            return response;
        }
    }
}