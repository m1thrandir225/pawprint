namespace Domain.DTOs
{
    public class CreateShelterRequest
    {
        /**
        * Shared with Adopter
        */
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }

        /**
        * Shelter Specific
        */
        public string Name { get; set; }
        public string PhoneNumber  { get; set; }

        public string? Website { get; set; }

        public int capacity { get; set; }

        public bool isNoKill { get; set; }
    }
}