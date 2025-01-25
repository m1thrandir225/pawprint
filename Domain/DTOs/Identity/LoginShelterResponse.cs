namespace Domain.DTOs
{
    public class LoginShelterResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public DateTime AccessTokenExpirationTime { get; set; }
        public DateTime RefreshTokenExpirationTime { get; set; }

        public ShelterDTO Shelter { get; set; }
    }
}