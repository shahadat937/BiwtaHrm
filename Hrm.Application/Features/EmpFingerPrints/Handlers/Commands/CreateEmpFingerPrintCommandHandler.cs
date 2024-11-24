using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpFingerPrints.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpFingerPrints.Handlers.Commands
{
    public class CreateEmpFingerPrintCommandHandler : IRequestHandler<CreateEmpFingerPrintCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateEmpFingerPrintCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateEmpFingerPrintCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            EmpFingerPrint EmpFingerPrints = new EmpFingerPrint();

            //Right Fingerprint Files

            if (request.EmpFingerPrintDto.RightThumbFile != null)
            {
                var photoName = Path.GetFileName(request.EmpFingerPrintDto.RightThumbFile.FileName);
                string uniqueImageName = request.EmpFingerPrintDto.PNo + "_RightThumb_" + photoName;
                var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpFingerprint", uniqueImageName);

                using (var photoSteam = new FileStream(photoPath, FileMode.Create))
                {
                    await request.EmpFingerPrintDto.RightThumbFile.CopyToAsync(photoSteam);
                }

                EmpFingerPrints.RightThumb = uniqueImageName;
            }

            if (request.EmpFingerPrintDto.RightIndexFile != null)
            {
                var photoName = Path.GetFileName(request.EmpFingerPrintDto.RightIndexFile.FileName);
                string uniqueImageName = request.EmpFingerPrintDto.PNo + "_RightIndex_" + photoName;
                var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpFingerprint", uniqueImageName);

                using (var photoSteam = new FileStream(photoPath, FileMode.Create))
                {
                    await request.EmpFingerPrintDto.RightIndexFile.CopyToAsync(photoSteam);
                }

                EmpFingerPrints.RightIndex = uniqueImageName;
            }

            if (request.EmpFingerPrintDto.RightMiddleFile != null)
            {
                var photoName = Path.GetFileName(request.EmpFingerPrintDto.RightMiddleFile.FileName);
                string uniqueImageName = request.EmpFingerPrintDto.PNo + "_RightMiddle_" + photoName;
                var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpFingerprint", uniqueImageName);

                using (var photoSteam = new FileStream(photoPath, FileMode.Create))
                {
                    await request.EmpFingerPrintDto.RightMiddleFile.CopyToAsync(photoSteam);
                }

                EmpFingerPrints.RightMiddle = uniqueImageName;
            }

            if (request.EmpFingerPrintDto.RightRingFile != null)
            {
                var photoName = Path.GetFileName(request.EmpFingerPrintDto.RightRingFile.FileName);
                string uniqueImageName = request.EmpFingerPrintDto.PNo + "_RightRing_" + photoName;
                var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpFingerprint", uniqueImageName);

                using (var photoSteam = new FileStream(photoPath, FileMode.Create))
                {
                    await request.EmpFingerPrintDto.RightRingFile.CopyToAsync(photoSteam);
                }

                EmpFingerPrints.RightRing = uniqueImageName;
            }

            if (request.EmpFingerPrintDto.RightLittleFile != null)
            {
                var photoName = Path.GetFileName(request.EmpFingerPrintDto.RightLittleFile.FileName);
                string uniqueImageName = request.EmpFingerPrintDto.PNo + "_RightLittle_" + photoName;
                var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpFingerprint", uniqueImageName);

                using (var photoSteam = new FileStream(photoPath, FileMode.Create))
                {
                    await request.EmpFingerPrintDto.RightLittleFile.CopyToAsync(photoSteam);
                }

                EmpFingerPrints.RightLittle = uniqueImageName;
            }


            //Left Fingerprint Files

            if (request.EmpFingerPrintDto.LeftThumbFile != null)
            {
                var photoName = Path.GetFileName(request.EmpFingerPrintDto.LeftThumbFile.FileName);
                string uniqueImageName = request.EmpFingerPrintDto.PNo + "_LeftThumb_" + photoName;
                var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpFingerprint", uniqueImageName);

                using (var photoSteam = new FileStream(photoPath, FileMode.Create))
                {
                    await request.EmpFingerPrintDto.LeftThumbFile.CopyToAsync(photoSteam);
                }

                EmpFingerPrints.LeftThumb = uniqueImageName;
            }

            if (request.EmpFingerPrintDto.LeftIndexFile != null)
            {
                var photoName = Path.GetFileName(request.EmpFingerPrintDto.LeftIndexFile.FileName);
                string uniqueImageName = request.EmpFingerPrintDto.PNo + "_LeftIndex_" + photoName;
                var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpFingerprint", uniqueImageName);

                using (var photoSteam = new FileStream(photoPath, FileMode.Create))
                {
                    await request.EmpFingerPrintDto.LeftIndexFile.CopyToAsync(photoSteam);
                }

                EmpFingerPrints.LeftIndex = uniqueImageName;
            }

            if (request.EmpFingerPrintDto.LeftMiddleFile != null)
            {
                var photoName = Path.GetFileName(request.EmpFingerPrintDto.LeftMiddleFile.FileName);
                string uniqueImageName = request.EmpFingerPrintDto.PNo + "_LeftMiddle_" + photoName;
                var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpFingerprint", uniqueImageName);

                using (var photoSteam = new FileStream(photoPath, FileMode.Create))
                {
                    await request.EmpFingerPrintDto.LeftMiddleFile.CopyToAsync(photoSteam);
                }

                EmpFingerPrints.LeftMiddle = uniqueImageName;
            }

            if (request.EmpFingerPrintDto.LeftRingFile != null)
            {
                var photoName = Path.GetFileName(request.EmpFingerPrintDto.LeftRingFile.FileName);
                string uniqueImageName = request.EmpFingerPrintDto.PNo + "_LeftRing_" + photoName;
                var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpFingerprint", uniqueImageName);

                using (var photoSteam = new FileStream(photoPath, FileMode.Create))
                {
                    await request.EmpFingerPrintDto.LeftRingFile.CopyToAsync(photoSteam);
                }

                EmpFingerPrints.LeftRing = uniqueImageName;
            }

            if (request.EmpFingerPrintDto.LeftLittleFile != null)
            {
                var photoName = Path.GetFileName(request.EmpFingerPrintDto.LeftLittleFile.FileName);
                string uniqueImageName = request.EmpFingerPrintDto.PNo + "_LeftLittle_" + photoName;
                var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpFingerprint", uniqueImageName);

                using (var photoSteam = new FileStream(photoPath, FileMode.Create))
                {
                    await request.EmpFingerPrintDto.LeftLittleFile.CopyToAsync(photoSteam);
                }

                EmpFingerPrints.LeftLittle = uniqueImageName;
            }

            EmpFingerPrints.EmpId = request.EmpFingerPrintDto.EmpId;
            EmpFingerPrints.IsActive = request.EmpFingerPrintDto.IsActive;


            await _unitOfWork.Repository<EmpFingerPrint>().Add(EmpFingerPrints);
            await _unitOfWork.Save();


            response.Success = true;
            response.Message = "Creation Successful";

            return response;
        }
    }
}