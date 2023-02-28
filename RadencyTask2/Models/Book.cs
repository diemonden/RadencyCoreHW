using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RadencyTask2.Models
{
    //1 rq
    public class GetBooksRequestDto
    {
        public string OrderBy { get; set; }
    }

    //1,2 rsp
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Rating { get; set; }
        public int ReviewsNumber { get; set; }
    }
    //2 rq
    public class GetRecommendedBooksRequestDto
    {
        public string Genre { get; set; }
    }
    //3 rsp
    public class BookDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Cover { get; set; }
        public string Content { get; set; }
        public decimal Rating { get; set; }
        public List<ReviewDto> Reviews { get; set; }
    }
    //3 rsp
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Reviewer { get; set; }
    }
    //5 rq
    public class SaveBookRequestDto
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Cover { get; set; }
        public string Content { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
    }
    //5rq
    public class SaveBookResponseDto
    {
        public int Id { get; set; }
    }

    //6 rq
    public class SaveReviewRequestDto
    {
        public string Message { get; set; }
        public string Reviewer { get; set; }
    }

    //6 rsp
    public class SaveReviewResponseDto
    {
        public int Id { get; set; }
    }
    //7 rq
    public class RateBookRequestDto
    {
        public int Score { get; set; }
    }
    //Base class
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Cover { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }

        public List<Review> Reviews { get; set; }
        public List<Rating> Ratings { get; set; }
    }
}
