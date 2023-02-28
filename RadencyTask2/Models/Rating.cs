using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RadencyTask2.Models
{
    public class RatingDto
    {

        public int Score { get; set; }
    }
    public class Rating
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int Score { get; set; }

        public Book Book { get; set; }
    }
}
