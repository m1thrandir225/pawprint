namespace Domain.DTOs.JWT
{
    public class RefreshTokenResponse
    {
        public string AccessToken { get; set; }

        public DateTime AccessTokenExpirationTime { get; set; }
    }
}