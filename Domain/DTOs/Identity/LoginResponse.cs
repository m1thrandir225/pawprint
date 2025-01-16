namespace Domain.DTOs
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public string Email { get; set; }
        public string Address { get; set; }

        public Shelter ShelterDetailsDto { get; set; }
        public User UserDetailsDto  { get; set; }
    }
}