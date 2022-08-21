using System;
using System.Threading.Tasks;
using movieRental.Api.BindingModels;
using movieRental.Api.Validation;
using movieRental.Api.ViewModels;
using movieRental.Data.Sql;
using movieRental.Data.Sql.DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using movieRental.Services;
using movieRental.Api.Mappers;
using movieRental.Api.Validation;
using movieRental.Data.Sql;
using movieRental.IServices.Client;
using Microsoft.AspNetCore.Mvc;
namespace movieRental.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]

    public class ClientController : Controller
    {
        private readonly movieRentalDbContext _context;
        private readonly IClientService _clientService;

        public ClientController(movieRentalDbContext context, IClientService clientService)
        {
            _context = context;
            _clientService = clientService;
        }
        [HttpGet(template: "GetAllClients")]
        public List<Client> GetAllClients()
        {
            return _context.Client.ToList();
        }


        [HttpGet("{clientId:min(1)}", Name = "GetClientById")]
        public async Task<IActionResult> GetClientById(int clientId)
        {
            var client = await _context.Client.FirstOrDefaultAsync(x => x.ClientId == clientId);

            if (client != null)
            {
                return Ok(new ClientViewModel
                {
                    ClientId = client.ClientId,
                    ClientFname = client.ClientFname,
                    ClientLname = client.ClientLname,
                    ClientMail = client.ClientMail

                });
            }

            return NotFound();
        }

        [HttpGet("name/{clientName}", Name = "GetClientByClientName")]
        public async Task<IActionResult> GetClientByClientName(string clientFname)
        {
            var client = await _context.Client.FirstOrDefaultAsync(x => x.ClientFname == clientFname);

            if (client != null)
            {
                return Ok(new ClientViewModel
                {
                    ClientId = client.ClientId,
                    ClientFname = client.ClientFname,
                    ClientLname = client.ClientLname,
                    ClientMail = client.ClientMail,
                });
            }

            return NotFound();
        }

        [ValidateModel]
        //        [Consumes("application/x-www-form-urlencoded")]
        //[HttpPost("create", Name = "CreateClient")]
        public async Task<IActionResult> Post([FromBody] CreateClient createClient)
        {
            var client = new Client
            {
                ClientMail = createClient.ClientMail,
                ClientFname = createClient.ClientFname,
                ClientLname = createClient.ClientLname,
            };
            await _context.AddAsync(client);
            await _context.SaveChangesAsync();

            return Created(client.ClientId.ToString(), new ClientViewModel
            {
                ClientId = client.ClientId,
                ClientFname = client.ClientFname,
                ClientLname = client.ClientLname,
                ClientMail = client.ClientMail,

            });
        }

        [ValidateModel]
        [HttpPatch("edit/{clientId:min(1)}", Name = "EditClient")]
        public async Task<IActionResult> EditClient([FromBody] EditClient editClient, int clientId)
        {
            var client = await _context.Client.FirstOrDefaultAsync(x => x.ClientId == clientId);
            client.ClientFname = editClient.ClientFname;
            client.ClientLname = editClient.ClientLname;
            client.ClientMail = editClient.ClientMail;
            await _context.SaveChangesAsync();
            return NoContent();
            return Ok(new ClientViewModel
            {
                ClientId = client.ClientId,
                ClientFname = client.ClientFname,
                ClientLname = client.ClientLname,
                ClientMail = client.ClientMail,
            });
        }
        [HttpDelete("remove/{clientId:min(1)}", Name = "RemoveCient")]
        [ValidateModel]
        public async Task<IActionResult> RemoveClient(int ClientId)
        {

            await _clientService.RemoveClient(ClientId);
            return NoContent();
        }
    }
}
