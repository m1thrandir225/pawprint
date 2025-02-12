namespace Domain.DTOs.JWT
{
    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; }
        public string Email { get; set; }
    }
}