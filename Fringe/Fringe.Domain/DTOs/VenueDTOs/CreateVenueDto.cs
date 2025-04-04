namespace Fringe.Domain.DTOs;

public class CreateVenueDto
{
    public string VenueName { get; set; }
    public int TypeId { get; set; }
    public int MaxCapacity { get; set; }
    public string Description { get; set; }
    public string ContactEmail { get; set; }
    public string ContactPhone { get; set; }
    public bool IsAccessible { get; set; }
    public string VenueUrl { get; set; }
    public int LocationId { get; set; }
}