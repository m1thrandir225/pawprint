namespace Domain.DTOs.Identity
{
    public class ShelterDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string? Website { get; set; }
        public string PhoneNumber { get; set; }
        public int Capacity { get; set; }
        public bool isNoKill { get; set; }
    }
}