using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Scale.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Scales.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Scales.Handlers.Commands
{
    public class UpdateScaleCommandHandler : IRequestHandler<UpdateScaleCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.Scale> _scaleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateScaleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Scale> scaleRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _scaleRepository = scaleRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateScaleCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateScaleDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ScaleDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var Scale = await _unitOfWork.Repository<Hrm.Domain.Scale>().Get(request.ScaleDto.ScaleId);

            if (Scale is null)
            {
                throw new NotFoundException(nameof(Scale), request.ScaleDto.ScaleId);
            }

            //var scaleName = request.ScaleDto.ScaleName.ToLower();
            var scaleName = request.ScaleDto.ScaleName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.Scale> scales = _scaleRepository.Where(x => x.ScaleName.ToLower() == scaleName);


            if (scales.Any())
            {
                response.Success = false;
                // response.Message = "Creation Failed Name already exists.";
                response.Message = $"Update Failed '{request.ScaleDto.ScaleName}' already exists.";

                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }
            else
            {

                _mapper.Map(request.ScaleDto, Scale);

                await _unitOfWork.Repository<Hrm.Domain.Scale>().Update(Scale);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successful";
                response.Id = Scale.ScaleId;

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
