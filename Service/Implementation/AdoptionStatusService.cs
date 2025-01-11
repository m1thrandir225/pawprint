﻿namespace Service.Interface;

using Domain;
using Domain.DTOs;

public interface IAdoptionStatusService : ICRUDService<AdoptionStatus, CreateAdoptionStatusRequest, UpdateAdoptionStatusRequest>
{
}