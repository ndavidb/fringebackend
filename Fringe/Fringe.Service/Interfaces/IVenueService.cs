namespace Fringe.Service.Interfaces;

public interface IVenueService
{
    Task<IEnumerable<VenueDto>> GetAllVenuesAsync();
}