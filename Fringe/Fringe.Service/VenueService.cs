namespace Fringe.Service;

public class VenueService : IVenueService
{
    private readonly IVenueRepository _venueRepository;

    public VenueService(IVenueRepository venueRepository)
    {
        _venueRepository = venueRepository;
    }

    public async Task<IEnumerable<VenueDto>> GetAllVenuesAsync()
    {
        var venues = await _venueRepository.GetAllVenuesAsync();

        return venues.Select(v => new VenueDto
        {
            VenueId = v.VenueId,
            VenueName = v.VenueName,
            VenueType = v.VenueTypeLookUp?.VenueType,
            MaxCapacity = v.MaxCapacity,
            ContactEmail = v.ContactEmail,
            ContactPhone = v.ContactPhone,
            IsAccessible = v.IsAccessible,
            VenueUrl = v.VenueUrl,
            LocationName = v.Location?.LocationName
        });
    }
}