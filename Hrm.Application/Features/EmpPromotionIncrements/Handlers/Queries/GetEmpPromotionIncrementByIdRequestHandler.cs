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
    public class GetEmpPromotionIncrementByIdRequestHandler : IRequestHandler<GetEmpPromotionIncrementByIdRequest, object>
    {

        private readonly IHrmRepository<EmpPromotionIncrement> _EmpPromotionIncrementRepository;
        private readonly IMapper _mapper;
        public GetEmpPromotionIncrementByIdRequestHandler(IHrmRepository<EmpPromotionIncrement> EmpPromotionIncrementRepository, IMapper mapper)
        {
            _EmpPromotionIncrementRepository = EmpPromotionIncrementRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetEmpPromotionIncrementByIdRequest request, CancellationToken cancellationToken)
        {
            var EmpPromotionIncrements = await _EmpPromotionIncrementRepository.FindOneAsync(x => x.Id == request.Id);


            var EmpPromotionIncrementDtos = _mapper.Map<EmpPromotionIncrementDto>(EmpPromotionIncrements);

            return EmpPromotionIncrementDtos;
        }
    }
}

