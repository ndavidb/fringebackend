using AutoMapper;
namespace Fringe.Service;

public class VenueService : IVenueService
{
    private readonly IVenueRepository _venueRepository;
    private readonly IMapper _mapper;

    public VenueService(IVenueRepository venueRepository, IMapper mapper)
    {
        _venueRepository = venueRepository;
        _mapper = mapper;
    }
    
    // Get venue list
    public async Task<IEnumerable<VenueDto>> GetAllVenuesAsync()
    {
        var venues = await _venueRepository.GetAllVenuesAsync();
        return _mapper.Map<IEnumerable<VenueDto>>(venues);
    }

    // get venue by ID
    public async Task<VenueDto?> GetVenueByIdAsync(int id)
    {
        var venue = await _venueRepository.GetByIdAsync(id);
        return venue == null ? null : _mapper.Map<VenueDto>(venue);
    }

    // save a venue
    public async Task<VenueDto> CreateVenueAsync(CreateVenueDto dto, string creatorUserId)
    {
        
        var guidUserId = Guid.Parse(creatorUserId);
        var creatorId = (int)(guidUserId.GetHashCode() & 0x7FFFFFFF); // Simple conversion for demo
        
        var venue = _mapper.Map<Venue>(dto);
        venue.CreatedById = creatorId;
        await _venueRepository.AddAsync(venue);
        return _mapper.Map<VenueDto>(venue);
    }
    
    
    public async Task<VenueDto> UpdateVenueAsync(int id, CreateVenueDto dto, string updaterUserId)
    {
        var existing = await _venueRepository.GetByIdAsync(id);
        if (existing == null)
            throw new InvalidOperationException($"Venue with ID {id} not found");
        
        var guidUserId = Guid.Parse(updaterUserId);
        var updaterId = (int)(guidUserId.GetHashCode() & 0x7FFFFFFF); // Simple conversion for demo

        
        _mapper.Map(dto, existing);
        existing.UpdatedId = updaterId; // <-- Set the updated user ID
        await _venueRepository.UpdateAsync(existing);
        return _mapper.Map<VenueDto>(existing);
    }
    
    public async Task<bool> DeleteVenueAsync(int id)
    {
        var venue = await _venueRepository.GetByIdAsync(id);
        if (venue == null)
            throw new InvalidOperationException($"Venue with ID {id} not found.");

        if (!venue.Active)
            throw new InvalidOperationException($"Venue with ID {id} is already deleted.");

        await _venueRepository.DeleteAsync(id);
        return true;;
    }
    

}