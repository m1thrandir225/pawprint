namespace Domain.DTOs;

public class UpdateVeterinarianRequest
{
    public string Name { get; set; }
    public string ContactNumber { get; set; }
    public string Email { get; set; }

    public UpdateVeterinarianRequest(string name, string contactNumber, string email)
    {
        Name = name;
        ContactNumber = contactNumber;
        Email = email;
    }
}