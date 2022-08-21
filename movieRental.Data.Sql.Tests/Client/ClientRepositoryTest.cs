using System;
using System.Threading.Tasks;
using movieRental.Data.Sql.Client;
using movieRental.IData.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xunit;
namespace movieRental.Data.Sql.Tests.Client
{
    public class ClientRepositoryTest
    {
        public IConfiguration Configuration { get; }
        private movieRentalDbContext _context;
        private IClientRepository _clientRepository;

        public ClientRepositoryTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<movieRentalDbContext>();
            optionsBuilder.UseMySQL(
                "server=localhost;userid=root;pwd=root;port=3307;database=movierental_db;");
            _context = new movieRentalDbContext(optionsBuilder.Options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            _clientRepository = new ClientRepository(_context);
        }

        [Fact]
        public async Task AddUser_Returns_Correct_Response()
        {
            var client = new Domain.Client.Client("ClientFname", "ClientLname","ClientMail");

            var clientId = await _clientRepository.AddClient(client);

            var createdClient = await _context.Client.FirstOrDefaultAsync(x => x.ClientId == clientId);
            Assert.NotNull(createdClient);

            _context.Client.Remove(createdClient);
            await _context.SaveChangesAsync();
        }

    }
}
