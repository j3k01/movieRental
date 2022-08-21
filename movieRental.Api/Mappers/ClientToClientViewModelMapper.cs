using movieRental.Api.ViewModels;

namespace movieRental.Api.Mappers
{
    public class ClientToClientViewModelMapper
    {
        public static ClientViewModel ClientToClientViewModel(Domain.Client.Client client)
        {
            var clientViewModel = new ClientViewModel
            {
                ClientId = client.ClientId,
            };
            return clientViewModel;
        }

    }
}
