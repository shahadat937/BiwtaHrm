using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpNomineeInfos.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpNomineeInfos.Handlers.Commands
{
    public class CreateEmpNomineeInfoCommandHandler : IRequestHandler<CreateEmpNomineeInfoCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<EmpNomineeInfo> _EmpNomineeInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateEmpNomineeInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<EmpNomineeInfo> EmpNomineeInfoRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _EmpNomineeInfoRepository = EmpNomineeInfoRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateEmpNomineeInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            int empId = 0;
            int photoCount = 1;
            int signatureCount = 1;

            foreach (var item in request.EmpNomineeInfoDto)
            {
                empId = item.EmpId;
                if (item.Id == 0 )
                {

                    var empNomineeInfos = _mapper.Map<EmpNomineeInfo>(item);

                    if (item.PhotoFile!=null)
                    {
                        var photoName = Path.GetFileName(item.PhotoFile.FileName);
                        string uniqueImageName = item.PNo + "_NomineePhoto" + "_" + photoCount + "_" + photoName;
                        var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpNomineePhoto", uniqueImageName);

                        using (var photoSteam = new FileStream(photoPath, FileMode.Create))
                        {
                            await item.PhotoFile.CopyToAsync(photoSteam);
                        }

                        empNomineeInfos.PhotoUrl = uniqueImageName;
                        photoCount++;
                    }
                    if (item.SignatureFile!=null)
                    {
                        var signName = Path.GetFileName(item.SignatureFile.FileName);
                        string uniqueSignName = item.PNo + "_NomineeSignature" + "_" + signName;
                        var signPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpNomineeSignature", uniqueSignName);

                        using (var signStream = new FileStream(signPath, FileMode.Create))
                        {
                            await item.SignatureFile.CopyToAsync(signStream);
                        }

                        empNomineeInfos.SignatureUrl = uniqueSignName;
                        signatureCount++;
                    }

                    await _unitOfWork.Repository<EmpNomineeInfo>().Add(empNomineeInfos);

                }
                else
                {
                    var empNomineeInfos = await _unitOfWork.Repository<EmpNomineeInfo>().Get(item.Id);

                    _mapper.Map(item, empNomineeInfos);

                    if (item.PhotoFile != null)
                    {
                        if (!string.IsNullOrEmpty(empNomineeInfos.PhotoUrl))
                        {
                            var oldPhotoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpNomineePhoto", empNomineeInfos.PhotoUrl);
                            if (File.Exists(oldPhotoPath))
                            {
                                File.Delete(oldPhotoPath);
                            }
                        }

                        var photoName = Path.GetFileName(item.PhotoFile.FileName);
                        string uniqueImageName = item.PNo + "_NomineePhoto" + "_" + photoCount + "_" + photoName;
                        var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpNomineePhoto", uniqueImageName);

                        using (var photoSteam = new FileStream(photoPath, FileMode.Create))
                        {
                            await item.PhotoFile.CopyToAsync(photoSteam);
                        }

                        empNomineeInfos.PhotoUrl = uniqueImageName;
                        photoCount++;

                    }

                    if (item.SignatureFile != null)
                    {
                        if (!string.IsNullOrEmpty(empNomineeInfos.SignatureUrl))
                        {
                            var oldSignPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpNomineeSignature", empNomineeInfos.SignatureUrl);
                            if (File.Exists(oldSignPath))
                            {
                                File.Delete(oldSignPath);
                            }
                        }

                        var signName = Path.GetFileName(item.SignatureFile.FileName);
                        string uniqueSignName = item.PNo + "_NomineeSignature" + "_" + signName;
                        var signPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpNomineeSignature", uniqueSignName);

                        using (var signStream = new FileStream(signPath, FileMode.Create))
                        {
                            await item.SignatureFile.CopyToAsync(signStream);
                        }

                        empNomineeInfos.SignatureUrl = uniqueSignName;
                        signatureCount++;
                    }


                    await _unitOfWork.Repository<EmpNomineeInfo>().Update(empNomineeInfos);
                }
            }

            IQueryable<EmpNomineeInfo> EmpNomineeInfos = _EmpNomineeInfoRepository.Where(x => x.EmpId == empId);

            if (EmpNomineeInfos.Any())
            {
                response.Success = true;
                response.Message = "Update Successful";
            }
            else
            {
                response.Success = true;
                response.Message = "Create Successful";
            }
            await _unitOfWork.Save();


            return response;
        }
    }
}