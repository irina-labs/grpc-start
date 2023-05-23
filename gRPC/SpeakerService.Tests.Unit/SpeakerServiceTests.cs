using AutoMapper;
using Data;
using Data.Interface;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Speaker.Service;
using Speaker.Service.Mappings;
using Speaker.Service.Protos;

namespace SpeakerService.Tests.Unit
{
    public class SpeakerServiceTests
    {
        private readonly ISpeakerRepository speakerRepository;
        private readonly ISpeakerService sut;

        private ILogger<Speaker.Service.SpeakerService> logger;

        public IMapper mapper { get; set; }

        public SpeakerServiceTests()
        {
            this.speakerRepository = Substitute.For<ISpeakerRepository>();
            logger = Substitute.For<ILogger<Speaker.Service.SpeakerService>>();

            if (mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new SpeakerMappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                this.mapper = mapper;
            }

            this.sut = new Speaker.Service.SpeakerService(logger, speakerRepository, mapper);

        }


        [Fact]
        public async Task GetById_ShouldReturn_an_Object()
        {
            //Arrage
            // create a test context
            var callContext = TestServerCallContext.Create();
            var requestObject= new SpeakerFilterRequest { Id = 1 };


            var speaker = new Domain.Speaker()
            {
                Id = 4,
                City = "Bucharest",
                Country = "Belgium",
                Email = "speaker@mail.com",
                FirstName = "Dev",
                LastName = "Speaker",
                Website = "www.speaker.com"
            };

            speakerRepository.GetByIdAsync(Arg.Any<int>())
              .Returns(speaker);

            var speakerResponse = new SpeakerResponse
            {
                Id = 4,
                City = "Bucharest",
                Country = "Belgium",
                Email = "speaker@mail.com",
                FirstName = "Dev",
                LastName = "Speaker",
                Website = "www.speaker.com",
            };

            //create a request object to pass down
            //create a Domain.Speaker to return


            //Act

            var response = await sut.GetById(requestObject, callContext);

            //Assert

            response.Should().BeEquivalentTo(speakerResponse);

        }
    }
}