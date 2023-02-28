using AutoMapper;
using RadencyTask2.Data;
using RadencyTask2.Models;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace RadencyTask2.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooks(string orderBy);
        Task<IEnumerable<BookDto>> GetRecommendedBooks(string genre);
        Task<BookDetailsDto> GetBookDetails(int id);
        Task<bool> DeleteBook(int id, string secretKey);
        Task<int> SaveBook(SaveBookRequestDto bookDto);
        Task<int> SaveReview(int bookId, SaveReviewRequestDto reviewDto);
        Task<bool> RateBook(int bookId, RateBookRequestDto ratingDto);
    }

    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private IRatingRepository _ratingRepository;

        public BookService(IBookRepository bookRepository, IRatingRepository ratingRepository)
        {
            _bookRepository = bookRepository;
            _ratingRepository = ratingRepository;
        }
        public async Task<IEnumerable<BookDto>> GetAllBooks(string orderBy)
        {
            var books = await _bookRepository.GetAllBooks(orderBy);
            foreach (var book in books)
            {
                Debug.WriteLine(book.Title);
            }
            return books.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                Rating = Convert.ToDecimal(b.Ratings?.Any() == true ? b.Ratings.Average(r => r.Score) : 0),
                ReviewsNumber = b.Reviews?.Count ?? 0
            });
        }

        public async Task<IEnumerable<BookDto>> GetRecommendedBooks(string genre)
        {
            var recommendedBooks = await _bookRepository.GetRecommendedBooks(genre);
            return recommendedBooks.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                Rating = Convert.ToDecimal(b.Ratings?.Any() == true ? b.Ratings.Average(r => r.Score) : 0),
                ReviewsNumber = b.Reviews?.Count ?? 0
            });
        }


        public async Task<BookDetailsDto> GetBookDetails(int id)
        {
            var book = await _bookRepository.GetBookById(id);
            if (book == null)
            {
                return null;
            }

            var bookDetailsDto = new BookDetailsDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Cover = book.Cover,
                Content = book.Content,
                Rating = Convert.ToDecimal(book.Ratings.Any() ? book.Ratings.Average(r => r.Score) : 0),
                Reviews = book.Reviews.Select(r => new ReviewDto
                {
                    Id = r.Id,
                    Message = r.Message,
                    Reviewer = r.Reviewer
                }).ToList()
            };

            return bookDetailsDto;
        }

        public async Task<bool> DeleteBook(int id, string secretKey)
        {
            var book = await _bookRepository.GetBookById(id);
            if (book == null)
            {
                return false;
            }
            return await _bookRepository.DeleteBook(id);
        }
        
        public async Task<bool> RateBook(int bookId, RateBookRequestDto ratingDto)
        {
            var book = await _bookRepository.GetBookById(bookId);
            if (book == null)
            {
                return false;
            }

            if (ratingDto.Score < 1 || ratingDto.Score > 5)
            {
                return false;
            }

            var rating = new Rating
            {
                BookId = bookId,
                Score = ratingDto.Score
            };

            _ratingRepository.Add(rating);

            return _ratingRepository.Save();
        }

        public async Task<int> SaveBook(SaveBookRequestDto bookDto)
        {
            
            if (bookDto.Id == null || bookDto.Id == 0)
            {
                return await _bookRepository.AddBook(bookDto);
            }
            else
            {
                return await _bookRepository.UpdateBook(bookDto);
            }
        }

        public async Task<int> SaveReview(int bookId, SaveReviewRequestDto reviewDto)
        {
            var review = new Review
            {
                BookId = bookId,
                Message = reviewDto.Message,
                Reviewer = reviewDto.Reviewer
            };

            return await _bookRepository.AddReview(review);
        }

        /*
        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            var bookDtos = _mapper.Map<IEnumerable<BookDto>>(books);
            return bookDtos;
        }

        public async Task<BookDto> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            var bookDto = _mapper.Map<BookDto>(book);
            return bookDto;
        }

        public async Task<BookDto> CreateBookAsync(CreateBookDto createBookDto)
        {
            var book = _mapper.Map<Book>(createBookDto);
            await _bookRepository.AddAsync(book);
            await _bookRepository.SaveChangesAsync();
            var bookDto = _mapper.Map<BookDto>(book);
            return bookDto;
        }

        public async Task UpdateBookAsync(int id, UpdateBookDto updateBookDto)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            _mapper.Map(updateBookDto, book);
            await _bookRepository.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            await _bookRepository.RemoveAsync(book);
            await _bookRepository.SaveChangesAsync();
        }
        */
    }
}
