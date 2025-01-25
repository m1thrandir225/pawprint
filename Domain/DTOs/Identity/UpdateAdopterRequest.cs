namespace Domain.DTOs
{
    public class UpdateAdopterRequest 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool HasChildren { get; set; }
        public bool HasOtherPets { get; set; }
        public string HomeType { get; set; }
    }
}