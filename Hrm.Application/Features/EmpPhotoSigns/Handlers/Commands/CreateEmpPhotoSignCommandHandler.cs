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

            string uniqueImageName = request.EmpPhotoSignDto.PNo + "_Photo";
            var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\Images\\EmpPhoto", uniqueImageName);

            string uniqueSignName = request.EmpPhotoSignDto.PNo + "_Signature";
            var signPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\Images\\EmpSignature", uniqueSignName);

            using (var photoSteam = new FileStream(photoPath, FileMode.Create))
            {
                await request.EmpPhotoSignDto.PhotoFile.CopyToAsync(photoSteam);
            }
            using (var signStream = new FileStream(signPath, FileMode.Create))
            {
                await request.EmpPhotoSignDto.SignatureFile.CopyToAsync(signStream);
            }

            EmpPhotoSign empPhotoSigns = new EmpPhotoSign
            {
                EmpId = request.EmpPhotoSignDto.EmpId,
                UniqueIdentity = request.EmpPhotoSignDto.UniqueIdentity,
                Remark = request.EmpPhotoSignDto.Remark,
                PhotoUrl = uniqueImageName,
                SignatureUrl = uniqueSignName
            };


            await _unitOfWork.Repository<EmpPhotoSign>().Add(empPhotoSigns);
            await _unitOfWork.Save();


            response.Success = true;
            response.Message = "Creation Successful";

            return response;
        }
    }
}