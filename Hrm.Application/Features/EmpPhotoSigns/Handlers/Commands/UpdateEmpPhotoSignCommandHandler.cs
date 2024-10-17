using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpPersonalInfos.Requests.Commands;
using Hrm.Application.Features.EmpPhotoSigns.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpPhotoSigns.Handlers.Commands
{
    public class UpdateEmpPhotoSignCommandHandler : IRequestHandler<UpdateEmpPhotoSignCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<EmpPhotoSign> _EmpPersonalInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEmpPhotoSignCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<EmpPhotoSign> EmpPersonalInfoRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _EmpPersonalInfoRepository = EmpPersonalInfoRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateEmpPhotoSignCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var EmpPhotoSign = await _unitOfWork.Repository<EmpPhotoSign>().Get(request.EmpPhotoSignDto.Id);

            if (request.EmpPhotoSignDto.PhotoFile != null)
            {
                if (!string.IsNullOrEmpty(EmpPhotoSign.PhotoUrl))
                {
                    var oldPhotoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpPhoto", EmpPhotoSign.PhotoUrl);
                    if (File.Exists(oldPhotoPath))
                    {
                        File.Delete(oldPhotoPath);
                    }
                }

                var photoName = Path.GetFileName(request.EmpPhotoSignDto.PhotoFile.FileName);
                string uniqueImageName = request.EmpPhotoSignDto.PNo + "_Photo" + "_" + photoName;
                var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpPhoto", uniqueImageName);

                using (var photoSteam = new FileStream(photoPath, FileMode.Create))
                {
                    await request.EmpPhotoSignDto.PhotoFile.CopyToAsync(photoSteam);
                }

                EmpPhotoSign.PhotoUrl = uniqueImageName;

            }

            if (request.EmpPhotoSignDto.SignatureFile != null)
            {
                if (!string.IsNullOrEmpty(EmpPhotoSign.SignatureUrl))
                {
                    var oldSignPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\Images\\EmpSignature", EmpPhotoSign.SignatureUrl);
                    if (File.Exists(oldSignPath))
                    {
                        File.Delete(oldSignPath);
                    }
                }

                var signName = Path.GetFileName(request.EmpPhotoSignDto.SignatureFile.FileName);
                string uniqueSignName = request.EmpPhotoSignDto.PNo + "_Signature" + "_" + signName;
                var signPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\Images\\EmpSignature", uniqueSignName);

                using (var signStream = new FileStream(signPath, FileMode.Create))
                {
                    await request.EmpPhotoSignDto.SignatureFile.CopyToAsync(signStream);
                }

                EmpPhotoSign.SignatureUrl = uniqueSignName;
            }

            EmpPhotoSign.UniqueIdentity = request.EmpPhotoSignDto.UniqueIdentity ?? "";
            EmpPhotoSign.Remark = request.EmpPhotoSignDto.Remark ?? "";
            EmpPhotoSign.MenuPosition = request.EmpPhotoSignDto.MenuPosition;
            EmpPhotoSign.IsActive = request.EmpPhotoSignDto.IsActive;

            await _unitOfWork.Repository<EmpPhotoSign>().Update(EmpPhotoSign);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Update Successfull";

            return response;
        }

    }
}