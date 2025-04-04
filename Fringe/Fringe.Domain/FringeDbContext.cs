using Fringe.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fringe.Domain
{
    public class FringeDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public FringeDbContext(DbContextOptions<FringeDbContext> options) : base(options)
        {
        }

        public DbSet<Show> Shows { get; set; }
        public DbSet<ShowTypeLookup> ShowTypeLookups { get; set; }
        public DbSet<AgeRestrictionLookup> AgeRestrictionLookups { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        // Venues DbSet
        public DbSet<Venue> Venues { get; set; }

        // Location DbSet
        public DbSet<Location> Locations { get; set; }
        
        // VenueTypes DbSet
        public DbSet<VenueTypeLookup> VenueTypeLookUps { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Role entity
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RoleId);
                entity.Property(e => e.RoleId).HasColumnName("roleid");
                entity.Property(e => e.RoleName).HasColumnName("rolename").HasMaxLength(100).IsRequired();
                entity.Property(e => e.CanCreate).HasColumnName("cancreate").HasDefaultValue(false);
                entity.Property(e => e.CanRead).HasColumnName("canread").HasDefaultValue(false);
                entity.Property(e => e.CanEdit).HasColumnName("canedit").HasDefaultValue(false);
                entity.Property(e => e.CanDelete).HasColumnName("candelete").HasDefaultValue(false);
                entity.HasIndex(e => e.RoleName).IsUnique();
            });

            // Your other entity configurations...
            
            //Configures Location entity.
            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.LocationId);
                entity.Property(e => e.LocationId).HasColumnName("locationid");
                entity.Property(e => e.LocationName).HasColumnName("locationname").HasMaxLength(100).IsRequired();
                entity.Property(e => e.Address).HasColumnName("address");
                entity.Property(e => e.Suburb).HasColumnName("suburb");
                entity.Property(e => e.PostalCode).HasColumnName("postalcode");
                entity.Property(e => e.State).HasColumnName("state");
                entity.Property(e => e.Country).HasColumnName("country");
                entity.Property(e => e.Latitude).HasColumnName("latitude");
                entity.Property(e => e.Longitude).HasColumnName("longitude");
                entity.Property(e => e.ParkingAvailable).HasColumnName("parkingavailable").HasDefaultValue(false);
                entity.Property(e => e.Active).HasColumnName("active").HasDefaultValue(true);
                entity.Property(e => e.CreatedById).HasColumnName("createdbyid");
                entity.Property(e => e.CreatedAt).HasColumnName("createdat");
                entity.Property(e => e.UpdatedId).HasColumnName("updatedid");
                entity.Property(e => e.UpdatedAt).HasColumnName("updatedat");
            });
            
            // Configure VenueTypeLookUp entity
            modelBuilder.Entity<VenueTypeLookup>(entity =>
            {
                entity.ToTable("VenueTypeLookup"); // explicitly set the table name
                entity.HasKey(e => e.TypeId);
                entity.Property(e => e.TypeId).HasColumnName("typeid");
                entity.Property(e => e.VenueType).HasColumnName("venuetype").HasMaxLength(100).IsRequired();
            });
            
            // Configures tVenue entity.
            modelBuilder.Entity<Venue>(entity =>
            {
                entity.HasKey(e => e.VenueId);
                entity.Property(e => e.VenueId).HasColumnName("venueid");
                entity.Property(e => e.VenueName).HasColumnName("venuename").HasMaxLength(150).IsRequired();
                entity.Property(e => e.LocationId).HasColumnName("locationid");
                entity.Property(e => e.TypeId).HasColumnName("typeid");
                entity.Property(e => e.MaxCapacity).HasColumnName("maxcapacity");
                entity.Property(e => e.SeatingPlanId).HasColumnName("seatingplanid");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.ContactEmail).HasColumnName("contactemail");
                entity.Property(e => e.ContactPhone).HasColumnName("contactphone");
                entity.Property(e => e.IsAccessible).HasColumnName("isaccessible").HasDefaultValue(false);
                entity.Property(e => e.VenueUrl).HasColumnName("venueurl");
                entity.Property(e => e.Active).HasColumnName("active").HasDefaultValue(true);
                entity.Property(e => e.CreatedById).HasColumnName("createdbyid");
                entity.Property(e => e.CreatedAt).HasColumnName("createdat");
                entity.Property(e => e.UpdatedId).HasColumnName("updatedid");
                entity.Property(e => e.UpdatedAt).HasColumnName("updatedat");

               
                entity.HasOne(e => e.Location)
                    .WithMany(l => l.Venues)
                    .HasForeignKey(e => e.LocationId);

                entity.HasOne(e => e.VenueTypeLookUp)
                    .WithMany()
                    .HasForeignKey(e => e.TypeId);
            });

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FringeDbContext).Assembly);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
