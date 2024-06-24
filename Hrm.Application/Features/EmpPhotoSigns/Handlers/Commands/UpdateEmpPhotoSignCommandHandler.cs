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

            string uniqueImageName = request.EmpPhotoSignDto.PNo + "_Photo";
            var a = Directory.GetCurrentDirectory();
            var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\Images\\EmpPhoto", uniqueImageName);



            _mapper.Map(request.EmpPhotoSignDto, EmpPhotoSign);

            await _unitOfWork.Repository<EmpPhotoSign>().Update(EmpPhotoSign);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Update Successfull";

            return response;
        }

    }
}