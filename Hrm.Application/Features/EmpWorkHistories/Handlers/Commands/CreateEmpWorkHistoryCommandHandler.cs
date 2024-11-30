using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpWorkHistories.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpWorkHistories.Handlers.Commands
{
    public class CreateEmpWorkHistoryCommandHandler : IRequestHandler<CreateEmpWorkHistoryCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<EmpWorkHistory> _EmpPersonalInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateEmpWorkHistoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<EmpWorkHistory> EmpPersonalInfoRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _EmpPersonalInfoRepository = EmpPersonalInfoRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateEmpWorkHistoryCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

                if (request.EmpWorkHistoryDto.Id == 0 )
                {
                    var EmpWorkHistory = _mapper.Map<EmpWorkHistory>(request.EmpWorkHistoryDto);

                    EmpWorkHistory = await _unitOfWork.Repository<EmpWorkHistory>().Add(EmpWorkHistory);
                }
                else
                {
                    var EmpWorkHistory = await _unitOfWork.Repository<EmpWorkHistory>().Get(request.EmpWorkHistoryDto.Id);

                    _mapper.Map(request.EmpWorkHistoryDto, EmpWorkHistory);

                    await _unitOfWork.Repository<EmpWorkHistory>().Update(EmpWorkHistory);
                }

            if (request.EmpWorkHistoryDto.Id != 0)
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