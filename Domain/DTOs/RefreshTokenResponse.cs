namespace Domain.DTOs
{
    public class RefreshTokenResponse
    {
        public string AccessToken { get; set; }

        public DateTime AccessTokenExpirationTime { get; set; }
    }
}