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
            int empId = 0;

            foreach (var item in request.EmpWorkHistoryDto)
            {
                empId = item.EmpId ?? 0;
                if (item.Id == 0)
                {
                    var EmpWorkHistory = _mapper.Map<EmpWorkHistory>(item);

                    EmpWorkHistory = await _unitOfWork.Repository<EmpWorkHistory>().Add(EmpWorkHistory);
                }
                else
                {
                    var EmpWorkHistory = await _unitOfWork.Repository<EmpWorkHistory>().Get(item.Id);

                    _mapper.Map(item, EmpWorkHistory);

                    await _unitOfWork.Repository<EmpWorkHistory>().Update(EmpWorkHistory);
                }
            }
            IQueryable<EmpWorkHistory> EmpWorkHistories = _EmpPersonalInfoRepository.Where(x => x.EmpId == empId);

            if (EmpWorkHistories.Any())
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