using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Form.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Form.Handlers.Commands
{
    public class CreateFormDataCommandHandler: IRequestHandler<CreateFormDataCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateFormDataCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateFormDataCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine(request.formData);

            var form = await _unitOfWork.Repository<Hrm.Domain.Form>().GetAll();

            var response = new BaseCommandResponse();

            return response;
        }
    }
}
