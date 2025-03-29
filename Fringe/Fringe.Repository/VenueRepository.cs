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
}