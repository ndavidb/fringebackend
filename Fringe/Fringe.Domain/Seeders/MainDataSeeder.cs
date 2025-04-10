using Fringe.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fringe.Domain.Extensions;

public class MainDataSeeder
{
    public static async Task SeedMainDataAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<FringeDbContext>();

        // Seed venue types if empty
        await SeedVenueTypesAsync(context);
        
        // Seed locations if empty
        await SeedLocationsAsync(context);
        
        // Seed venues if empty
        await SeedVenuesAsync(context);
        
        // Seed shows if empty
        await SeedShowsAsync(context);
    }
    
    private static async Task SeedVenueTypesAsync(FringeDbContext context)
    {
        if (!await context.VenueTypeLookUps.AnyAsync())
        {
            var venueTypes = new List<VenueTypeLookup>
            {
                new VenueTypeLookup { VenueType = "Theatre" },
                new VenueTypeLookup { VenueType = "Concert Hall" },
                new VenueTypeLookup { VenueType = "Open Air" },
                new VenueTypeLookup { VenueType = "Bar" },
                new VenueTypeLookup { VenueType = "Gallery" },
                new VenueTypeLookup { VenueType = "Studio" },
                new VenueTypeLookup { VenueType = "Church" },
                new VenueTypeLookup { VenueType = "Community Center" },
                new VenueTypeLookup { VenueType = "Nightclub" },
                new VenueTypeLookup { VenueType = "Cafe" }
            };

            await context.VenueTypeLookUps.AddRangeAsync(venueTypes);
            await context.SaveChangesAsync();
            Console.WriteLine("Venue types seeded successfully");
        }
    }
    
    private static async Task SeedLocationsAsync(FringeDbContext context)
    {
        if (!await context.Locations.AnyAsync())
        {
            // Use a consistent creator ID for seed data
            const int creatorId = 1; // Admin user
            
            var locations = new List<Location>
            {
                new Location
                {
                    LocationName = "Adelaide CBD Cultural District",
                    Address = "125 North Terrace",
                    Suburb = "Adelaide",
                    PostalCode = "5000",
                    State = "South Australia",
                    Country = "Australia",
                    Latitude = -34.9216,
                    Longitude = 138.5999,
                    ParkingAvailable = true,
                    Active = true,
                    CreatedById = creatorId,
                    CreatedAt = DateTime.UtcNow
                },
                new Location
                {
                    LocationName = "Rundle Mall Precinct",
                    Address = "22 Rundle Mall",
                    Suburb = "Adelaide",
                    PostalCode = "5000",
                    State = "South Australia",
                    Country = "Australia",
                    Latitude = -34.9223,
                    Longitude = 138.6048,
                    ParkingAvailable = false,
                    Active = true,
                    CreatedById = creatorId,
                    CreatedAt = DateTime.UtcNow
                },
                new Location
                {
                    LocationName = "Adelaide Riverbank",
                    Address = "Festival Drive",
                    Suburb = "Adelaide",
                    PostalCode = "5000",
                    State = "South Australia",
                    Country = "Australia",
                    Latitude = -34.9200,
                    Longitude = 138.5963,
                    ParkingAvailable = true,
                    Active = true,
                    CreatedById = creatorId,
                    CreatedAt = DateTime.UtcNow
                },
                new Location
                {
                    LocationName = "Glenelg Beach Area",
                    Address = "25 Jetty Road",
                    Suburb = "Glenelg",
                    PostalCode = "5045",
                    State = "South Australia",
                    Country = "Australia",
                    Latitude = -34.9812,
                    Longitude = 138.5145,
                    ParkingAvailable = true,
                    Active = true,
                    CreatedById = creatorId,
                    CreatedAt = DateTime.UtcNow
                },
                new Location
                {
                    LocationName = "North Adelaide Arts Hub",
                    Address = "78 O'Connell Street",
                    Suburb = "North Adelaide",
                    PostalCode = "5006",
                    State = "South Australia",
                    Country = "Australia",
                    Latitude = -34.9077,
                    Longitude = 138.5946,
                    ParkingAvailable = true,
                    Active = true,
                    CreatedById = creatorId,
                    CreatedAt = DateTime.UtcNow
                }
            };

            await context.Locations.AddRangeAsync(locations);
            await context.SaveChangesAsync();
            Console.WriteLine("Locations seeded successfully");
        }
    }
    
    private static async Task SeedVenuesAsync(FringeDbContext context)
    {
        if (!await context.Venues.AnyAsync())
        {
            // Ensure we have locations and venue types
            var locations = await context.Locations.ToListAsync();
            var venueTypes = await context.VenueTypeLookUps.ToListAsync();
            
            if (locations.Count == 0 || venueTypes.Count == 0)
            {
                Console.WriteLine("Cannot seed venues: missing locations or venue types");
                return;
            }
            
            const int creatorId = 1; // Admin user
            
            var venues = new List<Venue>
            {
                new Venue
                {
                    VenueName = "Adelaide Festival Centre",
                    LocationId = locations[0].LocationId,
                    TypeId = venueTypes.First(vt => vt.VenueType == "Theatre").TypeId,
                    MaxCapacity = 1000,
                    Description = "Premier performing arts venue with multiple spaces for theatre, music, and dance performances.",
                    ContactEmail = "bookings@adelaidefestivalcentre.com.au",
                    ContactPhone = "+61 8 8216 8600",
                    IsAccessible = true,
                    VenueUrl = "https://adelaidefestivalcentre.com.au",
                    Active = true,
                    CreatedById = creatorId,
                    CreatedAt = DateTime.UtcNow
                },
                new Venue
                {
                    VenueName = "The Garden of Unearthly Delights",
                    LocationId = locations[2].LocationId,
                    TypeId = venueTypes.First(vt => vt.VenueType == "Open Air").TypeId,
                    MaxCapacity = 3000,
                    Description = "Iconic outdoor hub with multiple performance spaces, food stalls, and bars during Fringe season.",
                    ContactEmail = "info@gardenofunearthlydelights.com.au",
                    ContactPhone = "+61 8 8210 5771",
                    IsAccessible = true,
                    VenueUrl = "https://gardenofunearthlydelights.com.au",
                    Active = true,
                    CreatedById = creatorId,
                    CreatedAt = DateTime.UtcNow
                },
                new Venue
                {
                    VenueName = "Holden Street Theatres",
                    LocationId = locations[4].LocationId,
                    TypeId = venueTypes.First(vt => vt.VenueType == "Theatre").TypeId,
                    MaxCapacity = 200,
                    Description = "Intimate theatre venue housed in a converted church with multiple performance spaces.",
                    ContactEmail = "admin@holdenstreettheatres.com",
                    ContactPhone = "+61 8 8223 1450",
                    IsAccessible = true,
                    VenueUrl = "https://holdenstreettheatres.com",
                    Active = true,
                    CreatedById = creatorId,
                    CreatedAt = DateTime.UtcNow
                },
                new Venue
                {
                    VenueName = "Gluttony",
                    LocationId = locations[2].LocationId,
                    TypeId = venueTypes.First(vt => vt.VenueType == "Open Air").TypeId,
                    MaxCapacity = 2500,
                    Description = "Popular Fringe hub with multiple performance tents and outdoor spaces in Rymill Park.",
                    ContactEmail = "info@gluttony.net.au",
                    ContactPhone = "+61 8 7226 1247",
                    IsAccessible = true,
                    VenueUrl = "https://gluttony.net.au",
                    Active = true,
                    CreatedById = creatorId,
                    CreatedAt = DateTime.UtcNow
                },
                new Venue
                {
                    VenueName = "The Howling Owl",
                    LocationId = locations[1].LocationId,
                    TypeId = venueTypes.First(vt => vt.VenueType == "Bar").TypeId,
                    MaxCapacity = 80,
                    Description = "Cozy gin bar that transforms into an intimate performance space during Fringe.",
                    ContactEmail = "hello@thehowlingowl.com.au",
                    ContactPhone = "+61 8 8227 1022",
                    IsAccessible = false,
                    VenueUrl = "https://thehowlingowl.com.au",
                    Active = true,
                    CreatedById = creatorId,
                    CreatedAt = DateTime.UtcNow
                },
                new Venue
                {
                    VenueName = "Nexus Arts",
                    LocationId = locations[0].LocationId,
                    TypeId = venueTypes.First(vt => vt.VenueType == "Gallery").TypeId,
                    MaxCapacity = 150,
                    Description = "Multicultural arts venue with gallery and performance space promoting diverse artistic voices.",
                    ContactEmail = "office@nexusarts.org.au",
                    ContactPhone = "+61 8 8212 4276",
                    IsAccessible = true,
                    VenueUrl = "https://nexusarts.org.au",
                    Active = true,
                    CreatedById = creatorId,
                    CreatedAt = DateTime.UtcNow
                }
            };

            await context.Venues.AddRangeAsync(venues);
            await context.SaveChangesAsync();
            Console.WriteLine("Venues seeded successfully");
        }
    }
    
    private static async Task SeedShowsAsync(FringeDbContext context)
    {
        if (!await context.Shows.AnyAsync())
        {
            // Ensure we have venues, show types, and age restrictions
            var venues = await context.Venues.ToListAsync();
            var showTypes = await context.ShowTypeLookups.ToListAsync();
            var ageRestrictions = await context.AgeRestrictionLookups.ToListAsync();
            var ticketTypes = await context.TicketTypes.ToListAsync();
            
            if (venues.Count == 0 || showTypes.Count == 0 || ageRestrictions.Count == 0 || ticketTypes.Count == 0)
            {
                Console.WriteLine("Cannot seed shows: missing venues, show types, age restrictions, or ticket types");
                return;
            }
            
            const int creatorId = 1; // Admin user
            
            // Create shows that span across the next few months
            var baseDate = DateTime.UtcNow;
            
            var shows = new List<Show>
            {
                new Show
                {
                    ShowName = "Laughter Unleashed",
                    VenueId = venues.First(v => v.VenueName == "The Howling Owl").VenueId,
                    ShowTypeId = showTypes.First(st => st.ShowType == "Comedy").TypeId,
                    Description = "A hilarious stand-up comedy showcase featuring five of the best comedians on the Adelaide circuit.",
                    AgeRestrictionId = ageRestrictions.First(ar => ar.Code == "MA15+").AgeRestrictionId,
                    WarningDescription = "Contains adult themes, coarse language, and references to alcohol.",
                    StartDate = baseDate.AddDays(14),
                    EndDate = baseDate.AddDays(21),
                    TicketTypeId = ticketTypes.First(tt => tt.TypeName == "Standard").TicketTypeId,
                    ImagesUrl = "https://example.com/images/laughter-unleashed",
                    VideosUrl = "https://example.com/videos/laughter-unleashed",
                    Active = true,
                    CreatedById = creatorId,
                    CreatedAt = DateTime.UtcNow
                },
                new Show
                {
                    ShowName = "The Night Sky",
                    VenueId = venues.First(v => v.VenueName == "Holden Street Theatres").VenueId,
                    ShowTypeId = showTypes.First(st => st.ShowType == "Drama").TypeId,
                    Description = "An intimate drama exploring themes of loss, connection, and healing through the story of two strangers who meet under unusual circumstances.",
                    AgeRestrictionId = ageRestrictions.First(ar => ar.Code == "M").AgeRestrictionId,
                    WarningDescription = "Contains mild themes and infrequent coarse language.",
                    StartDate = baseDate.AddDays(7),
                    EndDate = baseDate.AddDays(28),
                    TicketTypeId = ticketTypes.First(tt => tt.TypeName == "Standard").TicketTypeId,
                    ImagesUrl = "https://example.com/images/night-sky",
                    VideosUrl = "https://example.com/videos/night-sky",
                    Active = true,
                    CreatedById = creatorId,
                    CreatedAt = DateTime.UtcNow
                },
                new Show
                {
                    ShowName = "Adelaide Jazz Explosion",
                    VenueId = venues.First(v => v.VenueName == "Adelaide Festival Centre").VenueId,
                    ShowTypeId = showTypes.First(st => st.ShowType == "Music").TypeId,
                    Description = "A dynamic jazz concert featuring a fusion of traditional jazz with contemporary influences from around Australia.",
                    AgeRestrictionId = ageRestrictions.First(ar => ar.Code == "G").AgeRestrictionId,
                    WarningDescription = "",
                    StartDate = baseDate.AddDays(3),
                    EndDate = baseDate.AddDays(3), // One-night show
                    TicketTypeId = ticketTypes.First(tt => tt.TypeName == "Standard").TicketTypeId,
                    ImagesUrl = "https://example.com/images/jazz-fusion",
                    VideosUrl = "https://example.com/videos/jazz-fusion",
                    Active = true,
                    CreatedById = creatorId,
                    CreatedAt = DateTime.UtcNow
                },
                new Show
                {
                    ShowName = "Cirque du Fringe",
                    VenueId = venues.First(v => v.VenueName == "Gluttony").VenueId,
                    ShowTypeId = showTypes.First(st => st.ShowType == "Circus").TypeId,
                    Description = "A breathtaking circus spectacular with aerial acrobatics, contortion, juggling, and more performed by world-class artists.",
                    AgeRestrictionId = ageRestrictions.First(ar => ar.Code == "G").AgeRestrictionId,
                    WarningDescription = "",
                    StartDate = baseDate.AddDays(30),
                    EndDate = baseDate.AddDays(45),
                    TicketTypeId = ticketTypes.First(tt => tt.TypeName == "Children").TicketTypeId,
                    ImagesUrl = "https://example.com/images/cirque-du-fringe",
                    VideosUrl = "https://example.com/videos/cirque-du-fringe",
                    Active = true,
                    CreatedById = creatorId,
                    CreatedAt = DateTime.UtcNow
                },
                new Show
                {
                    ShowName = "Moving Pictures",
                    VenueId = venues.First(v => v.VenueName == "Nexus Arts").VenueId,
                    ShowTypeId = showTypes.First(st => st.ShowType == "Visual Arts").TypeId,
                    Description = "An innovative exhibition blending visual art with dance, creating a multi-sensory experience.",
                    AgeRestrictionId = ageRestrictions.First(ar => ar.Code == "PG").AgeRestrictionId,
                    WarningDescription = "Contains occasional mild themes.",
                    StartDate = baseDate.AddDays(10),
                    EndDate = baseDate.AddDays(40),
                    TicketTypeId = ticketTypes.First(tt => tt.TypeName == "Senior").TicketTypeId,
                    ImagesUrl = "https://example.com/images/moving-pictures",
                    VideosUrl = "https://example.com/videos/moving-pictures",
                    Active = true,
                    CreatedById = creatorId,
                    CreatedAt = DateTime.UtcNow
                },
                new Show
                {
                    ShowName = "Garden After Dark",
                    VenueId = venues.First(v => v.VenueName == "The Garden of Unearthly Delights").VenueId,
                    ShowTypeId = showTypes.First(st => st.ShowType == "Cabaret").TypeId,
                    Description = "A risqué late-night cabaret featuring burlesque, comedy, music, and more in the magical Garden setting.",
                    AgeRestrictionId = ageRestrictions.First(ar => ar.Code == "R18+").AgeRestrictionId,
                    WarningDescription = "Contains adult themes, nudity, sexual references, and coarse language.",
                    StartDate = baseDate.AddDays(5),
                    EndDate = baseDate.AddDays(35).AddHours(3), // Every Friday and Saturday for a month
                    TicketTypeId = ticketTypes.First(tt => tt.TypeName == "Standard").TicketTypeId,
                    ImagesUrl = "https://example.com/images/garden-after-dark",
                    VideosUrl = "https://example.com/videos/garden-after-dark",
                    Active = true,
                    CreatedById = creatorId,
                    CreatedAt = DateTime.UtcNow
                },
                new Show
                {
                    ShowName = "Shakespeare Reimagined",
                    VenueId = venues.First(v => v.VenueName == "Adelaide Festival Centre").VenueId,
                    ShowTypeId = showTypes.First(st => st.ShowType == "Theatre").TypeId,
                    Description = "A modern interpretation of Shakespeare's A Midsummer Night's Dream set in contemporary South Australia.",
                    AgeRestrictionId = ageRestrictions.First(ar => ar.Code == "M").AgeRestrictionId,
                    WarningDescription = "Contains mild themes and simulated violence.",
                    StartDate = baseDate.AddDays(60),
                    EndDate = baseDate.AddDays(75),
                    TicketTypeId = ticketTypes.First(tt => tt.TypeName == "Concession").TicketTypeId,
                    ImagesUrl = "https://example.com/images/shakespeare-reimagined",
                    VideosUrl = "https://example.com/videos/shakespeare-reimagined",
                    Active = true,
                    CreatedById = creatorId,
                    CreatedAt = DateTime.UtcNow
                },
                new Show
                {
                    ShowName = "Dance Dimensions",
                    VenueId = venues.First(v => v.VenueName == "Holden Street Theatres").VenueId,
                    ShowTypeId = showTypes.First(st => st.ShowType == "Dance").TypeId,
                    Description = "A showcase of contemporary dance exploring themes of space, time, and human connection.",
                    AgeRestrictionId = ageRestrictions.First(ar => ar.Code == "G").AgeRestrictionId,
                    WarningDescription = "",
                    StartDate = baseDate.AddDays(21),
                    EndDate = baseDate.AddDays(28),
                    TicketTypeId = ticketTypes.First(tt => tt.TypeName == "Standard").TicketTypeId,
                    ImagesUrl = "https://example.com/images/dance-dimensions",
                    VideosUrl = "https://example.com/videos/dance-dimensions",
                    Active = true,
                    CreatedById = creatorId,
                    CreatedAt = DateTime.UtcNow
                }
            };

            await context.Shows.AddRangeAsync(shows);
            await context.SaveChangesAsync();
            Console.WriteLine("Shows seeded successfully");
        }
    }
}