using Orleans.Runtime;

namespace Abarim.Contracts;

public interface IAccountGrain : IGrainWithStringKey
{
    Task<AccountDetails> GetAccountDetails();
}

public interface IBackendCoreGrain : IGrainWithIntegerKey
{
    public Task SetClient(Func<string, Task<string>> communicator);
    Task<string> GetReplyFromBackend(string msgPayload);
}

public interface IBackendCommunicator
{
    Task<string> GetReplyFromBackend(string connectionId, string msgPayload);
}

public record class AccountDetails(string Name);

[GenerateSerializer]
public record class AccountDetailsStorage
{
    [Id(0)]
    public string AccountKey { get; set; }

}