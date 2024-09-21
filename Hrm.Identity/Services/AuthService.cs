using Hrm.Application.Constants;
using Hrm.Application.Contracts.Identity;
using Hrm.Application.Models.Identity;
using Hrm.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Responses;

namespace Hrm.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHrmRepository<Domain.AspNetUsers> _aspNetUserRepository;
        private readonly IHrmRepository<Domain.AspNetUserRoles> _aspNetUserRolesRepository;
        private readonly IHrmRepository<Domain.AspNetRoles> _aspNetRolesRepository;
        private readonly JwtSettings _jwtSettings;

        public AuthService(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager,
            IOptions<JwtSettings> jwtSettings,
            SignInManager<ApplicationUser> signInManager, IHrmRepository<Domain.AspNetUsers> aspNetUserRepository, IHrmRepository<Domain.AspNetUserRoles> aspNetUserRolesRepository, IHrmRepository<Domain.AspNetRoles> aspNetRolesRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
            _signInManager = signInManager;
            _aspNetUserRepository = aspNetUserRepository;
            _aspNetUserRolesRepository = aspNetUserRolesRepository;
            _aspNetRolesRepository = aspNetRolesRepository;
        }

        public async Task<AuthResponse> Login(AuthRequest request)
        {


            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == request.Email || x.UserName == request.Email);
            if (user == null)
            {

                throw new NotFoundException("User", request.Email);
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

            if (user.IsActive == false)
            {

                throw new Exception("User is not Active");
            }

            if (!result.Succeeded)
            {
                throw new BadRequestException($"Credentials for '{request.Email} aren't valid'.");
            }


            string id = _aspNetUserRepository.Where(x => x.UserName == request.Email || x.Email == request.Email).Select(x => x.Id).FirstOrDefault();

            string role = _aspNetUserRolesRepository.Where(x => x.UserId == id).Select(x => x.RoleId).FirstOrDefault();

            string roleName = _aspNetRolesRepository.Where(x => x.Id == role).Select(x => x.Name).FirstOrDefault();

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

            AuthResponse response = new AuthResponse
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                Username = user.UserName,
                Role = roleName,
                BranchId = user.BranchId,
                EmpId = user.EmpId
            };

            return response;
        }

        public async Task<BaseCommandResponse> Register(RegistrationRequest request)
        {
            var response = new BaseCommandResponse();


            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                EmpId = request.EmpId,
                IsActive = request.IsActive,
                EmailConfirmed = true,
                CanEditProfile = false
            };

            var existingUser = await _userManager.FindByNameAsync(request.UserName);

            //IQueryable<Domain.AspNetUsers> pNoFound = _aspNetUserRepository.Where(x => x.PNo.ToLower() == request.PNo.ToLower());

            //var existingEmail = await _userManager.FindByEmailAsync(request.Email);

            if (existingUser != null)
            {
                response.Success = false;
                response.Message = $"Registration Failed, UserName '{request.UserName}' already Exists.";
            }

            //else if (pNoFound.Any())
            //{
            //    response.Success = false;
            //    response.Message = $"Registration Failed, pNo '{request.PNo}' already Exists.";
            //}

            //else if (existingEmail != null)
            //{
            //    response.Success = false;
            //    response.Message = $"Registration Failed, Email '{request.Email}' already Exists.";
            //}

            else
            {
                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    response.Success = true;
                    response.Message = $"Register Successfull, UserName : '{request.UserName}'.";
                }
                else
                {
                    response.Success = false;
                    response.Message = $"Register Failed! '{result.Errors}'";
                }
            }

            return response;
        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var rolelist = _roleManager.Roles.Where(r => roles.Contains(r.Name)).Select(x => x.Id).ToList();
            string roleid = rolelist[0].ToString();

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaimTypes.Uid, user.Id),
                new Claim(CustomClaimTypes.Rid, roleid)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

        public async Task<BaseCommandResponse> UpdateUser(UpdateUserRequest request)
        {
            var response = new BaseCommandResponse();

            var user = await _userManager.FindByIdAsync(request.Id);

            if (user == null)
            {
                response.Success = false;
                response.Message = $"User with ID '{request.Id}' not found.";
                return response;
            }

            IQueryable<Domain.AspNetUsers> existingUser = _aspNetUserRepository.Where(x => x.UserName.ToLower() == request.UserName.ToLower() && x.Id != request.Id);

            //IQueryable<Domain.AspNetUsers> pNoFound = _aspNetUserRepository.Where(x => x.PNo.ToLower() == request.PNo.ToLower() && x.Id != request.Id);

            //IQueryable<Domain.AspNetUsers> existingEmail = _aspNetUserRepository.Where(x => x.Email.ToLower() == request.Email.ToLower() && x.Id != request.Id);

            if (existingUser.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed, UserName '{request.UserName}' already Exists.";
            }

            //else if (pNoFound.Any())
            //{
            //    response.Success = false;
            //    response.Message = $"Registration Failed, pNo '{request.PNo}' already Exists.";
            //}

            //else if (existingEmail.Any())
            //{
            //    response.Success = false;
            //    response.Message = $"Registration Failed, Email '{request.Email}' already Exists.";
            //}

            else
            {
                // Update user properties
                user.UserName = request.UserName;
                user.IsActive = request.IsActive;
                user.CanEditProfile = request.CanEditProfile;


                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    response.Success = true;
                    response.Message = $"User information updated successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = $"Failed to update user information: {string.Join(", ", result.Errors)}";
                }
            }

            return response;
        }

        public async Task<BaseCommandResponse> UpdateUserAndChangePassword(UpdateUserRequest request)
        {
            var response = new BaseCommandResponse();

            var user = await _userManager.FindByIdAsync(request.Id);

            if (user == null)
            {
                response.Success = false;
                response.Message = $"User with ID '{request.Id}' not found.";
                return response;
            }

            else
            {

                var result = await _userManager.ChangePasswordAsync(user, request.OldPassword,request.Password);

                if (result.Succeeded)
                {
                    response.Success = true;
                    response.Message = $"Password Changed successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = $"Failed to Change: {string.Join(", ", result.Errors)}";
                }
                return response;
            }

        }


        public async Task<BaseCommandResponse> ResetPassword(UpdateUserRequest request)
        {
            var response = new BaseCommandResponse();

            var user = await _userManager.FindByIdAsync(request.Id);

            if (user == null)
            {
                response.Success = false;
                response.Message = $"User with ID '{request.Id}' not found.";
                return response;
            }

            else
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var result = await _userManager.ResetPasswordAsync(user, token, request.Password);

                if (result.Succeeded)
                {
                    response.Success = true;
                    response.Message = $"Password Reset successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = $"Failed to Reset: {string.Join(", ", result.Errors)}";
                }
                return response;
            }

        }

    }
}
