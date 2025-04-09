namespace Fringe.Repository;

public class ShowRepository : IShowRepository
{
    private readonly FringeDbContext _context;

    public ShowRepository(FringeDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Show>> GetAllShowsAsync()
    {
        return await _context.Shows
            .Include(s => s.Venue)
            .Include(s => s.ShowTypeLookup)
            .Include(s => s.AgeRestrictionLookup)
            .Include(s => s.TicketType)
            .Where(s => s.Active)
            .ToListAsync();
    }

    public async Task<Show> GetShowByIdAsync(int showId)
    {
        return await _context.Shows
            .Include(s => s.Venue)
            .Include(s => s.ShowTypeLookup)
            .Include(s => s.AgeRestrictionLookup)
            .Include(s => s.TicketType)
            .FirstOrDefaultAsync(s => s.ShowId == showId);
    }

    public async Task<Show> CreateShowAsync(Show show)
    {
        await _context.Shows.AddAsync(show);
        await _context.SaveChangesAsync();
        return show;
    }

    public async Task<Show> UpdateShowAsync(Show show)
    {
        _context.Shows.Update(show);
        await _context.SaveChangesAsync();
        return show;
    }

    public async Task<bool> DeleteShowAsync(int showId)
    {
        var show = await _context.Shows.FindAsync(showId);
        if (show == null)
            return false;

        // Soft delete
        show.Active = false;
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> ShowExistsAsync(int showId)
    {
        return await _context.Shows.AnyAsync(s => s.ShowId == showId);
    }

    public async Task<IEnumerable<AgeRestrictionLookup>> GetAllAgeRestrictionsAsync()
    {
        return await _context.AgeRestrictionLookups.ToListAsync();
    }

    public async Task<IEnumerable<ShowTypeLookup>> GetAllShowTypesAsync()
    {
        return await _context.ShowTypeLookups.ToListAsync();
    }
}