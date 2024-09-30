using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.SiteSettings.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SiteSettings.Handlers.Commands
{
    public class CreateSiteSettingCommandHandler : IRequestHandler<CreateSiteSettingCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateSiteSettingCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateSiteSettingCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            SiteSetting siteSetting = new SiteSetting();

            if (request.SiteSettingDto.SiteLogoFile != null)
            {
                var siteLogo = Path.GetFileName(request.SiteSettingDto.SiteLogoFile.FileName);
                string uniqueImageName = Guid.NewGuid().ToString() + "_" + siteLogo;
                var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\TempleteImage", uniqueImageName);

                using (var photoSteam = new FileStream(photoPath, FileMode.Create))
                {
                    await request.SiteSettingDto.SiteLogoFile.CopyToAsync(photoSteam);
                }

                siteSetting.SiteLogo = uniqueImageName;

            }

            var findActive = await _unitOfWork.Repository<SiteSetting>().FindOneAsync(x => x.IsActive == true);
            if (findActive != null && request.SiteSettingDto.IsActive == true)
            {
                findActive.IsActive = false;
                await _unitOfWork.Repository<Hrm.Domain.SiteSetting>().Update(findActive);
            }

            siteSetting.SiteName = request.SiteSettingDto.SiteName;
            siteSetting.SiteTitle = request.SiteSettingDto.SiteTitle;
            siteSetting.FooterTitle = request.SiteSettingDto.FooterTitle;
            siteSetting.Remark = request.SiteSettingDto.Remark ?? "";
            siteSetting.IsActive= request.SiteSettingDto.IsActive;


            await _unitOfWork.Repository<SiteSetting>().Add(siteSetting);
            await _unitOfWork.Save();


            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = siteSetting.Id;

            return response;
        }
    }
}
