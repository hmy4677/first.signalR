using System;
using Microsoft.AspNetCore.SignalR;

namespace Demo.SiganlR.Chat.Hubs
{
  public class ChatHub : Hub
  {
    //public override Task OnConnectedAsync()
    //{
    //  var user = ConnectionCollection.Users.FirstOrDefault(p => p.ConnectionId == Context.ConnectionId);
    //  if (user == null)
    //  {
    //    ConnectionCollection.AddUser(new User
    //    {
    //      ConnectionId = Context.ConnectionId,
    //      Name = ""
    //    });
    //  }
    //  return base.OnConnectedAsync();
    //}
    //public Task DisConnected(bool stopCalled)
    //{
    //  var user = ConnectionCollection.Users.Where(p => p.ConnectionId == Context.ConnectionId).FirstOrDefault();
    //  if (user != null)
    //  {
    //    ConnectionCollection.RemoveUser(user);
    //  }
    //  return base.OnDisconnectedAsync(Exception);
    //}
    public async Task AddUserName(string userName)
    {
      var user = ConnectionCollection.Users.FirstOrDefault(p => p.ConnectionId == Context.ConnectionId);
      if (user == null)
      {
        ConnectionCollection.AddUser(new User
        {
          ConnectionId = Context.ConnectionId,
          Name = userName
        });
      }
    }
    public async Task PublicMessage(string userName, string messageContent)
    {
      await Clients.All.SendAsync("publicMessage", userName, messageContent);
    }
    public async Task PrivateMessage(string userName, string toUserName, string messageContent)
    {
      Console.WriteLine($"{userName} to {toUserName} a msg");
      var user = ConnectionCollection.Users.FirstOrDefault(p => p.Name==toUserName);

      if (user != null)
      {
        await Clients.Client(user.ConnectionId).SendAsync("messageReceived", userName, messageContent);
      }
      else
      {
        throw new Exception("this user in offline");
      }
    }
  }
}

