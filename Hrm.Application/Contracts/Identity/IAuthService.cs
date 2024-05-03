using Hrm.Application.Models.Identity;
using Hrm.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest request);
        Task<BaseCommandResponse> Register(RegistrationRequest request);

    }
}
