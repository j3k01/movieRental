using System.Threading.Tasks;
using movieRental.IData.Client;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;

namespace movieRental.Data.Sql.Client
{
    public class ClientRepository : IClientRepository
    {
        private readonly movieRentalDbContext _context;

        public ClientRepository(movieRentalDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddClient(Domain.Client.Client client)
        {
            var clientDAO = new DAO.Client
            {
                ClientFname = client.ClientFname,
                //ClientId = client.ClientId,
                ClientLname = client.ClientLname,
                ClientMail = client.ClientMail,
            };
            await _context.AddAsync(clientDAO);
            await _context.SaveChangesAsync();
            return clientDAO.ClientId;
        }

        public async Task<Domain.Client.Client> GetClient(int clientId)
        {
            var client = await _context.Client.FirstOrDefaultAsync(x => x.ClientId == clientId);
            return new Domain.Client.Client(client.ClientId,
                client.ClientFname,
                client.ClientLname,
                client.ClientMail
                );
        }

        public async Task<Domain.Client.Client> GetClient(string clientFname)
        {
            var client = await _context.Client.FirstOrDefaultAsync(x => x.ClientFname == clientFname);
            return new Domain.Client.Client(client.ClientId,
                client.ClientFname,
                client.ClientLname,
                client.ClientMail
               );
        }

        public async Task EditClient(Domain.Client.Client client)
        {
            var editClient = await _context.Client.FirstOrDefaultAsync(x => x.ClientId == client.ClientId);
            editClient.ClientFname = client.ClientFname;
            editClient.ClientLname = client.ClientLname;
            editClient.ClientMail = client.ClientMail;
            await _context.SaveChangesAsync();
        }
        public async Task RemoveClient(Domain.Client.Client client)
        {
            var outWhile = true;
            do
            {
                var removeOrderClient = await _context.Order.FirstOrDefaultAsync(n => n.ClientId == client.ClientId);
                if (removeOrderClient != null)
                {
                    _context.Order.Remove(removeOrderClient);
                }
                else
                {
                    outWhile = false;
                }
                await _context.SaveChangesAsync();
            } while (outWhile);

            var removeRatingClient = await _context.Rating.FirstOrDefaultAsync(n => n.ClientId == client.ClientId);
            if (removeRatingClient != null)
            {
                _context.Rating.Remove(removeRatingClient);
            }
            var removeClient = await _context.Client.FirstOrDefaultAsync(n => n.ClientId == client.ClientId);
            if (removeClient != null)
            {
                _context.Client.Remove(removeClient);
            }

            await _context.SaveChangesAsync();
        }



    }

}
