using System;
using System.Threading.Tasks;
using movieRental.IData.Client;
using movieRental.IServices.Requests;
using movieRental.IServices.Client;
using movieRental.Services.Client;
using Moq;
using Xunit;

namespace movieRental.Services.Tests.Client
{
    public class ClientServiceTest
    {
        private readonly IClientService _clientService;
        private readonly Mock<IClientRepository> _clientRepositoryMock;

        public ClientServiceTest()
        {
            //Arrange
            _clientRepositoryMock = new Mock<IClientRepository>();
            _clientService = new ClientService(_clientRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateClient_Returns_Correct_Response()
        {
            var client = new CreateClient
            {
                ClientFname = "Jan",
                ClientLname = "Piłsudski",
                ClientMail = "mail333@gmail.com"
            };

            int expectedResult = 0;
            _clientRepositoryMock.Setup(x => x.AddClient
                (new Domain.Client.Client
                (client.ClientFname,
                client.ClientLname,
                client.ClientMail
                   )))
                .Returns(Task.FromResult(expectedResult));

            //Act
            var result = await _clientService.CreateClient(client);

            //Assert
            Assert.IsType<Domain.Client.Client>(result);
            Assert.NotNull(result);
            Assert.Equal(expectedResult, result.ClientId);
        }

    }
}
