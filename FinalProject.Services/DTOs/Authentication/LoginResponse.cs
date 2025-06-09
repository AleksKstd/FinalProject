namespace FinalProject.Services.DTOs.Authentication
{
    public class LoginResponse : BaseResponse
    {
        public int? UserId { get; set; }
        public string? Username { get; set; }
        public string? Name { get; set; }
    }
}
