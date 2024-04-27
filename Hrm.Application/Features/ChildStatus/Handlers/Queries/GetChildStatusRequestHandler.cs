using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.ChildStatus;
using Hrm.Application.DTOs.ChildStatus;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.ChildStatus.Requests.Queries;
using Hrm.Application.Models;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.ChildStatus.Handlers.Queries
{
    public class GetChildStatusRequestHandler : IRequestHandler<GetChildStatusRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.ChildStatus> _ChildStatusRepository;
        private readonly IMapper _mapper;
        public GetChildStatusRequestHandler(IHrmRepository<Hrm.Domain.ChildStatus> ChildStatusRepository, IMapper mapper)
        {
            _ChildStatusRepository = ChildStatusRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetChildStatusRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.ChildStatus> ChildStatus = _ChildStatusRepository.Where(x => true);
            ChildStatus = ChildStatus.OrderByDescending(x => x.ChildStatusId);
            //BankBranchs = BankBranchs.OrderByDescending(x => x.BankBranchId);
            var ChildStatusDtos = await Task.Run(() => _mapper.Map<List<ChildStatusDto>>(ChildStatus));

            return ChildStatusDtos;
        }
    }
}