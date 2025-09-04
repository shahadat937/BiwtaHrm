using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.Features.PostingTypes.Request.Commands;

namespace Hrm.Application.Features.PostingTypes.Handlers.Commands
{
    public class UpdatePostingTypeCommandHandler : IRequestHandler<UpdatePostingTypeCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.PostingType> _PostingTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdatePostingTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.PostingType> PostingTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _PostingTypeRepository = PostingTypeRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdatePostingTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            //var PostingTypeName = request.PostingTypeDto.PostingTypeName.ToLower();
            var PostingTypeName = request.PostingTypeDto.TypeName.Trim().ToLower().Replace(" ", string.Empty);
            IQueryable<Hrm.Domain.PostingType> PostingTypes = _PostingTypeRepository.Where(x => x.TypeName.ToLower().Replace(" ", string.Empty) == PostingTypeName && x.TypeNameBangla.ToLower().Replace(" ", string.Empty) == request.PostingTypeDto.TypeNameBangla.Trim().ToLower().Replace(" ", string.Empty) && x.Id != request.PostingTypeDto.Id);



            if (PostingTypes.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed '{request.PostingTypeDto.TypeName}' already exists.";


            }

            else
            {

                var PostingType = await _unitOfWork.Repository<Hrm.Domain.PostingType>().Get(request.PostingTypeDto.Id);

                if (PostingType is null)
                {
                    throw new NotFoundException(nameof(PostingType), request.PostingTypeDto.Id);
                }

                _mapper.Map(request.PostingTypeDto, PostingType);

                await _unitOfWork.Repository<Hrm.Domain.PostingType>().Update(PostingType);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = PostingType.Id;

            }

            return response;
        }
    }
}
