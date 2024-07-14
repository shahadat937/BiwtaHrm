using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpPhotoSigns.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpPhotoSigns.Handlers.Commands
{
    public class CreateEmpPhotoSignCommandHandler : IRequestHandler<CreateEmpPhotoSignCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateEmpPhotoSignCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateEmpPhotoSignCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            EmpPhotoSign empPhotoSigns = new EmpPhotoSign();

            if (request.EmpPhotoSignDto.PhotoFile != null)
            {
                var photoName = Path.GetFileName(request.EmpPhotoSignDto.PhotoFile.FileName);
                string uniqueImageName = request.EmpPhotoSignDto.PNo + "_Photo" + "_" + photoName;
                var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpPhoto", uniqueImageName);

                using (var photoSteam = new FileStream(photoPath, FileMode.Create))
                {
                    await request.EmpPhotoSignDto.PhotoFile.CopyToAsync(photoSteam);
                }

                empPhotoSigns.PhotoUrl = uniqueImageName;

            }
            if (request.EmpPhotoSignDto.SignatureFile != null)
            {
                var signName = Path.GetFileName(request.EmpPhotoSignDto.SignatureFile.FileName);
                string uniqueSignName = request.EmpPhotoSignDto.PNo + "_Signature" + "_" + signName;
                var signPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpSignature", uniqueSignName);

                using (var signStream = new FileStream(signPath, FileMode.Create))
                {
                    await request.EmpPhotoSignDto.SignatureFile.CopyToAsync(signStream);
                }

                empPhotoSigns.SignatureUrl = uniqueSignName;
            }

            empPhotoSigns.EmpId = request.EmpPhotoSignDto.EmpId;
            empPhotoSigns.UniqueIdentity = request.EmpPhotoSignDto.UniqueIdentity;
            empPhotoSigns.Remark = request.EmpPhotoSignDto.Remark;


            await _unitOfWork.Repository<EmpPhotoSign>().Add(empPhotoSigns);
            await _unitOfWork.Save();


            response.Success = true;
            response.Message = "Creation Successful";

            return response;
        }
    }
}