using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.NavbarSettings.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.NavbarSettings.Handlers.Commands
{
    public class CreateNavbarSettingCommandHandler : IRequestHandler<CreateNavbarSettingCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateNavbarSettingCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateNavbarSettingCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            NavbarSetting navbarSettings = new NavbarSetting();


            //var navbarSettings = _mapper.Map<NavbarSetting>(request.NavbarSettingDto);

            if (request.NavbarSettingDto.NavbarLogoFile != null)
            {
                var navbarLogoFile = Path.GetFileName(request.NavbarSettingDto.NavbarLogoFile.FileName);
                string uniqueImageName = Guid.NewGuid().ToString() + "_NavbarLogo_" + navbarLogoFile;
                var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\TempleteImage", uniqueImageName);

                using (var photoSteam = new FileStream(photoPath, FileMode.Create))
                {
                    await request.NavbarSettingDto.NavbarLogoFile.CopyToAsync(photoSteam);
                }

                navbarSettings.NavbarLogo = uniqueImageName;

            }

            if (request.NavbarSettingDto.BrandLogoFile != null)
            {
                var brandLogoFile = Path.GetFileName(request.NavbarSettingDto.BrandLogoFile.FileName);
                string uniqueImageName = Guid.NewGuid().ToString() + "_BrandLogo_" + brandLogoFile;
                var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\TempleteImage", uniqueImageName);

                using (var photoSteam = new FileStream(photoPath, FileMode.Create))
                {
                    await request.NavbarSettingDto.BrandLogoFile.CopyToAsync(photoSteam);
                }

                navbarSettings.BrandLogo = uniqueImageName;

            }

            var findActive = await _unitOfWork.Repository<NavbarSetting>().Where(x => x.IsActive == true).ToListAsync();
            if (findActive != null && request.NavbarSettingDto.IsActive == true)
            {
                foreach (var item in findActive)
                {
                    item.IsActive = false;
                    await _unitOfWork.Repository<NavbarSetting>().Update(item);
                }
            }

            navbarSettings.BrandName = request.NavbarSettingDto.BrandName;
            navbarSettings.ShowLogo = request.NavbarSettingDto.ShowLogo;
            navbarSettings.ThemId = request.NavbarSettingDto.ThemId;
            navbarSettings.Remark = request.NavbarSettingDto.Remark;
            navbarSettings.IsActive = request.NavbarSettingDto.IsActive;


            await _unitOfWork.Repository<NavbarSetting>().Add(navbarSettings);
            await _unitOfWork.Save();


            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = navbarSettings.Id;

            return response;
        }
    }
}
