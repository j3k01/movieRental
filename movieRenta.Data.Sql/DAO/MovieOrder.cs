using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using movieRental.Common.Enums;

namespace movieRental.Data.Sql.DAO
{
    public class MovieOrder
    {

        public int MovieOrderId { get; set; }
        public int MovieId { get; set; }
        public int OrderId { get; set; }

        public virtual Order Order { get; set; }
        public virtual Movie Movie { get; set; }

    }
}
