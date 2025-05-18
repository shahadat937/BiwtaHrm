using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.OfficeOrders.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.OfficeOrders.Handlers.Commands
{
    public class UpdateOfficeOrderCommandHandler : IRequestHandler<UpdateOfficeOrderCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateOfficeOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateOfficeOrderCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var OfficeOrder = await _unitOfWork.Repository<OfficeOrder>().Get(request.OfficeOrderDto.Id);

            var officeOrderDto = _mapper.Map(request.OfficeOrderDto, OfficeOrder);

            if (request.OfficeOrderDto.OrderFile != null)
            {
                if (!string.IsNullOrEmpty(OfficeOrder.FileUrl))
                {
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\OfficeOrder", OfficeOrder.FileUrl);
                    if (File.Exists(oldFilePath))
                    {
                        File.Delete(oldFilePath);
                    }
                }

                var orderFile = Path.GetFileName(request.OfficeOrderDto.OrderFile.FileName);
                string uniqueImageName = Guid.NewGuid().ToString() + "_" + orderFile;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\OfficeOrder", uniqueImageName);

                using (var photoSteam = new FileStream(filePath, FileMode.Create))
                {
                    await request.OfficeOrderDto.OrderFile.CopyToAsync(photoSteam);
                }

                officeOrderDto.FileUrl = uniqueImageName;

            }
            else
            {
                officeOrderDto.FileUrl = request.OfficeOrderDto.FileUrl;
            }



            await _unitOfWork.Repository<OfficeOrder>().Update(officeOrderDto);
            await _unitOfWork.Save();


            response.Success = true;
            response.Message = "Update Successful";
            response.Id = OfficeOrder.Id;

            return response;
        }
    }
}