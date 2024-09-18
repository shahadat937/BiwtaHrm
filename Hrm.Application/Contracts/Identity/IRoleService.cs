using Hrm.Application.DTOs.Role;
using Hrm.Application.Responses;
using Hrm.Shared.Models;

namespace Hrm.Application.Contracts.Identity
{
    public interface IRoleService
    {
        //Task<List<Employee>> GetEmployees();
        //Task<Employee> GetEmployee(string userId);
        //Task<PagedResult<UserDto>> GetUsers(QueryParams queryParams);
        //Task<BaseCommandResponse> Save(CreateUserDto user);
        Task<BaseCommandResponse> Save(AspNetRolesDto model);
        Task<BaseCommandResponse> Update(AspNetRolesDto model);
        Task<BaseCommandResponse> Delete(string Id);
        Task<object> Get();
        Task<object> GetById(string Id);
        Task<List<SelectedModel>> GetSelectedRoleList();
        Task<List<SelectedModel>> GetSelectedAllRoleList();
        Task<List<SelectedModel>> GetSelectedRoleForTraineeList();
    }
}
