using Abarim.Contracts;
using Orleans.Runtime;

namespace Abarim.Actors;

public sealed class BackendCoreGrain : Grain, IBackendCoreGrain
{
    private Func<string, Task<string>> _communicator;

    public Task SetClient(Func<string, Task<string>> communicator)
    {
        _communicator = communicator;
        return Task.CompletedTask;
    }

    public Task<string> GetReplyFromBackend(string msgPayload)
    {
        _communicator.GetReplyFromBackend(msgPayload);
    }
}
