using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using movieRental.Common.Enums;

namespace movieRental.Data.Sql.DAO
{
    public class Movie
    {
        public Movie()
        {
            MovieOrders = new List<MovieOrder>();
            Ratings = new List<Rating>();
        }
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public string MovieDirector { get; set; }
        public bool MovieAvailability { get; set; }
        public double MovieRating { get; set; }

        public virtual ICollection<MovieOrder> MovieOrders { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
