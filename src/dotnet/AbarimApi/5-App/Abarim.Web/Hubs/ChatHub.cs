using Abarim.Contracts;
using Microsoft.AspNetCore.SignalR;

namespace Abarim.Web.Hubs;

public class BackendCoreHub : Hub, IBackendCommunicator
{
    private readonly IGrainFactory _grains;
    private int _currGrainCount = 0;
    private int _lastGrainUsedId = 0;

    public BackendCoreHub(IGrainFactory grains)
    {
        this._grains = grains;
    }
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public override async Task OnConnectedAsync()
    {
        var connectionId = Context.ConnectionId;
        var communicator = (string msPayload) =>
        {
            return GetReplyFromBackend(connectionId, msPayload);
        };
        // TODO: creatre actor and pass method to allow communication with new client
        //   maybe have Orleans do round robin on actors for us?
        var newGrain = _grains.GetGrain<IBackendCoreGrain>(_currGrainCount++);
        await base.OnConnectedAsync();
    }

    public async Task<string> GetReplyFromBackend(string connectionId, string msgPayload)
    {
        var message = await Clients.Client(connectionId).InvokeAsync<string>("GetData",
            msgPayload, CancellationToken.None);
            
        return message;
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }
}