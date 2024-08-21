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
        private readonly IMapper _mapper;

        public GetFormRecordRequestHandler(IHrmRepository<Domain.FormRecord> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<FormRecordDto>> Handle(GetFormRecordRequest request, CancellationToken cancellationToken)
        {
            var formRecords = await _repository.Where(x => true)
                .Include(x => x.Form)
                .Include(x => x.Employee)
                .ToListAsync();

            var formRecordDto = _mapper.Map<List<FormRecordDto>>(formRecords);

            return formRecordDto;
        }
    }
}
