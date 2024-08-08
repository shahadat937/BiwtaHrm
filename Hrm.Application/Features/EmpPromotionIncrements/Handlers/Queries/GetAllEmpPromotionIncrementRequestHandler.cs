using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpPromotionIncrement;
using Hrm.Application.Features.EmpPromotionIncrements.Requests.Queries;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpPromotionIncrements.Handlers.Queries
{
    public class GetAllEmpPromotionIncrementRequestHandler : IRequestHandler<GetAllEmpPromotionIncrementRequest, object>
    {

        private readonly IHrmRepository<EmpPromotionIncrement> _EmpPromotionIncrementRepository;
        private readonly IMapper _mapper;
        public GetAllEmpPromotionIncrementRequestHandler(IHrmRepository<Hrm.Domain.EmpPromotionIncrement> EmpPromotionIncrementRepository, IMapper mapper)
        {
            _EmpPromotionIncrementRepository = EmpPromotionIncrementRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetAllEmpPromotionIncrementRequest request, CancellationToken cancellationToken)
        {
            IQueryable<EmpPromotionIncrement> EmpPromotionIncrements = _EmpPromotionIncrementRepository.Where(x => true);

            EmpPromotionIncrements = EmpPromotionIncrements.OrderByDescending(x => x.Id);

            var EmpPromotionIncrementDtos = _mapper.Map<List<EmpPromotionIncrementDto>>(EmpPromotionIncrements);

            return EmpPromotionIncrementDtos;
        }
    }
}
