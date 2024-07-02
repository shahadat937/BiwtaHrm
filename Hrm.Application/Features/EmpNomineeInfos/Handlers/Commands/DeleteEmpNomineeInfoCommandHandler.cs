using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Department.Requests.Commands;
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
    public class DeleteEmpNomineeInfoCommandHandler : IRequestHandler<DeleteEmpNomineeInfoCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteEmpNomineeInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteEmpNomineeInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var empNomineeInfos = await _unitOfWork.Repository<Hrm.Domain.EmpNomineeInfo>().Get(request.Id);

            if (empNomineeInfos == null)
            {
                throw new NotFoundException(nameof(EmpNomineeInfo), request.Id);
            }

            if (!string.IsNullOrEmpty(empNomineeInfos.PhotoUrl))
            {
                var oldPhotoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpNomineePhoto", empNomineeInfos.PhotoUrl);
                if (File.Exists(oldPhotoPath))
                {
                    File.Delete(oldPhotoPath);
                }
            }

            if (!string.IsNullOrEmpty(empNomineeInfos.SignatureUrl))
            {
                var oldSignPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpNomineeSignature", empNomineeInfos.SignatureUrl);
                if (File.Exists(oldSignPath))
                {
                    File.Delete(oldSignPath);
                }
            }


            await _unitOfWork.Repository<Hrm.Domain.EmpNomineeInfo>().Delete(empNomineeInfos);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = empNomineeInfos.Id;

            return response;
        }
    }
}
