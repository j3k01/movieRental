using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using movieRental.Common.Enums;

namespace movieRental.Data.Sql.DAO
{
    public class Order
    {
        public Order()
        {
            MovieOrders = new List<MovieOrder>();
        }
        public int OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime TransDate { get; set; }
        public int ClientId { get; set; }


        public virtual ICollection<MovieOrder> MovieOrders { get; set; }
        public virtual Client Client { get; set; }
    }
}
