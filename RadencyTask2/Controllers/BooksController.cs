using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RadencyTask2.Models;
using RadencyTask2.Services;

namespace RadencyTask2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAllBooks(string order = null)
        {
            var books = await _bookService.GetAllBooks(order);
            return Ok(books);
        }

        [HttpGet("recommended")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetRecommendedBooks(string genre = null)
        {
            var books = await _bookService.GetRecommendedBooks(genre);
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDetailsDto>> GetBookDetails(int id)
        {
            var bookDetails = await _bookService.GetBookDetails(id);
            return Ok(bookDetails);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id, [FromQuery] string secret)
        {
            if (secret != "qwerty")
            {
                return Forbid();
            }

            await _bookService.DeleteBook(id, secret);
            return NoContent();
        }

        [HttpPost("save")]
        public async Task<ActionResult<int>> SaveBook([FromBody]SaveBookRequestDto saveBookDto)
        {
            var bookId = await _bookService.SaveBook(saveBookDto);
            return Ok(bookId);
        }

        [HttpPut("{id}/review")]
        public async Task<ActionResult<int>> AddReview(int id, [FromBody] SaveReviewRequestDto addReviewDto)
        {
            Debug.WriteLine(addReviewDto.Reviewer);
            Debug.WriteLine(addReviewDto.Reviewer);
            Debug.WriteLine(addReviewDto.Reviewer);
            Debug.WriteLine(addReviewDto.Reviewer);
            var reviewId = await _bookService.SaveReview(id, addReviewDto);
            return Ok(reviewId);
        }

        [HttpPut("{id}/rate")]
        public async Task<IActionResult> RateBook(int id, [FromBody] RateBookRequestDto rateBookDto)
        {
            Debug.WriteLine(rateBookDto.Score);
            Debug.WriteLine(rateBookDto.Score);
            Debug.WriteLine(rateBookDto.Score);
            Debug.WriteLine(rateBookDto.Score);
            Debug.WriteLine(rateBookDto.Score);
            await _bookService.RateBook(id, rateBookDto);
            return NoContent();
        }
    }
}
