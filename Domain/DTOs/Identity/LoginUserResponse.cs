namespace Domain.DTOs
{
    public class LoginUserResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime AccessTokenExpirationTime { get; set; }
        public DateTime RefreshTokenExpirationTime { get; set; }

        public UserDTO User { get; set; }
    }
}