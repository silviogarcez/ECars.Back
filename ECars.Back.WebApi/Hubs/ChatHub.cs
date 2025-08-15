using Microsoft.AspNetCore.SignalR;

namespace ECars.Back.Hubs
{
    public class ChatHub : Hub
    {
        public async Task EnviarMensagem(string user, string message)
        {
            await Clients.All.SendAsync("ReceberMensagem", user, message);
        }
    }
}