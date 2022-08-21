using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using movieRental.Common.Enums;

namespace movieRental.Data.Sql.DAO
{
    public class Rating
    {

        public int RatingId { get; set; }
        public int MovieId { get; set; }
        public double MovieRating { get; set; }
        public int ClientId { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual Client Client { get; set; }

    }
}
