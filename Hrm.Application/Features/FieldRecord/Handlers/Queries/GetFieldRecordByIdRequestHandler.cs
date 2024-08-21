using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.FieldRecord;
using Hrm.Application.Features.FieldRecord.Requests.Queries;
using Hrm.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FieldRecord.Handlers.Queries
{
    public class GetFieldRecordByIdRequestHandler: IRequestHandler<GetFieldRecordByIdRequest,FieldRecordDto>
    {
        private readonly IHrmRepository<Hrm.Domain.FieldRecord> _repository;
        private readonly IMapper _mapper;

        public GetFieldRecordByIdRequestHandler(IHrmRepository<Domain.FieldRecord> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<FieldRecordDto> Handle(GetFieldRecordByIdRequest request, CancellationToken cancellationToken)
        {
            var fieldRecord = await _repository.Where(x => x.FieldRecordId == request.FieldRecordId)
                .Include(x => x.FormField)
                .FirstOrDefaultAsync();

            if (fieldRecord == null)
            {
                throw new NotFoundException(nameof(fieldRecord), request.FieldRecordId);
            }

            var fieldRecordDto = _mapper.Map<FieldRecordDto>(fieldRecord);

            return fieldRecordDto;
        }
    }
}
