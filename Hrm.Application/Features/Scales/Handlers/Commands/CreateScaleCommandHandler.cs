using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Scale.Validators;
using Hrm.Application.Features.Scales.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Scales.Handlers.Commands
{
    public class CreateScaleCommandHandler : IRequestHandler<CreateScaleCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.Scale> _scaleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateScaleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Scale> scaleRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _scaleRepository = scaleRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateScaleCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateScaleDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ScaleDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var scaleName = request.ScaleDto.ScaleName.ToLower();

                IQueryable<Hrm.Domain.Scale> Scale = _scaleRepository.Where(x => x.ScaleName.ToLower() == scaleName);


                if (ScaleNameExists(request))
                {
                    response.Success = false;
                    //response.Message = "Creation Failed Name already exists.";
                    response.Message = $"Creation Failed '{request.ScaleDto.ScaleName}' already exists.";

                    response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

                }
                else
                {
                    var ScaleName = _mapper.Map<Hrm.Domain.Scale>(request.ScaleDto);

                    ScaleName = await _unitOfWork.Repository<Hrm.Domain.Scale>().Add(ScaleName);
                    await _unitOfWork.Save();


                    response.Success = true;
                    response.Message = "Creation Successful";
                    response.Id = ScaleName.ScaleId;
                }
            }

            return response;
        }
        private bool ScaleNameExists(CreateScaleCommand request)
        {
            var ScaleName = request.ScaleDto.ScaleName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.Scale> Scales = _scaleRepository.Where(x => x.ScaleName.Trim().ToLower().Replace(" ", string.Empty) == ScaleName);

            return Scales.Any();
        }
    }
}
