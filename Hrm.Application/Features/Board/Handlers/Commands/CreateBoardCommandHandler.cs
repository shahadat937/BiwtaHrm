using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Board.Validators;
using Hrm.Application.Features.Board.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Microsoft.EntityFrameworkCore;
using Hrm.Application.DTOs.Board.Validators;

namespace Hrm.Application.Features.Board.Handlers.Commands
{
    public class CreateBoardCommandHandler : IRequestHandler<CreateBoardCommand, BaseCommandResponse>
    {


        private readonly IHrmRepository<Hrm.Domain.Board> _BoardRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateBoardCommandHandler(IHrmRepository<Hrm.Domain.Board> BoardRepository, IUnitOfWork unitOfWork, IMapper mapper)
       
        {
            _BoardRepository = BoardRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _BoardRepository = BoardRepository;
        }

        public async Task<BaseCommandResponse> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateBoardDtoValidator ();
            var validatorResult = await validator.ValidateAsync(request.BoardDto);

            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                var BoardName = request.BoardDto.BoardName.ToLower();

                IQueryable<Hrm.Domain.Board> Boards = _BoardRepository.Where(x => x.BoardName.ToLower() == BoardName);


                if (BoardNameExists(request))
                {
                    response.Success = false;
                    response.Message = $"Creation Failed '{request.BoardDto.BoardName}' already exists.";


                    //response.Message = "Creation Failed, Name already exists";
                    response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();

                }
                else
                {
                    var Board = _mapper.Map<Hrm.Domain.Board>(request.BoardDto);

                    Board = await _unitOfWork.Repository<Hrm.Domain.Board>().Add(Board);
                    await _unitOfWork.Save();
                    response.Success = true;
                    response.Message = "Creation Successfull";
                    response.Id = Board.BoardId;
                }
            }

            return response;
        }
        private bool BoardNameExists(CreateBoardCommand request)
        {

            var BoardName = request.BoardDto.BoardName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.Board> Boards = _BoardRepository.Where(x => x.BoardName.Trim().ToLower().Replace(" ", string.Empty) == BoardName);

            return Boards.Any();

        }
    }
}
