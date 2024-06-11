using apbd_kol2.Models;
using Microsoft.EntityFrameworkCore;

namespace apbd_kol2.Data;

public class DatabaseContext : DbContext
{
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<backpack> Backpacks { get; set; }
    public DbSet<character> Characters { get; set; }
    public DbSet<characterTitle> CharacterTitles { get; set; }
    public DbSet<item> Items { get; set; }
    public DbSet<title> Titles { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<backpack>().HasData(new List<backpack>
        {
            new backpack
            {
                CharacterId = 1,
                ItemId = 1,
                Amount = 10
            },
            new backpack
            {
                CharacterId = 1,
                ItemId = 2,
                Amount = 5
            },
            new backpack
            {
                CharacterId = 2,
                ItemId = 3,
                Amount = 1
            },
        });

        modelBuilder.Entity<character>().HasData(new List<character>
        {
            new character
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                CurrentWeight = 10,
                MaxWeight = 100
            },
            new character
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Doe",
                CurrentWeight = 5,
                MaxWeight = 50
            },
            new character
            {
                Id = 3,
                FirstName = "Alice",
                LastName = "Smith",
                CurrentWeight = 1,
                MaxWeight = 10
            },
        });

        modelBuilder.Entity<characterTitle>().HasData(new List<characterTitle>
        {
            new characterTitle
            {
                CharacterId = 1,
                TitleId = 1,
                AcquiredAt = DateTime.Now
            },
            new characterTitle
            {
                CharacterId = 2,
                TitleId = 2,
                AcquiredAt = DateTime.Now
            },
            new characterTitle
            {
                CharacterId = 3,
                TitleId = 3,
                AcquiredAt = DateTime.Now
            },
        });
        
        
        modelBuilder.Entity<item>().HasData(new List<item>
        {
            new item
            {
                Id = 1,
                Name = "Sword",
                Weight = 5
            },
            new item
            {
                Id = 2,
                Name = "Shield",
                Weight = 10
            },
            new item
            {
                Id = 3,
                Name = "Potion",
                Weight = 1
            },
        });
        
        modelBuilder.Entity<title>().HasData(new List<title>
        {
            new title
            {
                Id = 1,
                Name = "Knight"
            },
            new title
            {
                Id = 2,
                Name = "Mage"
            },
            new title
            {
                Id = 3,
                Name = "Rogue"
            },
        });
        
    }
    
    
    
}