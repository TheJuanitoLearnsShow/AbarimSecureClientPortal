namespace Abarim.Actors;

public sealed class AccountGrain : Grain, IAccountGrain
{
    private readonly IPersistentState<AccountDetailsStorage> _state;

    public AccountGrain(
        [PersistentState(
            stateName: "accounts",
            storageName: "accounts")]
        IPersistentState<AccountDetailsStorage> state) => _state = state;

    public async Task<AccountDetails> GetAccountDetails()
    {
        // call the Hub actor?
        return new AccountDetails("John");
    }

}
