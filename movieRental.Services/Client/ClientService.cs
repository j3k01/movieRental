using System.Threading.Tasks;
using movieRental.IData.Client;
using movieRental.IServices.Requests;
using movieRental.IServices.Client;
using movieRental.Api.BindingModels;

namespace movieRental.Services.Client
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public Task<Domain.Client.Client> GetClientByClientId(int clientId)
        {
            return _clientRepository.GetClient(clientId);
        }

        public Task<Domain.Client.Client> GetClientByClientName(string clientName)
        {
            return _clientRepository.GetClient(clientName);
        }

        public async Task<Domain.Client.Client> CreateClient(CreateClient createClient)
        {
            var client = new Domain.Client.Client(createClient.ClientFname, createClient.ClientLname, createClient.ClientMail);
            client.ClientId = await _clientRepository.AddClient(client);
            return client;
        }

        public async Task EditClient(EditClient editClient, int clientId)
        {
            var client = await _clientRepository.GetClient(clientId);
            client.EditClient(editClient.ClientFname, editClient.ClientLname, editClient.ClientMail);
            await _clientRepository.EditClient(client);
        }
        public async Task RemoveClient(int clientId)
        {
            var client = await _clientRepository.GetClient(clientId);
            await _clientRepository.RemoveClient(client);
        }

    }

}
