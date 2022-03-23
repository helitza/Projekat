using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Models
{
    public class KompanijaContext : DbContext
    {
        public DbSet<Tribina> Tribine { get; set; }
        public DbSet<Odrzavanje> Odrzavanja { get; set; }
     
        public DbSet<Akreditacija> Akreditacije { get; set; }

        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Mesto> Mesta{get; set;}
        public DbSet<Sediste> Sedista{get;set;}
        public DbSet<Kompanija> Kompanije{get; set;}
        public DbSet<KompanijaTribina> KopmanijeTribine{get; set;}
        public KompanijaContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
        }
        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<KompanijaContext> {
        public KompanijaContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<KompanijaContext>();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        builder.UseSqlServer(connectionString);

        return new KompanijaContext(builder.Options);
        }
}

        public static implicit operator KompanijaContext(Kompanija v)
        {
            throw new System.NotImplementedException();
        }
    }
}