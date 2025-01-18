using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.FormGroup;
using Hrm.Application.Features.FormGroup.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.FormGroup.Handlers.Queries
{
    public class GetFormGroupRequestHandler : IRequestHandler<GetFormGroupRequest,List<FormGroupDto>>
    {
        private readonly IHrmRepository<Hrm.Domain.FormGroup> _formGroupRepo;
        private readonly IMapper _mapper;

        public GetFormGroupRequestHandler(IHrmRepository<Domain.FormGroup> formGroupRepo, IMapper mapper)
        {
            _formGroupRepo = formGroupRepo;
            _mapper = mapper;
        }

        public async Task<List<FormGroupDto>> Handle(GetFormGroupRequest request, CancellationToken cancellationToken)
        {
            var formGroup = _formGroupRepo.Where(x => true)
                .Include(x => x.ParentField)
                .Include(x => x.ChildField);

            List<FormGroupDto> formGroupDtos = _mapper.Map<List<FormGroupDto>>(await formGroup.ToListAsync());

            return formGroupDtos;
        }
    }
}
