using AutoMapper;
using Fringe.Domain.Entities;
using Fringe.Domain.DTOs;

//This class is to map objects directly

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Venue mappings
        CreateMap<Venue, VenueDto>().ReverseMap();
        CreateMap<Venue, CreateVenueDto>().ReverseMap();
    }
}