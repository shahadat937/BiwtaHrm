using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.FieldRecord;
using Hrm.Application.Features.FieldRecord.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FieldRecord.Handlers.Queries
{
    public class GetFieldRecordByFormRecordIdRequestHandler: IRequestHandler<GetFieldRecordByFormRecordIdRequest, List<FieldRecordDto>>
    {
        private readonly IHrmRepository<Hrm.Domain.FieldRecord> _repository;
        private readonly IMapper _mapper;

        public GetFieldRecordByFormRecordIdRequestHandler(IHrmRepository<Domain.FieldRecord> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<FieldRecordDto>> Handle(GetFieldRecordByFormRecordIdRequest request, CancellationToken cancellationToken)
        {
            var fieldRecords = await _repository.Where(x => x.FormRecordId == request.FormRecordId)
                .Include(x => x.FormField).OrderBy(x => x.FormField.FieldName)
                .ToListAsync();

            var fieldRecordDtos =_mapper.Map<List<FieldRecordDto>>(fieldRecords);

            return fieldRecordDtos;
        }
    }
}
