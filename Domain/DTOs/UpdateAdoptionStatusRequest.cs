﻿namespace Domain.DTOs;

public class UpdateAdoptionStatusRequest
{
    public string Name { get; set; }

    public UpdateAdoptionStatusRequest(string name)
    {
        Name = name;
    }
}