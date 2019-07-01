using Microsoft.EntityFrameworkCore;

namespace Phx.People.Data
{
    public class PeopleContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("Phx.People.InMem");
            }

            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasData(
                new Person
                {
                    Id = 1,
                    Name = "Denis",
                    Age = 30
                },
                new Person
                {
                    Id = 2,
                    Name = "Eric",
                    Age = 26
                });
        }
    }
}
