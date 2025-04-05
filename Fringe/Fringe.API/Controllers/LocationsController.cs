﻿using System.Security.Claims;
using Fringe.Domain.DTOs.LocationDTOs;
using Fringe.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fringe.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LocationsController : ControllerBase
{
    private readonly ILocationService _locationService;
    private readonly ILogger<ShowsController> _logger;

    public LocationsController(ILocationService locationService,  ILogger<ShowsController> logger)
    {
        _locationService = locationService;
        _logger = logger;
    }
    
    // Retrieve all locations
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllLocations() =>
        Ok(await _locationService.GetAllLocationsAsync());

    // GET - get location by id
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetLocationById(int id) =>
        Ok(await _locationService.GetLocationByIdAsync(id));

    //POST - save location
    [HttpPost]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateLocation([FromBody] CreateLocationDto createLocationDto)
    {
        try
        {
            var creatorUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var createdLocation = await _locationService.CreateLocationAsync(createLocationDto, creatorUserId);
            return CreatedAtAction(nameof(GetLocationById), new { id = createdLocation.LocationId }, createdLocation);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating location");
            return BadRequest(ex.Message);
        }
    }

    //PUT - update location by id
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> UpdateShow(int id, [FromBody] CreateLocationDto dto)
    {
        try
        {
            var updaterUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var updatedLocation = await _locationService.UpdateLocationAsync(id, dto, updaterUserId);
            return Ok(updatedLocation);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("not found"))
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating location");
            return BadRequest(ex.Message);
        }
    }
    
    

    //DELETE - delete location by id
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Delete(int id)
    {
        
        try
        {
            await _locationService.DeleteLocationAsync(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Error deleting location");
            return NotFound(ex.Message);
        }
    }
    
}