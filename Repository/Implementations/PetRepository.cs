using Domain;
using Repository.Interface;

namespace Repository.Implementations;

public class PetRepository : IPetRepository
{
    private readonly ApplicationDbContext _context;

    public PetRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Pet> GetAll()
    {
        return _context.Pets.ToList();
    }

    public Pet GetById(Guid petId)
    {
        return _context.Pets.Find(petId);
        // return _context.Pets.FirstOrDefault(p => p.Id == pet);
    }

    public void Insert(Pet pet)
    {
        _context.Pets.Add(pet);
    }

    public void Update(Pet pet)
    {
        _context.Pets.Update(pet);
    }

    public void Delete(Guid petId)
    {
        var pet = _context.Pets.Find(petId);
        if (pet != null)
        {
            _context.Pets.Remove(pet);
        }
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}