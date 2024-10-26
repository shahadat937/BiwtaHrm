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
            NavbarSetting NavbarSetting = new NavbarSetting();


            var navbarSettings = _mapper.Map<NavbarSetting>(request.NavbarSettingDto);

            if (request.NavbarSettingDto.NavbarLogoFile != null)
            {
                var navbarLogoFile = Path.GetFileName(request.NavbarSettingDto.NavbarLogoFile.FileName);
                string uniqueImageName = Guid.NewGuid().ToString() + "_" + navbarLogoFile;
                var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\TempleteImage", uniqueImageName);

                using (var photoSteam = new FileStream(photoPath, FileMode.Create))
                {
                    await request.NavbarSettingDto.NavbarLogoFile.CopyToAsync(photoSteam);
                }

                navbarSettings.NavbarLogo = uniqueImageName;

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


            await _unitOfWork.Repository<NavbarSetting>().Add(navbarSettings);
            await _unitOfWork.Save();


            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = NavbarSetting.Id;

            return response;
        }
    }
}
