 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using movieRental.Common.Enums;

namespace movieRental.Data.Sql.DAO
{
    public class Client
    {
        public Client()
        {
            Orders = new List<Order>();
            Ratings = new List<Rating>();
        }
        public int ClientId { get; set; }
        public string ClientFname { get; set; }
        public string ClientLname { get; set; }
        public string ClientMail { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }


}
