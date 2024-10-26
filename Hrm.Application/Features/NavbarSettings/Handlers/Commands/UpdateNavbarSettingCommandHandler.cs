using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.NavbarSettings.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.NavbarSettings.Handlers.Commands
{
    public class UpdateNavbarSettingCommandHandler : IRequestHandler<UpdateNavbarSettingCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateNavbarSettingCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateNavbarSettingCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var NavbarSetting = await _unitOfWork.Repository<NavbarSetting>().Get(request.NavbarSettingDto.Id);


            var navbarSettings = _mapper.Map<NavbarSetting>(request.NavbarSettingDto);

            if (request.NavbarSettingDto.NavbarLogoFile != null)
            {
                if (!string.IsNullOrEmpty(NavbarSetting.NavbarLogo))
                {
                    var oldPhotoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\TempleteImage", NavbarSetting.NavbarLogo);
                    if (File.Exists(oldPhotoPath))
                    {
                        File.Delete(oldPhotoPath);
                    }
                }

                var navbarLogo = Path.GetFileName(request.NavbarSettingDto.NavbarLogoFile.FileName);
                string uniqueImageName = Guid.NewGuid().ToString() + "_" + navbarLogo;
                var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\TempleteImage", uniqueImageName);

                using (var photoSteam = new FileStream(photoPath, FileMode.Create))
                {
                    await request.NavbarSettingDto.NavbarLogoFile.CopyToAsync(photoSteam);
                }

                navbarSettings.NavbarLogo = uniqueImageName;

            }

            var findActive = await _unitOfWork.Repository<NavbarSetting>().Where(x => x.IsActive == true && x.Id != request.NavbarSettingDto.Id).ToListAsync();
            if (findActive != null && request.NavbarSettingDto.IsActive == true)
            {
                foreach (var item in findActive)
                {
                    item.IsActive = false;
                    await _unitOfWork.Repository<NavbarSetting>().Update(item);
                }
            }



            await _unitOfWork.Repository<NavbarSetting>().Update(navbarSettings);
            await _unitOfWork.Save();


            response.Success = true;
            response.Message = "Update Successful";
            response.Id = navbarSettings.Id;

            return response;
        }
    }
}