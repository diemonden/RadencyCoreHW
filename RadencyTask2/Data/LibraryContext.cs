using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RadencyTask2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RadencyTask2.Data
{
    public interface IDbContext
    {
        DbSet<Book> Books { get; set; }
        DbSet<Review> Reviews { get; set; }
        DbSet<Rating> Ratings { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        EntityEntry Entry(object entity);
    }

    public class LibraryContext : DbContext, IDbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
            
            //SeedBooks(options);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .EnableSensitiveDataLogging();
        }
        // DbSets for your entities
        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        public override EntityEntry Entry(object entity)
        {
            return base.Entry(entity);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasMany(b => b.Reviews).WithOne(r => r.Book);
            modelBuilder.Entity<Book>().HasMany(b => b.Ratings).WithOne(r => r.Book);
        }


        public void SeedBooks()
        {
            var books = new List<Book>
            {
                new Book
                {
                    Title = "The Great Gatsby",
                    Cover = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f7/The_Great_Gatsby_Cover_1925_Retouched.jpg/220px-The_Great_Gatsby_Cover_1925_Retouched.jpg",
                    Content = "In 1922, Nick Carraway, a Yale graduate and veteran of the Great War from the Midwest—who serves as the novel's narrator—takes a job in New York as a bond salesman. He rents a small house on Long Island, in the (fictional) village of West Egg, next door to the lavish mansion of Jay Gatsby, a mysterious multi-millionaire who holds extravagant parties but does not participate in them.",
                    Author = "F. Scott Fitzgerald",
                    Genre = "Classic",
                    Ratings = new List<Rating>()
                    {
                        new Rating()
                        {
                            Score = 5
                        },
                        new Rating()
                        {
                            Score = 4
                        }
                    },
                    Reviews = new List<Review>
                    {
                        new Review()
                        {
                            Reviewer = "John Doe",
                            Message = "This book was a classic for a reason. It was beautifully written and the characters were well-developed. Definitely a must-read!",
                        },
                        new Review()
                        {
                            Reviewer = "Jane Smith",
                            Message = "I didn't enjoy this book as much as I thought I would. The characters seemed superficial and the plot was slow-moving.",
                        }
                    }
                },
                new Book
                {
                    Title = "To Kill a Mockingbird",
                    Cover = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7a/To_Kill_a_Mockingbird_%28first_edition_cover%29.jpg/220px-To_Kill_a_Mockingbird_%28first_edition_cover%29.jpg",
                    Content = "The story is set in the Great Depression era of the 1930s in the fictional 'tired old town' of Maycomb, Alabama, the seat of Maycomb County. It focuses on six-year-old Jean Louise Finch (nicknamed Scout), who lives with her older brother Jeremy (nicknamed Jem) and their widowed father, Atticus, a middle-aged lawyer. Jem and Scout befriend a boy named Dill who visits Maycomb to stay with his aunt each summer. The three children are terrified of, and fascinated by, their neighbor, the reclusive Arthur 'Boo' Radley.",
                    Author = "Harper Lee",
                    Genre = "Classic",
                    Reviews = new List<Review>()
                    {
                    new Review()
                    {
                        Reviewer = "Mark Johnson",
                        Message = "This book was amazing! The writing was beautiful and the characters were so well-developed.",
                    },
                    new Review()
                    {
                        Reviewer = "Emily Chen",
                        Message = "I didn't enjoy this book as much as I thought I would. The plot seemed to drag on and the characters were unrelatable.",
                    }
                },
                Ratings = new List<Rating>()
                {
                    new Rating()
                    {
                        Score = 5
                    },
                    new Rating()
                    {
                        Score = 3
                    }
                }
                },
                new Book
                {
                    Title = "1984",
                    Cover = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c3/1984first.jpg/220px-1984first.jpg",
                    Content = "The story takes place in an imagined future, the year 1984, when much of the world has fallen victim to perpetual war, omnipresent government surveillance, historical negationism, and propaganda. Great Britain, known as Airstrip One, has become a province of a totalitarian superstate named Oceania that is ruled by the Party who employ the Thought Police to persecute individuality and independent thinking.",
                    Author = "George Orwell",
                    Genre = "Science Fiction",
                    Ratings = new List<Rating>
                    {
                        new Rating { Score = 4 },
                        new Rating { Score = 5 },
                        new Rating { Score = 3 }
                    },
                    Reviews = new List<Review>
                    {
                        new Review { Message = "Great book!", Reviewer = "John" },
                        new Review { Message = "Enjoyed it", Reviewer = "Jane" }
                    }
                },
                new Book
                {
                    Title = "Pride and Prejudice",
                    Cover = "https://i.imgur.com/3BkJv4z.jpg",
                    Content = "The story of the Bennet family and their five unmarried daughters. The novel explores the themes of love, reputation, and class.",
                    Author = "Jane Austen",
                    Genre = "Romance",
                    Ratings = new List<Rating>
                    {
                        new Rating { Score = 4 },
                        new Rating { Score = 5 },
                        new Rating { Score = 3 }
                    },
                    Reviews = new List<Review>
                    {
                        new Review { Message = "Great book!", Reviewer = "John" },
                        new Review { Message = "Enjoyed it", Reviewer = "Jane" }
                    }
                },

                new Book
                {
                    Title = "Jane Eyre",
                    Cover = "https://i.imgur.com/YKsJl7E.jpg",
                    Content = "The story of Jane Eyre, an orphan who becomes a governess and falls in love with her employer, Mr. Rochester. But dark secrets from his past threaten their happiness.",
                    Author = "Charlotte Bronte",
                    Genre = "Romance",
                    Ratings = new List<Rating>
                    {
                        new Rating { Score = 4 },
                        new Rating { Score = 5 },
                        new Rating { Score = 3 }
                    },
                    Reviews = new List<Review>
                    {
                        new Review { Message = "Great book!", Reviewer = "John" },
                        new Review { Message = "Enjoyed it", Reviewer = "Jane" }
                    }
                },
                new Book
                {
                    Title = "The Picture of Dorian Gray",
                    Cover = "https://i.imgur.com/PoTzvMJ.jpg",
                    Content = "The story of a young man, Dorian Gray, who becomes obsessed with his own beauty and youth, and sells his soul to preserve it.",
                    Author = "Oscar Wilde",
                    Genre = "Classics",
                    Ratings = new List<Rating>
                    {
                        new Rating { Score = 4 },
                        new Rating { Score = 5 },
                        new Rating { Score = 3 }
                    },
                    Reviews = new List<Review>
                    {
                        new Review { Message = "Great book!", Reviewer = "John" },
                        new Review { Message = "Enjoyed it", Reviewer = "Jane" }
                    }
                },
                new Book
                {
                    Title = "One Hundred Years of Solitude",
                    Cover = "https://i.imgur.com/lFkVl6C.jpg",
                    Content = "The story of the Buendía family, whose patriarch, José Arcadio Buendía, founds the town of Macondo, and the many generations of Buendías who inhabit it.",
                    Author = "Gabriel García Márquez",
                    Genre = "Magical Realism",
                    Ratings = new List<Rating>
                    {
                        new Rating { Score = 4 },
                        new Rating { Score = 5 },
                        new Rating { Score = 3 }
                    },
                    Reviews = new List<Review>
                    {
                        new Review { Message = "Great book!", Reviewer = "John" },
                        new Review { Message = "Enjoyed it", Reviewer = "Jane" }
                    }
                },
                new Book
                {
                    Title = "The Hitchhiker's Guide to the Galaxy",
                    Cover = "https://i.imgur.com/xSKV7LW.jpg",
                    Content = "The story of Arthur Dent, a man who is whisked away from Earth seconds before it is destroyed, and his adventures in space with his alien friend Ford Prefect.",
                    Author = "Douglas Adams",
                    Genre = "Science Fiction",
                    Ratings = new List<Rating>
                    {
                        new Rating { Score = 4 },
                        new Rating { Score = 5 },
                        new Rating { Score = 3 }
                    },
                    Reviews = new List<Review>
                    {
                        new Review { Message = "Great book!", Reviewer = "John" },
                        new Review { Message = "Enjoyed it", Reviewer = "Jane" }
                    }
                }
            };
            
            Books.AddRange(books);
            SaveChanges();
        }
    }
}
