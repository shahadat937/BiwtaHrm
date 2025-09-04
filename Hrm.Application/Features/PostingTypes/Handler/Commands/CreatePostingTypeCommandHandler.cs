using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.PostingTypes.Request.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.PostingTypes.Handler.Commands
{
    public class CreatePostingTypeCommandHandler : IRequestHandler<CreatePostingTypeCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.PostingType> _PostingTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreatePostingTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.PostingType> PostingTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _PostingTypeRepository = PostingTypeRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreatePostingTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

                var PostingTypeName = request.PostingTypeDto.TypeName.ToLower();

                IQueryable<Hrm.Domain.PostingType> PostingTypes = _PostingTypeRepository.Where(x => x.TypeName.ToLower().Replace(" ", string.Empty) == PostingTypeName && x.TypeNameBangla.ToLower().Replace(" ", string.Empty) == request.PostingTypeDto.TypeNameBangla.Trim().ToLower().Replace(" ", string.Empty) && x.Id != request.PostingTypeDto.Id);

            if (PostingTypes.Any())
                {
                    response.Success = false;
                   //response.Message = "Creation Failed Name already exists.";
                    response.Message = $"Creation Failed '{request.PostingTypeDto.TypeName}' already exists.";


                }
                else
                {
                    var PostingType = _mapper.Map<Hrm.Domain.PostingType>(request.PostingTypeDto);

                    PostingType = await _unitOfWork.Repository<Hrm.Domain.PostingType>().Add(PostingType);
                    await _unitOfWork.Save();


                    response.Success = true;
                    response.Message = "Creation Successful";
                    response.Id = PostingType.Id;
                }

            return response;
        }
    }
}
