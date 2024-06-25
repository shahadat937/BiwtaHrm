using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Board.Validators;
using Hrm.Application.DTOs.Board.ValidatorsBoard;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Board.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Board.Handlers.Commands
{
    public class UpdateBoardCommandHandler : IRequestHandler<UpdateExamTypeCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.Board> _BoardRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public UpdateBoardCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Board> BoardRepository)

        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _BoardRepository = BoardRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateExamTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateBoardDtoValidator();
            var validationResult = await validator.ValidateAsync(request.BoardDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            //var BoardName = request.BoardDto.BoardName.ToLower();
            var BoardName = request.BoardDto.BoardName.Trim().ToLower().Replace(" ", string.Empty);
            IQueryable<Hrm.Domain.Board> Boards = _BoardRepository.Where(x => x.BoardName.ToLower() == BoardName);
            if (Boards.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed '{request.BoardDto.BoardName}' already exists.";

                //response.Message = "Creation Failed Name already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }

            else
            {

                var Board = await _unitOfWork.Repository<Hrm.Domain.Board>().Get(request.BoardDto.BoardId);

                if (Board is null)
                {
                    throw new NotFoundException(nameof(Board), request.BoardDto.BoardId);
                }

                _mapper.Map(request.BoardDto, Board);

                await _unitOfWork.Repository<Hrm.Domain.Board>().Update(Board);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = Board.BoardId;

            }

            return response;
        }
    }
}
