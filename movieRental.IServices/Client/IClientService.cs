using System.Threading.Tasks;
using movieRental.IServices.Requests;

namespace movieRental.IServices.Client
{
    public interface IClientService
    {
        Task<movieRental.Domain.Client.Client> GetClientByClientId(int clientId);
        Task<movieRental.Domain.Client.Client> GetClientByClientName(string clientFname);
        Task<movieRental.Domain.Client.Client> CreateClient(CreateClient createClient);
        Task EditClient(EditClient createClient, int uclientId);
        Task RemoveClient(int clientId);
    }
}
