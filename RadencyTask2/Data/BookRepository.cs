using Microsoft.EntityFrameworkCore;
using RadencyTask2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RadencyTask2.Data
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooks(string orderBy);
        Task<IEnumerable<Book>> GetRecommendedBooks(string genre);
        Task<Book> GetBookById(int id);
        Task<int> AddBook(SaveBookRequestDto book);
        Task<int> UpdateBook(SaveBookRequestDto book);
        Task<bool> DeleteBook(int id);
        Task<int> AddReview(Review review);
        Task<bool> Save();
        Task<Rating> GetRatingByBookId(int bookId);
    }
    public class BookRepository : IBookRepository
    {
        private readonly IDbContext _dbContext;

        public BookRepository(IDbContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooks(string orderBy)
        {
            IQueryable<Book> books = _dbContext.Books
                                        .Include(b => b.Reviews)
                                        .Include(b => b.Ratings);

            switch (orderBy.ToLower())
            {
                case "title":
                    books = books.OrderBy(b => b.Title);
                    break;
                case "author":
                    books = books.OrderBy(b => b.Author);
                    break;
                case "rating":
                    books = books.OrderByDescending(b => b.Ratings.Average(r => r.Score));
                    break;
                default:
                    break;
            }

            return await books.ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetRecommendedBooks(string genre)
        {
            return await _dbContext.Books
                .Include(b => b.Reviews)
                .Include(b => b.Ratings)
                .Where(b => b.Genre == genre.ToLower())
                .OrderByDescending(b => b.Ratings.Average(r => r.Score))
                .ThenByDescending(b => b.Reviews.Count)
                .Take(10)
                .ToListAsync();
        }

        public async Task<Book> GetBookById(int id)
        {
            return await _dbContext.Books
                        .Include(b => b.Reviews)
                        .Include(b => b.Ratings)
                        .SingleOrDefaultAsync(b => b.Id == id);
        }

        public async Task<int> AddBook(SaveBookRequestDto bookDto)
        {
            Book newBook = new Book
            {
                Title = bookDto.Title,
                Author = bookDto.Author,
                Cover = bookDto.Cover,
                Content = bookDto.Content,
                Genre = bookDto.Genre,
                Reviews = new List<Review>(),
                Ratings = new List<Rating>()

            };
            _dbContext.Books.Add(newBook);
            await Save();
            return newBook.Id;
        }

        public async Task<int> AddReview(Review review)
        {
            _dbContext.Reviews.Add(review);
            await Save();
            return review.Id;
        }

        public async Task<bool> DeleteBook(int id)
        {
            var book = await _dbContext.Books.FindAsync(id);
            if (book == null)
            {
                return false;
            }

            _dbContext.Books.Remove(book);
            await Save();
            return true;
        }

        

        public async Task<Rating> GetRatingByBookId(int bookId)
        {
            return await _dbContext.Ratings.SingleOrDefaultAsync(r => r.BookId == bookId);
        }

        

        public async Task<bool> Save()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Log the exception
                return false;
            }
        }

        public async Task<int> UpdateBook(SaveBookRequestDto bookDto)
        {
            var existingBook = await _dbContext.Books.FindAsync(bookDto.Id);
            if (existingBook == null)
            {
                return 0;
            }

            existingBook.Title = bookDto.Title;
            existingBook.Author = bookDto.Author;
            existingBook.Cover = bookDto.Cover;
            existingBook.Content = bookDto.Content;
            existingBook.Genre = bookDto.Genre;
            await Save();
            return existingBook.Id;
        }
    }
}
