using System;
using Xunit;

namespace movieRental.Domain.Tests.Client
{
    public class ClientTest
    {
        public ClientTest()
        {
            //Arrange
            //Act
            //Assert
        }

        [Fact]
        public void CreateClient_Returns_Correct_Response()
        {
            var client = new Domain.Client.Client("ClientFname", "ClientLname","ClientMail");

            Assert.Equal("ClientFname", client.ClientFname);
            Assert.Equal("ClientLname", client.ClientLname);
            Assert.Equal("ClientMail", client.ClientMail);
        }

    }
}
