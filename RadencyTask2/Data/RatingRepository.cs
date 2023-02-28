using RadencyTask2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RadencyTask2.Data
{
    public interface IRatingRepository
    {
        void Add(Rating rating);
        bool Save();
    }

    public class RatingRepository : IRatingRepository
    {
        private readonly IDbContext _dbContext;

        public RatingRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Rating rating)
        {
            _dbContext.Ratings.Add(rating);
        }

        public bool Save()
        {
            try
            {
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
