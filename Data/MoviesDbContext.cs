using FinalAssignmentWebAPI.Models;
using Microsoft.CodeAnalysis.Completion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace FinalAssignmentWebAPI.Data
{
    public class MoviesDbContext : DbContext
    {
        public MoviesDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Franchise> Franchises { get; set; }
        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Movie>().HasData(
                new Movie()
                {
                    Id = 1,
                    MovieTitle = "Dune",
                    Genre = "Sci-fi",
                    ReleaseYear = 2017,
                    Director = "Denis Villeneuve",
                    Picture = "https://cdn.entries.clios.com/styles/clio_aotw_ems_image_details_retina/s3/entry_attachments/image/72/2297/22197/122278/noif6bM212YhBupvTTAoRBZPy7rnN7gLiFiwUeVaSBg.jpg",
                    Trailer = "https://www.youtube.com/watch?v=8g18jFHCLXk",
                    FranchiseId = 1
                },

                new Movie()
                {
                    Id = 2,
                    MovieTitle = "Blade Runner 2049",
                    Genre = "Sci-fi",
                    ReleaseYear = 2021,
                    Director = "Denis Villeneuve",
                    Picture = "https://alternativemovieposters.com/wp-content/uploads/2022/11/Matt-Ferguson_BladeRunner.jpg",
                    Trailer = "https://www.youtube.com/watch?v=gCcx85zbxz4",
                    FranchiseId = 2

                },

                new Movie()
                {
                    Id = 3,
                    MovieTitle = "Dune: Part Two",
                    Genre = "Sci-fi",
                    ReleaseYear = 2023,
                    Director = "Denis Villeneuve",
                    Picture = "https://mlpnk72yciwc.i.optimole.com/cqhiHLc.IIZS~2ef73/w:auto/h:auto/q:75/https://bleedingcool.com/wp-content/uploads/2023/05/DUNE2_VERT_Tsr_2764x4096_DOM_REV.jpg",
                    Trailer = "https://www.youtube.com/watch?v=Way9Dexny3w",
                    FranchiseId = 1

                },

                new Movie()
                {
                    Id = 4,
                    MovieTitle = "Blade Runner",
                    Genre = "Sci-fi",
                    ReleaseYear = 1982,
                    Director = "Ridley Scott",
                    Picture = "https://i.redd.it/lg5vx7dnwkd51.jpg",
                    Trailer = "https://www.youtube.com/watch?v=eogpIG53Cis",
                    FranchiseId = 2
                }
                );



            modelBuilder.Entity<Franchise>().HasData(
                new Franchise()
                {
                    Id = 1,
                    Name = "Dune",
                    Description = "Contains the movies of the Dune franchise"
                },

                new Franchise()
                {
                    Id = 2,
                    Name = "Blade Runner",
                    Description = "Contains the movies of the Blade Runner franchise"
                }
                );



            modelBuilder.Entity<Character>().HasData(
                 new Character()
                 {
                     Id = 1,
                     Name = "Rick Deckard",
                     Alias = "Blade Runner",
                     Gender = "Male",
                     Picture = "https://static.wikia.nocookie.net/bladerunner/images/e/ed/Deck.jpg/revision/latest?cb=20220616043815"
                 },

                 new Character()
                 {
                     Id = 2,
                     Name = "Rachael",
                     Alias = "N7FAA52318",
                     Gender = "Female",
                     Picture = "https://static.wikia.nocookie.net/bladerunner/images/c/ca/Rachael_Voight-Kampff_Test.jpg/revision/latest/scale-to-width-down/1000?cb=20220613123504"
                 },

                 new Character()
                 {
                     Id = 3,
                     Name = "Roy Batty",
                     Alias = "N6MAA10816",
                     Gender = "Male",
                     Picture = "https://static.wikia.nocookie.net/bladerunner/images/c/c6/Roy_Batty_new.jpg/revision/latest?cb=20190301233437"
                 }
                 );



        }
    }
}
