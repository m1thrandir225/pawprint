using Domain;
using Repository.Interface;

namespace Repository.Implementations;

public class PetTypeRepository : IPetTypeRepository
{
    private readonly ApplicationDbContext _context;

    public PetTypeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<PetType> GetAll()
    {
        return _context.PetTypes.ToList();
    }

    public PetType GetById(Guid petTypeId)
    {
        return _context.PetTypes.Find(petTypeId);
    }

    public void Insert(PetType petType)
    {
        _context.PetTypes.Add(petType);
    }

    public void Update(PetType petType)
    {
        _context.PetTypes.Update(petType);
    }

    public void Delete(Guid petTypeId)
    {
        var petType = _context.PetTypes.Find(petTypeId);
        if (petType != null)
        {
            _context.PetTypes.Remove(petType);
        }
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}