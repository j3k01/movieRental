using System.Threading.Tasks;

namespace movieRental.IData.Client
{
    public interface IClientRepository
    {
        Task<int> AddClient(movieRental.Domain.Client.Client client);
        Task<movieRental.Domain.Client.Client> GetClient(int clientId);
        Task<movieRental.Domain.Client.Client> GetClient(string clientFname);
        Task EditClient(Domain.Client.Client client);
        Task RemoveClient(Domain.Client.Client client);
    }
}
