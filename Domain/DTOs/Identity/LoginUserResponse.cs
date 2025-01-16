namespace Domain.DTOs
{
    public class LoginUserResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public UserDTO User { get; set; }
    }
}