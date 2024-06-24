using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpSpouseInfos.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpSpouseInfos.Handlers.Commands
{
    public class CreateEmpSpouseInfoCommandHandler : IRequestHandler<CreateEmpSpouseInfoCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateEmpSpouseInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        private readonly IHrmRepository<EmpSpouseInfo> _EmpPersonalInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateEmpSpouseInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<EmpSpouseInfo> EmpPersonalInfoRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _EmpPersonalInfoRepository = EmpPersonalInfoRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateEmpSpouseInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            foreach (var item in request.EmpSpouseInfoDto)
            {
                if (item.Id == 0 )
                {
                    var EmpSpouseInfo = _mapper.Map<EmpSpouseInfo>(request.EmpSpouseInfoDto);

                    EmpSpouseInfo = await _unitOfWork.Repository<EmpSpouseInfo>().Add(EmpSpouseInfo);
                    await _unitOfWork.Save();
            int empId = 0;

            foreach (var item in request.EmpSpouseInfoDto)
            {
                empId = item.EmpId;
                if (item.Id == 0 )
                {
                    var EmpSpouseInfo = _mapper.Map<EmpSpouseInfo>(item);

                    EmpSpouseInfo = await _unitOfWork.Repository<EmpSpouseInfo>().Add(EmpSpouseInfo);

                }
                else
                {
                    var EmpSpouseInfo = await _unitOfWork.Repository<EmpSpouseInfo>().Get(item.Id);


                    _mapper.Map(request.EmpSpouseInfoDto, EmpSpouseInfo);

                    await _unitOfWork.Repository<EmpSpouseInfo>().Update(EmpSpouseInfo);
                    await _unitOfWork.Save();
                }
            }

            response.Success = true;
            response.Message = "Creation Successful";

                    _mapper.Map(item, EmpSpouseInfo);

                    await _unitOfWork.Repository<EmpSpouseInfo>().Update(EmpSpouseInfo);
                }
            }

            IQueryable<EmpSpouseInfo> empSpouseInfos = _EmpPersonalInfoRepository.Where(x => x.EmpId == empId);

            if (empSpouseInfos.Any())
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