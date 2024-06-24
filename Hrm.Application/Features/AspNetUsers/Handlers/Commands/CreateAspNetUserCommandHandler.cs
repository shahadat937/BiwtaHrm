using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.AspNetUsers.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.AspNetUsers.Handlers.Commands
{
    public class CreateAspNetUserCommandHandler : IRequestHandler<CreateAspNetUserCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Domain.AspNetUsers> _aspNetUserRepository;

        public CreateAspNetUserCommandHandler(IHrmRepository<Domain.AspNetUsers> aspNetUserRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _aspNetUserRepository = aspNetUserRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateAspNetUserCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            IQueryable<Domain.AspNetUsers> userNameFound = _aspNetUserRepository.Where(x => x.UserName.ToLower() == request.AspNetUserDto.UserName.ToLower());
            IQueryable<Domain.AspNetUsers> emailFound = _aspNetUserRepository.Where(x => x.Email.ToLower() == request.AspNetUserDto.Email.ToLower());
            IQueryable<Domain.AspNetUsers> pNoFound = _aspNetUserRepository.Where(x => x.PNo.ToLower() == request.AspNetUserDto.PNo.ToLower());

            if (userNameFound.Any())
            {
                response.Success = false;
                response.Message = $"Creation Failed '{request.AspNetUserDto.UserName}' already exists.";
            }
            else if (emailFound.Any())
            {
                response.Success = false;
                response.Message = $"Creation Failed '{request.AspNetUserDto.Email}' already exists.";
            }
            else if (pNoFound.Any())
            {
                response.Success = false;
                response.Message = $"Creation Failed '{request.AspNetUserDto.PNo}' already exists.";
            }
            else
            {
                Guid UserId = Guid.NewGuid();
                string passwordHash = PasswordHelper.HashPassword(request.AspNetUserDto.Password);


                var aspNetUsers = new Domain.AspNetUsers
                {
                    Id = UserId.ToString(),
                    UserName = request.AspNetUserDto.UserName,
                    NormalizedUserName = request.AspNetUserDto.UserName.ToUpper(),
                    FirstName = request.AspNetUserDto.FirstName,
                    LastName = request.AspNetUserDto.LastName,
                    Email = request.AspNetUserDto.Email,
                    NormalizedEmail = request.AspNetUserDto.Email.ToUpper(),
                    PhoneNumber = request.AspNetUserDto.PhoneNumber,
                    PNo = request.AspNetUserDto.PNo,
                    PasswordHash = passwordHash,
                    IsActive = request.AspNetUserDto.IsActive,
                };

                aspNetUsers = await _unitOfWork.Repository<Domain.AspNetUsers>().Add(aspNetUsers);
                await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
            }
            return response;
        }
        public class PasswordHelper
        {
            public static string HashPassword(string password)
            {
                using (SHA512 sha256 = SHA512.Create())
                {
                    byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    return Convert.ToBase64String(hashedBytes);
                }
            }
        }
    }
}
