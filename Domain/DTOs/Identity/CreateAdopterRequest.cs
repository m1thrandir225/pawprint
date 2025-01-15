namespace Domain.DTOs
{
    public class CreateAdopterRequest 
    {
        /**
        * Shared with Shelter
        */
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        

        /**
        * Adopter Specific
        */
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool HasChildren { get; set; }
        public bool HasOtherPets { get; set; }
        public string HomeType { get; set; }
    }
}