using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.OfficeOrders.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.OfficeOrders.Handlers.Commands
{
    public class CreateOfficeOrderCommandHandler : IRequestHandler<CreateOfficeOrderCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateOfficeOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateOfficeOrderCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var officeOrderDto = _mapper.Map<OfficeOrder>(request.OfficeOrderDto);

            if (request.OfficeOrderDto.OrderFile != null)
            {
                var orderFile = Path.GetFileName(request.OfficeOrderDto.OrderFile.FileName);
                string uniqueImageName = Guid.NewGuid().ToString() + "_" + orderFile;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\OfficeOrder", uniqueImageName);

                using (var photoSteam = new FileStream(filePath, FileMode.Create))
                {
                    await request.OfficeOrderDto.OrderFile.CopyToAsync(photoSteam);
                }

                officeOrderDto.FileUrl = uniqueImageName;

            }


            await _unitOfWork.Repository<OfficeOrder>().Add(officeOrderDto);
            await _unitOfWork.Save();


            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = officeOrderDto.Id;

            return response;
        }
    }
}
