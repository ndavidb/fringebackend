namespace Fringe.Repository.Interfaces;

public interface IVenueRepository
{
    Task<IEnumerable<Venue>> GetAllVenuesAsync();
}