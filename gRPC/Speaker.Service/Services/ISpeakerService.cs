using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Speaker.Service.Protos;

namespace Speaker.Service
{
    public interface ISpeakerService
    {
        Task<Empty> Delete(SpeakerFilterRequest request, ServerCallContext context);
        Task GetAll(Empty request, IServerStreamWriter<SpeakerResponse> responseStream, ServerCallContext context);
        Task<SpeakerResponse> GetById(SpeakerFilterRequest request, ServerCallContext context);
        Task<Empty> Update(SpeakerUpdateRequest request, ServerCallContext context);
    }
}