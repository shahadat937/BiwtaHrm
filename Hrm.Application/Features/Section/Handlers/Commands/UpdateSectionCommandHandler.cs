using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Section.Validators;
using Hrm.Application.DTOs.Section.ValidatorsSection;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Section.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Section.Handlers.Commands
{
    public class UpdateSectionCommandHandler : IRequestHandler<UpdateSectionCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.Section> _SectionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public UpdateSectionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Section> SectionRepository)

        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _SectionRepository = SectionRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateSectionCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateSectionDtoValidator();
            var validationResult = await validator.ValidateAsync(request.SectionDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            //var SectionName = request.SectionDto.SectionName.ToLower();
            var SectionName = request.SectionDto.SectionName.Trim().ToLower().Replace(" ", string.Empty);
            IQueryable<Hrm.Domain.Section> Sections = _SectionRepository.Where(x => x.SectionName.ToLower() == SectionName);
            if (Sections.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed '{request.SectionDto.SectionName}' already exists.";

                //response.Message = "Creation Failed Name already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }

            else
            {

                var Section = await _unitOfWork.Repository<Hrm.Domain.Section>().Get(request.SectionDto.SectionId);

                if (Section is null)
                {
                    throw new NotFoundException(nameof(Section), request.SectionDto.SectionId);
                }

                _mapper.Map(request.SectionDto, Section);

                await _unitOfWork.Repository<Hrm.Domain.Section>().Update(Section);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = Section.SectionId;

            }

            return response;
        }
    }
}
