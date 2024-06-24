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



namespace Hrm.Identity.Services
{
    public class RoleService : IRoleService
    {
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

        public Task<BaseCommandResponse> Save(string roleId, CreateRoleDto model)
        {
            throw new NotImplementedException();
        }
    }
}
