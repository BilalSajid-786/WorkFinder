using Microsoft.AspNetCore.SignalR;
using WorkFinder.Common;

namespace WorkFinder.Api.SignalR
{
    public class ChatHub : Hub
    {
        private readonly UserConnectionManager _connections;

        public ChatHub(UserConnectionManager connections)
        {
            _connections = connections;
        }

        public override Task OnConnectedAsync()
        {
            var userIdStr = Context.GetHttpContext()?.Request.Query["userId"];
            if(Guid.TryParse(userIdStr, out var userId))
            {
                _connections.AddConnection(userId,Context.ConnectionId);
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            _connections.RemoveConnection(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(Guid senderId, Guid receiverId, string message)
        {
            var receiverConnection = _connections.GetConnectionId(receiverId);
            if(receiverConnection != null)
            {
                await Clients.Client(receiverConnection)
                    .SendAsync("ReceiveMessage",senderId,message);
            }
        }
    }
}
