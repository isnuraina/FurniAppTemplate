using FurniAppTemplate.Migrations;
using FurniAppTemplate.Models;
using Microsoft.EntityFrameworkCore;

namespace FurniAppTemplate.Data
{
    public class FurnitureDbContext:DbContext
    {
        public FurnitureDbContext(DbContextOptions options):base(options)
        {
            
        }
        public DbSet<MainChairImageUrl> MainChairImageUrls { get; set; }
        public DbSet<MainChairAndContent> MainChairAndContents { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Furniture> Furnitures { get; set; }
        public DbSet<Team> Teams { get; set; }

    }
}
