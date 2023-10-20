using Microsoft.EntityFrameworkCore;

namespace ApiEquipo.Entities
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Equipo>()
                .HasOne(e => e.Jefe)  
                .WithMany()           
                .HasForeignKey(e => e.JefeId)
                .OnDelete(DeleteBehavior.SetNull);  
        }

        public DbSet<Equipo> Equipos { get; set; }
        }

    }

