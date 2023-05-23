
namespace SpeakerService.Tests.Integration
{
    using FluentAssertions;
    using Speaker.Service.Protos;
    public class SpeakerServiceTests : IClassFixture<MyFactory<Program>>
    {
        private readonly MyFactory<Program> factory;

        public SpeakerServiceTests(MyFactory<Program> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task GetByID_Async()
        {
            //Arrange
             var client = factory.CreateGrpcClient();   
             var expectedResponse= new SpeakerResponse()
             {
                 Id = 5,
                 City = "Oslo",
                 Country = "Norway",
                 Email = "",
                 Bio = "",
                 FirstName = "",
                 LastName = "",
                 Website = ""
             };

            //Act

            var response = await client.GetByIdAsync(new SpeakerFilterRequest { Id = 5 });

            // Assert
            response.Should().BeEquivalentTo(expectedResponse);
        }
    }
}