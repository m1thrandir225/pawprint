namespace Domain.DTOs
{
    public class UpdateShelterRequest
    {
        public string Name { get; set; }
        public string PhoneNumber  { get; set; }

        public string? Website { get; set; }

        public int capacity { get; set; }

        public bool isNoKill { get; set; }
    }
}