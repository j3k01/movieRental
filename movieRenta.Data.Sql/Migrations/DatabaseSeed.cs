using System;
using System.Collections.Generic;
using System.Linq;
using movieRental.Common.Enums;
using movieRental.Data.Sql.DAO;

namespace movieRental.Data.Sql.Migrations
{
    //klasa odpowiadająca za wypełnienie testowymi danymi bazę danych
    public class DatabaseSeed
    {
        private readonly movieRentalDbContext _context;

        //wstrzyknięcie instancji klasy movieRentalDbContext poprzez konstruktor
        public DatabaseSeed(movieRentalDbContext context)
        {
            _context = context;
        }

        //metoda odpowiadająca za uzupełnienie utworzonej bazy danych testowymi danymi
        //kolejność wywołania ma niestety znaczenie, ponieważ nie da się utworzyć rekordu
        //w bazie dnaych bez znajmości wartości klucza obcego
        //dlatego należy zacząć od uzupełniania tabel, które nie posiadają kluczy obcych
        //--OFFTOP
        //w przeciwną stronę działa ręczne usuwanie tabel z wypełnionymi danymi w bazie danych
        //należy zacząć od tabel, które posiadają klucze obce, a skończyć na tabelach, które 
        //nie posiadają




        public void Seed()
        {
            //regiony pozwalają na zwinięcie kodu w IDE
            //nie sa dobrą praktyką, kod w danej klasie/metodzie nie powinien wymagać jego zwijania
            //zastosowałem je z lenistwa nie powinno to mieć miejsca 
            #region CreateClients
            var clientList = BuildClientList();
            //dodanie użytkowników do tabeli User
            _context.Client.AddRange(clientList);
            //zapisanie zmian w bazie danych
            _context.SaveChanges();
            #endregion

            #region CreateMovies
            var movieList = BuildMovieList();
            _context.Movie.AddRange(movieList);
            _context.SaveChanges();
            #endregion

            #region CreateRatings
            var ratingList = BuildRatingList(clientList,movieList);
            _context.Rating.AddRange(ratingList);
            _context.SaveChanges();
            #endregion

            #region CreateOrders
            var orderList = BuildOrderList(clientList);
            _context.Order.AddRange(orderList);
            _context.SaveChanges();
            #endregion


            #region CreateMovieOrders
            var movieOrderList = BuildMovieOrderList(orderList,movieList);
            _context.MovieOrder.AddRange(movieOrderList);
            _context.SaveChanges();
            #endregion



        }

        private IEnumerable<DAO.Client> BuildClientList()
        {
            var userList = new List<DAO.Client>();
            var user = new DAO.Client()
            {
                ClientFname = "Jakub",
                ClientLname = "Kostka",
                ClientMail = "jakub.kostka@student.po.edu.pl",
            };
            userList.Add(user);

            var user2 = new DAO.Client
            {
                ClientFname = "Adam",
                ClientLname = "Nowak",
                ClientMail = "adam.nowak@gmail.com",
            };
            userList.Add(user2);

            for (int i = 3; i <= 20; i++)
            {
                var user3 = new DAO.Client
                {
                    ClientFname = "Zuzanna" + i,
                    ClientLname = "Kowalska" + i,
                    ClientMail = "z.kowalska" + i + "@gmail.com",
                };
                userList.Add(user3);
            }

            return userList;
        }
        private IEnumerable<Movie> BuildMovieList()
        {
            var movieList = new List<Movie>();
            var movie = new Movie()
            {
                //MovieId = 11,
                MovieTitle = "Spider-Man: No way home",
                MovieDirector = "Jon Watts",
                MovieAvailability = true,
                //MovieRating = 8.5
            };
            movieList.Add(movie);

            var movie2 = new Movie()
            {
                //MovieId = 12,
                MovieTitle = "Avatar",
                MovieDirector = "James Cameron",
                MovieAvailability = false,
                //MovieRating = 7.9
            };
            movieList.Add(movie2);

            for (int i = 3; i <= 20; i++)
            {
                var movie3 = new Movie
                {
                    //MovieId = 13 + i,
                    MovieTitle = "Film" + i,
                    MovieDirector = "Reżyser" + i,
                    MovieAvailability = true,
                    //MovieRating = 5
                };
                movieList.Add(movie3);
            }

            return movieList;
        }

        private IEnumerable<Order> BuildOrderList(
        IEnumerable<DAO.Client> clientList)
        {
            var orderList = new List<Order>();
            foreach(var client in clientList)
            {
                if (client.ClientFname == "Jan")
                    orderList.Add(new Order
                    {
                        ClientId = clientList.First(x => x.ClientFname == "Jan").ClientId,
                        PaymentMethod = "cash",
                        TransDate = DateTime.Now.AddYears(-2),
                    });
                if (client.ClientFname != "Jan"&&client.ClientFname == "Karol")
                    orderList.Add(new Order
                    {
                        ClientId = clientList.First(x => x.ClientFname == "Karol").ClientId,
                        PaymentMethod = "blik",
                        TransDate = DateTime.Now,
                    });
            }
            foreach(var client in clientList)
            {
                if (client.ClientFname != "Jan" && client.ClientFname != "Karol")
                    orderList.Add(new Order
                    {
                        ClientId = clientList.First(x => x.ClientFname == "Zuzanna13").ClientId,
                        PaymentMethod = "blik",
                        TransDate = DateTime.Now,
                    });
                if (client.ClientFname != "Jan" && client.ClientFname == "Karol")
                    orderList.Add(new Order
                    {
                        ClientId = clientList.First(x => x.ClientFname == "Karol").ClientId,
                        PaymentMethod = "blik",
                        TransDate = DateTime.Now,
                    });
            }
            return orderList;
        }
        private IEnumerable<Rating> BuildRatingList(
                IEnumerable<DAO.Client> clientList,
                IEnumerable<Movie> movieList)
            {
            var ratingList = new List<Rating>();
            Random rnd = new Random();
            foreach(var client in clientList)
            {
                if (client.ClientFname != "Jan")
                    ratingList.Add(new Rating
                    {
                        MovieId = movieList.ToList()[1].MovieId,
                        ClientId = client.ClientId,
                        MovieRating = rnd.Next(1,10),
                    }) ;
                
            }
            return ratingList;
            }

        private IEnumerable<MovieOrder> BuildMovieOrderList(
        IEnumerable<Order> orderList,
        IEnumerable<Movie> movieList)
        {
            var movieOrderList = new List<MovieOrder>();
            var rng = new Random();
            var mOrderNum = orderList.ToList().Count;
            var mMovieSize = movieList.ToList().Count;
            for(int i = 0; i < 10; i++)
            {
                movieOrderList.Add(new MovieOrder
                {
                    OrderId = orderList.ToList()[rng.Next(mOrderNum)].OrderId,
                    MovieId = movieList.ToList()[rng.Next(mMovieSize)].MovieId,
                });

            }
            return movieOrderList;

        }
    }
}
