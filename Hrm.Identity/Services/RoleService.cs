using AutoMapper;

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Hrm.Application.Contracts.Identity;
using Hrm.Shared.Models;
using Hrm.Application.Responses;
using Hrm.Application.DTOs.Role;
using Hrm.Identity.Models;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Models.Identity;
using Microsoft.Extensions.Options;



namespace Hrm.Identity.Services
{
    public class RoleService : IRoleService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHrmRepository<Domain.AspNetUsers> _aspNetUserRepository;
        private readonly IHrmRepository<Domain.AspNetUserRoles> _aspNetUserRolesRepository;
        private readonly IHrmRepository<Domain.AspNetRoles> _aspNetRolesRepository;
        private readonly JwtSettings _jwtSettings;

        public RoleService(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager,
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

        public Task<List<SelectedModel>> GetSelectedAllRoleList()
        {
            throw new NotImplementedException();
        }

        public Task<List<SelectedModel>> GetSelectedRoleForTraineeList()
        {
            throw new NotImplementedException();
        }

        public Task<List<SelectedModel>> GetSelectedRoleList()
        {
            throw new NotImplementedException();
        }

        public async Task<BaseCommandResponse> Save(string roleId, CreateRoleDto request)
        {
            var response = new BaseCommandResponse();

            var role = new ApplicationRole
            {
                Name = request.RoleName
            };

            var existingRole = await _roleManager.FindByNameAsync(request.RoleName);

            if (existingRole != null)
            {
                response.Success = false;
                response.Message = $"Creation Failed, RoleName '{request.RoleName}' already Exists.";
            }

            else
            {
                var result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    response.Success = true;
                    response.Message = $"Register Successfull, RoleName : '{request.RoleName}'.";
                }
                else
                {
                    response.Success = false;
                    response.Message = $"Register Failed! '{result.Errors}'";
                }
            }

            return response;
            //throw new NotImplementedException();

        }
    }
}
