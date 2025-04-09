namespace Fringe.Repository;

public class LocationRepository : ILocationRepository
{
    private readonly FringeDbContext _context;

    public LocationRepository(FringeDbContext context)
    {
        _context = context;
    }
    
    // retrieve all locations
    public async Task<List<Location>> GetAllAsync() =>
        await _context.Locations.ToListAsync();

    // get location by id
    public async Task<Location?> GetByIdAsync(int id) =>
        await _context.Locations.FindAsync(id);
    
    // save location
    public async Task<Location> AddAsync(Location location)
    {
        _context.Locations.Add(location);
        await _context.SaveChangesAsync();
        return location;
    }

    // update location
    public async Task UpdateAsync(Location location)
    {
        _context.Locations.Update(location);
        await _context.SaveChangesAsync();
    }

    // delete location
    public async Task<bool> DeleteAsync(int id)
    {
        var location = await _context.Shows.FindAsync(id);
        if (location == null)
            return false;

        // Soft delete for location by changing status
        location.Active = false;
        return await _context.SaveChangesAsync() > 0;
    }
}