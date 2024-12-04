using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpPersonalInfos.Requests.Commands;
using Hrm.Application.Features.EmpFingerPrints.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpFingerPrints.Handlers.Commands
{
    public class UpdateEmpFingerPrintCommandHandler : IRequestHandler<UpdateEmpFingerPrintCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<EmpFingerPrint> _EmpPersonalInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEmpFingerPrintCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<EmpFingerPrint> EmpPersonalInfoRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _EmpPersonalInfoRepository = EmpPersonalInfoRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateEmpFingerPrintCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var EmpFingerPrint = await _unitOfWork.Repository<EmpFingerPrint>().Get(request.EmpFingerPrintDto.Id);


            //Right Fingerprint Files

            if (request.EmpFingerPrintDto.RightThumbFile != null)
            {
                if (!string.IsNullOrEmpty(EmpFingerPrint.RightThumb))
                {
                    var oldPhotoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpFingerprint", EmpFingerPrint.RightThumb);
                    if (File.Exists(oldPhotoPath))
                    {
                        File.Delete(oldPhotoPath);
                    }
                }

                var photoName = Path.GetFileName(request.EmpFingerPrintDto.RightThumbFile.FileName);
                string uniqueImageName = request.EmpFingerPrintDto.PNo + "_RightThumb_" + photoName;
                var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpFingerprint", uniqueImageName);

                using (var photoSteam = new FileStream(photoPath, FileMode.Create))
                {
                    await request.EmpFingerPrintDto.RightThumbFile.CopyToAsync(photoSteam);
                }

                EmpFingerPrint.RightThumb = uniqueImageName;
            }

            if (request.EmpFingerPrintDto.RightIndexFile != null)
            {
                if (!string.IsNullOrEmpty(EmpFingerPrint.RightIndex))
                {
                    var oldPhotoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpFingerprint", EmpFingerPrint.RightIndex);
                    if (File.Exists(oldPhotoPath))
                    {
                        File.Delete(oldPhotoPath);
                    }
                }

                var photoName = Path.GetFileName(request.EmpFingerPrintDto.RightIndexFile.FileName);
                string uniqueImageName = request.EmpFingerPrintDto.PNo + "_RightIndex_" + photoName;
                var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpFingerprint", uniqueImageName);

                using (var photoSteam = new FileStream(photoPath, FileMode.Create))
                {
                    await request.EmpFingerPrintDto.RightIndexFile.CopyToAsync(photoSteam);
                }

                EmpFingerPrint.RightIndex = uniqueImageName;
            }

            if (request.EmpFingerPrintDto.RightMiddleFile != null)
            {
                if (!string.IsNullOrEmpty(EmpFingerPrint.RightMiddle))
                {
                    var oldPhotoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpFingerprint", EmpFingerPrint.RightMiddle);
                    if (File.Exists(oldPhotoPath))
                    {
                        File.Delete(oldPhotoPath);
                    }
                }

                var photoName = Path.GetFileName(request.EmpFingerPrintDto.RightMiddleFile.FileName);
                string uniqueImageName = request.EmpFingerPrintDto.PNo + "_RightMiddle_" + photoName;
                var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpFingerprint", uniqueImageName);

                using (var photoSteam = new FileStream(photoPath, FileMode.Create))
                {
                    await request.EmpFingerPrintDto.RightMiddleFile.CopyToAsync(photoSteam);
                }

                EmpFingerPrint.RightMiddle = uniqueImageName;
            }

            if (request.EmpFingerPrintDto.RightRingFile != null)
            {
                if (!string.IsNullOrEmpty(EmpFingerPrint.RightRing))
                {
                    var oldPhotoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpFingerprint", EmpFingerPrint.RightRing);
                    if (File.Exists(oldPhotoPath))
                    {
                        File.Delete(oldPhotoPath);
                    }
                }

                var photoName = Path.GetFileName(request.EmpFingerPrintDto.RightRingFile.FileName);
                string uniqueImageName = request.EmpFingerPrintDto.PNo + "_RightRing_" + photoName;
                var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpFingerprint", uniqueImageName);

                using (var photoSteam = new FileStream(photoPath, FileMode.Create))
                {
                    await request.EmpFingerPrintDto.RightRingFile.CopyToAsync(photoSteam);
                }

                EmpFingerPrint.RightRing = uniqueImageName;
            }

            if (request.EmpFingerPrintDto.RightLittleFile != null)
            {
                if (!string.IsNullOrEmpty(EmpFingerPrint.RightLittle))
                {
                    var oldPhotoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpFingerprint", EmpFingerPrint.RightLittle);
                    if (File.Exists(oldPhotoPath))
                    {
                        File.Delete(oldPhotoPath);
                    }
                }

                var photoName = Path.GetFileName(request.EmpFingerPrintDto.RightLittleFile.FileName);
                string uniqueImageName = request.EmpFingerPrintDto.PNo + "_RightLittle_" + photoName;
                var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpFingerprint", uniqueImageName);

                using (var photoSteam = new FileStream(photoPath, FileMode.Create))
                {
                    await request.EmpFingerPrintDto.RightLittleFile.CopyToAsync(photoSteam);
                }

                EmpFingerPrint.RightLittle = uniqueImageName;
            }



            //Left Fingerprint Files

            if (request.EmpFingerPrintDto.LeftThumbFile != null)
            {
                if (!string.IsNullOrEmpty(EmpFingerPrint.LeftThumb))
                {
                    var oldPhotoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpFingerprint", EmpFingerPrint.LeftThumb);
                    if (File.Exists(oldPhotoPath))
                    {
                        File.Delete(oldPhotoPath);
                    }
                }

                var photoName = Path.GetFileName(request.EmpFingerPrintDto.LeftThumbFile.FileName);
                string uniqueImageName = request.EmpFingerPrintDto.PNo + "_LeftThumb_" + photoName;
                var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpFingerprint", uniqueImageName);

                using (var photoSteam = new FileStream(photoPath, FileMode.Create))
                {
                    await request.EmpFingerPrintDto.LeftThumbFile.CopyToAsync(photoSteam);
                }

                EmpFingerPrint.LeftThumb = uniqueImageName;
            }

            if (request.EmpFingerPrintDto.LeftIndexFile != null)
            {
                if (!string.IsNullOrEmpty(EmpFingerPrint.LeftIndex))
                {
                    var oldPhotoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpFingerprint", EmpFingerPrint.LeftIndex);
                    if (File.Exists(oldPhotoPath))
                    {
                        File.Delete(oldPhotoPath);
                    }
                }

                var photoName = Path.GetFileName(request.EmpFingerPrintDto.LeftIndexFile.FileName);
                string uniqueImageName = request.EmpFingerPrintDto.PNo + "_LeftIndex_" + photoName;
                var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpFingerprint", uniqueImageName);

                using (var photoSteam = new FileStream(photoPath, FileMode.Create))
                {
                    await request.EmpFingerPrintDto.LeftIndexFile.CopyToAsync(photoSteam);
                }

                EmpFingerPrint.LeftIndex = uniqueImageName;
            }

            if (request.EmpFingerPrintDto.LeftMiddleFile != null)
            {
                if (!string.IsNullOrEmpty(EmpFingerPrint.LeftMiddle))
                {
                    var oldPhotoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpFingerprint", EmpFingerPrint.LeftMiddle);
                    if (File.Exists(oldPhotoPath))
                    {
                        File.Delete(oldPhotoPath);
                    }
                }

                var photoName = Path.GetFileName(request.EmpFingerPrintDto.LeftMiddleFile.FileName);
                string uniqueImageName = request.EmpFingerPrintDto.PNo + "_LeftMiddle_" + photoName;
                var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpFingerprint", uniqueImageName);

                using (var photoSteam = new FileStream(photoPath, FileMode.Create))
                {
                    await request.EmpFingerPrintDto.LeftMiddleFile.CopyToAsync(photoSteam);
                }

                EmpFingerPrint.LeftMiddle = uniqueImageName;
            }

            if (request.EmpFingerPrintDto.LeftRingFile != null)
            {
                if (!string.IsNullOrEmpty(EmpFingerPrint.LeftRing))
                {
                    var oldPhotoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpFingerprint", EmpFingerPrint.LeftRing);
                    if (File.Exists(oldPhotoPath))
                    {
                        File.Delete(oldPhotoPath);
                    }
                }

                var photoName = Path.GetFileName(request.EmpFingerPrintDto.LeftRingFile.FileName);
                string uniqueImageName = request.EmpFingerPrintDto.PNo + "_LeftRing_" + photoName;
                var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpFingerprint", uniqueImageName);

                using (var photoSteam = new FileStream(photoPath, FileMode.Create))
                {
                    await request.EmpFingerPrintDto.LeftRingFile.CopyToAsync(photoSteam);
                }

                EmpFingerPrint.LeftRing = uniqueImageName;
            }

            if (request.EmpFingerPrintDto.LeftLittleFile != null)
            {
                if (!string.IsNullOrEmpty(EmpFingerPrint.LeftLittle))
                {
                    var oldPhotoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpFingerprint", EmpFingerPrint.LeftLittle);
                    if (File.Exists(oldPhotoPath))
                    {
                        File.Delete(oldPhotoPath);
                    }
                }

                var photoName = Path.GetFileName(request.EmpFingerPrintDto.LeftLittleFile.FileName);
                string uniqueImageName = request.EmpFingerPrintDto.PNo + "_LeftLittle_" + photoName;
                var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpFingerprint", uniqueImageName);

                using (var photoSteam = new FileStream(photoPath, FileMode.Create))
                {
                    await request.EmpFingerPrintDto.LeftLittleFile.CopyToAsync(photoSteam);
                }

                EmpFingerPrint.LeftLittle = uniqueImageName;
            }


            await _unitOfWork.Repository<EmpFingerPrint>().Update(EmpFingerPrint);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Update Successfull";

            return response;
        }

    }
}