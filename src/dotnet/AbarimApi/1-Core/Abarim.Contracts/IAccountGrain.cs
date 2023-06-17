namespace Abarim.Contracts;
public interface IAccountGrain : IGrainWithStringKey
{
    Task<AccountDetails> GetAccountDetails();
}

public interface IBackendCore {
    Task<string> GetReplyFromBackend(string msgPayload);
}

public record class AccountDetails(string Name);

[GenerateSerializer]
public record class AccountDetailsStorage
{
    [Id(0)]
    public string AccountKey { get; set; }

}