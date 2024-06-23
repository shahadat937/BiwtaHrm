using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpForeignTourInfos.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpForeignTourInfos.Handlers.Commands
{
    public class CreateEmpForeignTourInfoCommandHandler : IRequestHandler<CreateEmpForeignTourInfoCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<EmpForeignTourInfo> _EmpPersonalInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateEmpForeignTourInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<EmpForeignTourInfo> EmpPersonalInfoRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _EmpPersonalInfoRepository = EmpPersonalInfoRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateEmpForeignTourInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            int empId = 0;

            foreach (var item in request.EmpForeignTourInfoDto)
            {
                empId = item.EmpId;
                if (item.Id == 0 )
                {
                    var EmpForeignTourInfo = _mapper.Map<EmpForeignTourInfo>(item);

                    EmpForeignTourInfo = await _unitOfWork.Repository<EmpForeignTourInfo>().Add(EmpForeignTourInfo);
                }
                else
                {
                    var EmpForeignTourInfo = await _unitOfWork.Repository<EmpForeignTourInfo>().Get(item.Id);

                    _mapper.Map(item, EmpForeignTourInfo);

                    await _unitOfWork.Repository<EmpForeignTourInfo>().Update(EmpForeignTourInfo);
                }
            }

            IQueryable<EmpForeignTourInfo> empForeignTourInfos = _EmpPersonalInfoRepository.Where(x => x.EmpId == empId);

            if (empForeignTourInfos.Any())
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