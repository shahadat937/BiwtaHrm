using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.FormRecord;
using Hrm.Application.Features.FormRecord.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FormRecord.Handlers.Queries
{
    public class GetFormRecordRequestHandler: IRequestHandler<GetFormRecordRequest,List<FormRecordDto>>
    {
        private readonly IHrmRepository<Hrm.Domain.FormRecord> _repository;
        private readonly IHrmRepository<Hrm.Domain.EmpJobDetail> _jobDetailRepository;
        private readonly IMapper _mapper;

        public GetFormRecordRequestHandler(IHrmRepository<Domain.FormRecord> repository, 
            IHrmRepository<Hrm.Domain.EmpJobDetail> jobDetailRepository,
            IMapper mapper)
        {
            _repository = repository;
            _jobDetailRepository = jobDetailRepository;
            _mapper = mapper;
        }

        public async Task<List<FormRecordDto>> Handle(GetFormRecordRequest request, CancellationToken cancellationToken)
        {
            var formRecords = _repository.Where(x => true)
                .Include(x => x.Form)
                .Include(x => x.Employee)
                    .ThenInclude(x => x.EmpJobDetail)
                .AsQueryable();

            if (request.Filters.RecordId.HasValue)
            {
                formRecords = formRecords.Where(x => x.RecordId == request.Filters.RecordId);
            }

            if (request.Filters.FormId.HasValue)
            {
                formRecords = formRecords.Where(x => x.FormId == request.Filters.FormId);
            }

            if (request.Filters.DepartmentId.HasValue)
            {
                formRecords = formRecords.Where(x => x.Employee.EmpJobDetail != null && x.Employee.EmpJobDetail.FirstOrDefault().DepartmentId == request.Filters.DepartmentId);
            }

            if (request.Filters.SectionId.HasValue)
            {
                formRecords = formRecords.Where(x => x.Employee.EmpJobDetail != null && x.Employee.EmpJobDetail.FirstOrDefault().SectionId == request.Filters.SectionId);
            }

            if (request.Filters.ReporterId.HasValue)
            {
                formRecords = formRecords.Where(x => x.ReportingOfficerId == request.Filters.ReporterId);
            }

            if (request.Filters.CounterSignatureId.HasValue)
            {
                formRecords = formRecords.Where(x => x.CounterSignatoryId == request.Filters.CounterSignatureId);
            }

            if (request.Filters.ReceiverId.HasValue)
            {
                formRecords = formRecords.Where(x => x.ReceiverId == request.Filters.ReceiverId);
            }

            if(request.Filters.ReportingOfficerApproval.HasValue)
            {
                formRecords = formRecords.Where(x => x.ReportingOfficerApproval == request.Filters.ReportingOfficerApproval);
            }

            if(request.Filters.CounterSignatoryApproval.HasValue)
            {
                formRecords = formRecords.Where(x => x.CounterSignatoryApproval == request.Filters.CounterSignatoryApproval);
            }

            if(request.Filters.ReceiverApproval.HasValue)
            {
                formRecords = formRecords.Where(x => x.ReceiverApproval == request.Filters.ReceiverApproval);
            }
            

            var formRecordDtos = _mapper.Map<List<FormRecordDto>>(formRecords);

            foreach (var formRecordDto in formRecordDtos)
            {
                var empDetail = await _jobDetailRepository.Where(x => x.EmpId == formRecordDto.EmpId)
                    .Include(x => x.Department)
                    .FirstOrDefaultAsync();

                if (empDetail == null || empDetail.Department == null)
                {
                    continue;
                }

                formRecordDto.Department = empDetail.Department.DepartmentName;
            }

            return formRecordDtos;
        }
    }
}
