using Fringe.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fringe.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VenuesController : ControllerBase
{
    private readonly IVenueService _venueService;

    public VenuesController(IVenueService venueService)
    {
        _venueService = venueService;
    }

    /**Retrieves a list of all venues along with their related location and type information.
    <returns>List of VenueDto</returns> **/
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllVenues()
    {
        var venues = await _venueService.GetAllVenuesAsync();
        return Ok(venues);
    }
}