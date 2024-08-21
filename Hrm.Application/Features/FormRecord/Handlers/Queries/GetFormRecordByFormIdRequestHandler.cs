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
    public class GetFormRecordByFormIdRequestHandler: IRequestHandler<GetFormRecordByFormIdRequest,List<FormRecordDto>>
    {
        private readonly IHrmRepository<Hrm.Domain.FormRecord> _repository;
        private readonly IMapper _mapper;

        public GetFormRecordByFormIdRequestHandler(IHrmRepository<Domain.FormRecord> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<FormRecordDto>> Handle(GetFormRecordByFormIdRequest request, CancellationToken cancellationToken)
        {
            var formRecords = await _repository.Where(x => x.FormId == request.FormId)
                .Include(x => x.Form)
                .Include(x => x.Employee)
                .ToListAsync();

            var formRecordDtos = _mapper.Map<List<FormRecordDto>>(formRecords);

            return formRecordDtos;
        }
    }
}
