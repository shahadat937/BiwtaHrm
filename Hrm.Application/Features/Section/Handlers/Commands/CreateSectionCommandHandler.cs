using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Section.Validators;
using Hrm.Application.Features.Section.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Microsoft.EntityFrameworkCore;
using Hrm.Application.DTOs.SectionSection.Validators;

namespace Hrm.Application.Features.Section.Handlers.Commands
{
    public class CreateSectionCommandHandler : IRequestHandler<CreateSectionCommand, BaseCommandResponse>
    {


        private readonly IHrmRepository<Hrm.Domain.Section> _SectionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateSectionCommandHandler(IHrmRepository<Hrm.Domain.Section> SectionRepository, IUnitOfWork unitOfWork, IMapper mapper)
       
        {
            _SectionRepository = SectionRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _SectionRepository = SectionRepository;
        }

        public async Task<BaseCommandResponse> Handle(CreateSectionCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateSectionDtoValidator ();
            var validatorResult = await validator.ValidateAsync(request.SectionDto);

            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                //var SectionName = request.SectionDto.SectionName.ToLower();

                //IQueryable<Hrm.Domain.Section> Sections = _SectionRepository.Where(x => x.SectionName.ToLower() == SectionName);


                //if (SectionNameExists(request))
                //{
                //    response.Success = false;
                //    response.Message = $"Creation Failed '{request.SectionDto.SectionName}' already exists.";


                //    //response.Message = "Creation Failed, Name already exists";
                //    response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();

                //}
                //else
                //{
                    var Section = _mapper.Map<Hrm.Domain.Section>(request.SectionDto);

                    Section = await _unitOfWork.Repository<Hrm.Domain.Section>().Add(Section);
                    await _unitOfWork.Save();
                    response.Success = true;
                    response.Message = "Creation Successfull";
                    response.Id = Section.SectionId;
                //}
            }

            return response;
        }
        //private bool SectionNameExists(CreateSectionCommand request)
        //{

        //    var SectionName = request.SectionDto.SectionName.Trim().ToLower().Replace(" ", string.Empty);

        //    IQueryable<Hrm.Domain.Section> Sections = _SectionRepository.Where(x => x.SectionName.Trim().ToLower().Replace(" ", string.Empty) == SectionName);

        //    return Sections.Any();

        //}
    }
}
