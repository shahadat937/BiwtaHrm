using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.SiteSettings.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SiteSettings.Handlers.Commands
{
    public class UpdateSiteSettingCommandHandler : IRequestHandler<UpdateSiteSettingCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateSiteSettingCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateSiteSettingCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var siteSetting = await _unitOfWork.Repository<SiteSetting>().Get(request.SiteSettingDto.Id);

            if (request.SiteSettingDto.SiteLogoFile != null)
            {
                if (!string.IsNullOrEmpty(siteSetting.SiteLogo))
                {
                    var oldPhotoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\TempleteImage", siteSetting.SiteLogo);
                    if (File.Exists(oldPhotoPath))
                    {
                        File.Delete(oldPhotoPath);
                    }
                }

                var siteLogo = Path.GetFileName(request.SiteSettingDto.SiteLogoFile.FileName);
                string uniqueImageName = Guid.NewGuid().ToString() + "_" + siteLogo;
                var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\TempleteImage", uniqueImageName);

                using (var photoSteam = new FileStream(photoPath, FileMode.Create))
                {
                    await request.SiteSettingDto.SiteLogoFile.CopyToAsync(photoSteam);
                }

                siteSetting.SiteLogo = uniqueImageName;

            }

            var findActive = await _unitOfWork.Repository<SiteSetting>().Where(x => x.IsActive == true && x.Id != request.SiteSettingDto.Id).ToListAsync();
            if (findActive != null && request.SiteSettingDto.IsActive == true)
            {
                foreach (var item in findActive)
                {
                    item.IsActive = false;
                    await _unitOfWork.Repository<SiteSetting>().Update(item);
                }
            }

            siteSetting.SiteName = request.SiteSettingDto.SiteName;
            siteSetting.SiteTitle = request.SiteSettingDto.SiteTitle;
            siteSetting.FooterTitle = request.SiteSettingDto.FooterTitle;
            siteSetting.Remark = request.SiteSettingDto.Remark ?? "";
            siteSetting.DefaultPassword = request.SiteSettingDto.DefaultPassword ?? "";
            siteSetting.IsActive = request.SiteSettingDto.IsActive;


            await _unitOfWork.Repository<SiteSetting>().Update(siteSetting);
            await _unitOfWork.Save();


            response.Success = true;
            response.Message = "Update Successful";
            response.Id = siteSetting.Id;

            return response;
        }
    }
}