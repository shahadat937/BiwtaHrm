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
        }
        public async Task<BaseCommandResponse> Handle(CreateEmpSpouseInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            foreach (var item in request.EmpSpouseInfoDto)
            {
                if (item.Id == 0 )
                {
                    var EmpSpouseInfo = _mapper.Map<EmpSpouseInfo>(item);

                    EmpSpouseInfo = await _unitOfWork.Repository<EmpSpouseInfo>().Add(EmpSpouseInfo);
                    await _unitOfWork.Save();
                }
                else
                {
                    var EmpSpouseInfo = await _unitOfWork.Repository<EmpSpouseInfo>().Get(item.Id);

                    _mapper.Map(item, EmpSpouseInfo);

                    await _unitOfWork.Repository<EmpSpouseInfo>().Update(EmpSpouseInfo);
                    await _unitOfWork.Save();
                }
            }

            response.Success = true;
            response.Message = "Creation Successful";

            return response;
        }
    }
}