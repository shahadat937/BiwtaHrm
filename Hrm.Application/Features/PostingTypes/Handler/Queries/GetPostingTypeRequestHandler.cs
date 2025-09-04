using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.PostingType;
using Hrm.Application.DTOs.PostingType;
using Hrm.Application.DTOs.TrainingType;
using Hrm.Application.Features.PostingTypes.Request.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.PostingTypes.Handler.Queries
{
    public class GetPostingTypeRequestHandler : IRequestHandler<GetPostingTypeRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.PostingType> _PostingTypeRepository;
        private readonly IMapper _mapper;
        public GetPostingTypeRequestHandler(IHrmRepository<Hrm.Domain.PostingType> PostingTypeRepository, IMapper mapper)
        {
            _PostingTypeRepository = PostingTypeRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetPostingTypeRequest request, CancellationToken cancellationToken)
        {
            // Fetch blood groups from repository
            IQueryable<Hrm.Domain.PostingType> PostingTypes = _PostingTypeRepository.Where(x => true);

            // Order blood groups by descending order
            PostingTypes = PostingTypes.OrderByDescending(x => x.Id);

            // Map the ordered blood groups to PostingTypeDto
            var PostingTypeDtos = _mapper.Map<List<PostingTypeDto>>(PostingTypes.ToList());

            return PostingTypeDtos;
        }
    }
}