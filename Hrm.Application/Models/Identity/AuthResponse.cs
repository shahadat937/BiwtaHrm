namespace Hrm.Application.Models.Identity
{
    public class AuthResponse
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public string BranchId { get; set; }
        public int? EmpId { get; set; }
    }
}
