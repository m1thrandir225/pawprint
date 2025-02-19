namespace Domain.DTOs.Identity
{
    public class UserDTO
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool HasChildren { get; set; }
        public bool HasOtherPets { get; set; }
        public string HomeType { get; set; }

        public bool? IsAdmin { get; set; }
    }
}