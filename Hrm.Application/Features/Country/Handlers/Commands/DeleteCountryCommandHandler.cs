using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Country.Requests.Commands;
using Hrm.Application.Features.TrainingType.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Country.Handlers.Commands
{
    public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteCountryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var Country = await _unitOfWork.Repository<Hrm.Domain.Country>().Get(request.CountryId);

            if (Country == null)
            {
                throw new NotFoundException(nameof(Country), request.CountryId);
            }

            await _unitOfWork.Repository<Hrm.Domain.Country>().Delete(Country);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = Country.CountryId;

            return response;
        }
    }
}
