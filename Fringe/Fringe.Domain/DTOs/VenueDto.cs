namespace Fringe.Domain.DTOs;

// DTO to get venue data to the API
public class VenueDto
{
    public int VenueId { get; set; }
    public string VenueName { get; set; }
    public string VenueType { get; set; }
    public int MaxCapacity { get; set; }
    public string ContactEmail { get; set; }
    public string ContactPhone { get; set; }
    public bool IsAccessible { get; set; }
    public string VenueUrl { get; set; }
    public string LocationName { get; set; }
}