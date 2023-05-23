using AutoMapper;
using Data.Interface;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Speaker.Service.Protos;

namespace Speaker.Service
{
    public class SpeakerService : SpeakerServiceDefinition.SpeakerServiceDefinitionBase
    {
        private readonly ILogger<SpeakerService> logger;
        private readonly ISpeakerRepository speakerRepository;
        private readonly IMapper mapper;

        public SpeakerService(ILogger<SpeakerService> logger, ISpeakerRepository speakerRepository, IMapper mapper)
        {
            this.logger = logger;
            this.speakerRepository = speakerRepository;
            this.mapper = mapper;
        }

        public override Task<SpeakerResponse> GetById(SpeakerFilterRequest request, ServerCallContext context)
        {
            //if (!context.RequestHeaders.Where(x => x.Key == "grpc-previous-rpc-attempts").Any())
            //{
            //    throw new RpcException(new Status(StatusCode.Internal, $"Not here:Try again"));
            //}

            var speaker = speakerRepository.GetByIdAsync(request.Id);
            if (speaker.Result == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Speaker is not found"));
            }


            var speakerResponse = mapper.Map<SpeakerResponse>(speaker.Result);
            //Metadata.Entry myfakeHeader = new Metadata.Entry("my-fake-header", "grpc-header");
            //context.ResponseTrailers.Add(myfakeHeader);
            return Task.FromResult(speakerResponse);
        }

        public override async Task GetAll(Empty request, IServerStreamWriter<SpeakerResponse> responseStream, ServerCallContext context)
        {
            var speakers = await speakerRepository.GetAllAsync();
            foreach (var speaker in speakers)
            {
                var speakerResponse = mapper.Map<SpeakerResponse>(speaker);
                // context.ResponseTrailers.Add("key1", "value1");//this exhausts 
                await responseStream.WriteAsync(speakerResponse);
            }

            Metadata.Entry myfakeHeader = new Metadata.Entry("my-fake-header", "grpc-header");
            context.ResponseTrailers.Add(myfakeHeader);
            await Task.CompletedTask;
        }

        public override async Task<Empty> Update(SpeakerUpdateRequest request, ServerCallContext context)
        {
            var speakerToUpdate = mapper.Map<Domain.Speaker>(request);

            var updateSucceeded = await speakerRepository.UpdateAsync(speakerToUpdate);
            if (updateSucceeded <= 0)
            {
                throw new RpcException(new Status(StatusCode.Internal, $"Speaker with Id {request.Id} hasn't been updated"));

            }

            return new Empty();

        }

        public override async Task<Empty> Delete(SpeakerFilterRequest request, ServerCallContext context)
        {
            var deleteSucceed = await speakerRepository.DeleteAsync(request.Id);

            if (deleteSucceed <= 0)
                throw new RpcException(new Status(StatusCode.Internal, $"Speaker with Id {request.Id} hasn't been deleted."));

            return new Empty();
        }

        /// <summary>
        /// TO be implemented?
        /// </summary>
        private void ValidateSpeaker()
        {

        }
    }
}
