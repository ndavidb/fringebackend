namespace Fringe.Repository;

// Handles Venue data access logic
public class VenueRepository : IVenueRepository
{
    private readonly FringeDbContext _context;

    public VenueRepository(FringeDbContext context)
    {
        _context = context;
    }

    // Retrieves all venues and includes associated Location data
    public async Task<IEnumerable<Venue>> GetAllVenuesAsync()
    {
        return await _context.Venues
            .Include(v => v.Location)
            .Include(v => v.VenueTypeLookUp)
            .ToListAsync();
    }
    
    // Retrieves a venue by id
    public async Task<Venue?> GetByIdAsync(int id)
    {
        return await _context.Venues
            .Include(v => v.Location)
            .Include(v => v.VenueTypeLookUp)
            .FirstOrDefaultAsync(v => v.VenueId == id);
    }

    public async Task AddAsync(Venue venue)
    {
        _context.Venues.Add(venue);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(Venue venue)
    {
        _context.Venues.Update(venue);
        await _context.SaveChangesAsync();
    }
    
    
    public async Task DeleteAsync(int id)
    {
        var venue = await _context.Venues.FindAsync(id);
        if (venue == null)
            throw new InvalidOperationException("Venue not found");

        venue.Active = false; // Soft delete by deactivating
        await _context.SaveChangesAsync();
    }
}