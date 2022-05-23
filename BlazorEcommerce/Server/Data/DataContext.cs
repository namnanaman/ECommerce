
namespace BlazorEcommerce.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                 new Product
                 {
                     Id = 1,
                     Title = "The Hitchhiker's Guide to the Galaxy",
                     Description = "One Thursday morning, Arthur Dent discovers that his house is to be immediately demolished to make way for a bypass. He tries delaying the bulldozers by lying down in front of them. Ford Prefect, a friend of Arthur's, convinces him to go to a pub with him. Over several pints of beer, Ford explains that he is an alien from the vicinity of Betelgeuse, and a journalist working on the Hitchhiker's Guide to the Galaxy, a universal guide book. Ford warns that the Earth is to be demolished later that day by a race called Vogons, to make way for a hyperspace bypass.",
                     ImageUrl = "https://picsum.photos/200",
                     Price = 9.99m
                 },
                new Product
                {
                    Id = 2,
                    Title = "Ready Player One",
                    Description = "Ready Player One is a 2018 American science fiction adventure film based on Ernest Cline's novel of the same name. Directed by Steven Spielberg, from a screenplay by Zak Penn and Cline, it stars Tye Sheridan, Olivia Cooke, Ben Mendelsohn, Lena Waithe, T.J. Miller, Simon Pegg, and Mark Rylance. Set in 2045, much of humanity uses the OASIS, a virtual reality simulation, to escape the real world. The orphaned Wade Watts finds clues to a contest that promises the ownership of the OASIS to the winner, and he and his allies try to complete it before an evil corporation can do so.	",
                    ImageUrl = "https://picsum.photos/200",
                    Price = 41.93m
                },
                new Product
                {
                    Id = 3,
                    Title = "Nineteen Eighty-Four",
                    Description = "Nineteen Eighty-Four (also stylised as 1984) is a dystopian social science fiction novel and cautionary tale written by English writer George Orwell. It was published on 8 June 1949 by Secker & Warburg as Orwell's ninth and final book completed in his lifetime. Thematically, it centres on the consequences of totalitarianism, mass surveillance and repressive regimentation of people and behaviours within society.[2][3] Orwell, a democratic socialist, modelled the totalitarian government in the novel after Stalinist Russia and Nazi Germany.[2][3][4] More broadly, the novel examines the role of truth and facts within politics and the ways in which they are manipulated.",
                    ImageUrl = "https://picsum.photos/200",
                    Price = 29.99m
                }
            );

        }

        public DbSet<Product> Products { get; set; }

    }
}
