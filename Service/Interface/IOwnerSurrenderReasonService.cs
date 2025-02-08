using Domain.DTOs.OwnerSurrenderReason;

namespace Service.Interface;

using Domain;
using Domain.DTOs;

public interface IOwnerSurrenderReasonService : ICRUDService<OwnerSurrenderReason, CreateOwnerSurrenderReasonRequest, UpdateOwnerSurrenderReasonRequest>
{
}