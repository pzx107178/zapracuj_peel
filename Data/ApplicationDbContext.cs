using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using mapkowanie.Models;

namespace mapkowanie.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<mapkowanie.Models.ExcerciceType>? ExcerciceType { get; set; }
        public DbSet<mapkowanie.Models.Excercice>? Excercice { get; set; }
        public DbSet<mapkowanie.Models.Session>? Session { get; set; }
        public DbSet<mapkowanie.Models.Statistics>? Statistics { get; set; }
        public DbSet<mapkowanie.Models.Branza>? Branza { get; set; }
        public DbSet<mapkowanie.Models.Konto>? Konto { get; set; }
        public DbSet<mapkowanie.Models.Oferta>? Oferta { get; set; }
        public DbSet<mapkowanie.Models.Rodzaje>? Rodzaje { get; set; }
    }
}