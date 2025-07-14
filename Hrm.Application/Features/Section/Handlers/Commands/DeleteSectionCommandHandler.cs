using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using SendGrid;

namespace hrm.Application.Features.Sections.Handlers.Commands
{
    public class DeleteSectionCommandHandler : IRequestHandler<DeleteSectionCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteSectionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteSectionCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var Section = await _unitOfWork.Repository<Section>().Get(request.SectionId);

            if (Section == null)
                throw new NotFoundException(nameof(Section), request.SectionId);


            await _unitOfWork.Repository<Hrm.Domain.Section>().Delete(Section);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = Section.SectionId;

            return response;
        }
    }
}
