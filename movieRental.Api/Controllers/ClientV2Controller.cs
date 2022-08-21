using System.Threading.Tasks;
using movieRental.Api.Mappers;
using movieRental.Api.Validation;
using movieRental.Data.Sql;
using movieRental.IServices.Client;
using Microsoft.AspNetCore.Mvc;
using movieRental.Api.BindingModels;
using movieRental.Api.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace movieRental.Api.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/client")]
    public class ClientV2Controller : Controller
    {
        private readonly movieRentalDbContext _context;
        private readonly IClientService _clientService;

        /// <inheritdoc />
        public ClientV2Controller(movieRentalDbContext context, IClientService clientService)
        {
            _context = context;
            _clientService = clientService;
        }

        [HttpGet("{clientId:min(1)}", Name = "GetClientById")]
        public async Task<IActionResult> GetClientById(int clientId)
        {
            var client = await _clientService.GetClientByClientId(clientId);
            if (client != null)
            {
                return Ok(ClientToClientViewModelMapper.ClientToClientViewModel(client));
            }
            return NotFound();
        }

        [HttpGet("name/{clientName}", Name = "GetClientByClientName")]
        public async Task<IActionResult> GetClientByClientName(string clientName)
        {
            var client = await _clientService.GetClientByClientName(clientName);
            if (client != null)
            {
                return Ok(ClientToClientViewModelMapper.ClientToClientViewModel(client));
            }
            return NotFound();
        }

        [ValidateModel]
        public async Task<IActionResult> Post([FromBody] IServices.Requests.CreateClient createClient)
        {
            var client = await _clientService.CreateClient(createClient);

            return Created(client.ClientId.ToString(), ClientToClientViewModelMapper.ClientToClientViewModel(client));
        }


        [ValidateModel]
        [HttpPatch("edit/{clientId:min(1)}", Name = "EditClient")]
        //        public async Task<IActionResult> EditClient([FromBody] EditClient editClient,[FromQuery] int clientId)
        public async Task<IActionResult> EditClient([FromBody] IServices.Requests.EditClient editClient, int clientId)
        {
            await _clientService.EditClient(editClient, clientId);

            return NoContent();
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
