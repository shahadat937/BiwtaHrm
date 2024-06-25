using AutoMapper;

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.Models;

using System.ComponentModel.DataAnnotations;
using Hrm.Application.Responses;
using Hrm.Application.DTOs.User;
using Hrm.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Hrm.Application.Constants;
using Hrm.Application.Contracts.Persistence;

using Microsoft.EntityFrameworkCore;
using Hrm.Application.Contracts.Identity;
using Hrm.Shared.Models;
using Hrm.Application.DTOs.Role;
using Hrm.Application.Models.Identity;
using Hrm.Application.DTOs.Common;


namespace Hrm.Identity.Services
{
    public class UserService : IUserService
    {
        public Task<BaseCommandResponse> DeleteUser(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetEmployee(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetEmployeeByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Employee>> GetEmployees()
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<UserDto>> GetTeacherUsers(QueryParams queryParams)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> GetUserById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<UserDto>> GetUsers(QueryParams queryParams)
        {
            throw new NotImplementedException();
        }

        public Task<BaseCommandResponse> ResetPassword(string userId, CreateUserDto user)
        {
            throw new NotImplementedException();
        }

        public Task<BaseCommandResponse> Save(string userId, CreateUserDto user)
        {
            throw new NotImplementedException();
        }

        public Task<BaseCommandResponse> UpdateUser(string userId, UpdateEmailPhoneDto user)
        {
            throw new NotImplementedException();
        }

        public Task<BaseCommandResponse> UpdateUserPassword(string userId, PasswordChangeDto userDto)
        {
            throw new NotImplementedException();
        }
    }
}
