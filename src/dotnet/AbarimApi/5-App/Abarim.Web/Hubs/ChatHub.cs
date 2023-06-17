using Microsoft.AspNetCore.SignalR;

namespace Abarim.Web.Hubs;

public class BackendCoreHub : Hub
{
//    private Dictionary<string, 
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public async Task<string> GetReplyFromBackend(string msgPayload)
    {
        var message = await Clients.Client(connectionId).InvokeAsync<string>(
            msgPayload);
        return message;
    }
}