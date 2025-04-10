namespace Fringe.Repository.Interfaces;

public interface IVenueRepository
{
    Task<IEnumerable<Venue>> GetAllVenuesAsync();
    
    Task<Venue?> GetByIdAsync(int id);
    
    Task AddAsync(Venue venue);
    
    Task UpdateAsync(Venue venue);
    
    Task DeleteAsync(int id);
}