using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using strikeshowdown_backend.Models.Hub;
using strikeshowdown_backend.Services.Context;

namespace strikeshowdown_backend.Hubs
{
    public sealed class ChatHub : Hub
    {
      private readonly SharedDB _shared;

        public ChatHub(SharedDB db){
            _shared = db;
        }

        public async Task JoinChat(UserConnection con){
            await Clients.All.SendAsync("ReceiveMessage", con.Username, $"{con.Username} has entered the chat. Say hi!");
        }

        public async Task JoinSpecificChat(UserConnection con){
            await Groups.AddToGroupAsync(Context.ConnectionId, con.Chatroom);

            _shared.connections[Context.ConnectionId] = con;

            await Clients.Group(con.Chatroom).SendAsync("JoinSpecificChat", con.Username, $"{con.Username} has entered the chat. Say hi!");
        }

        public async Task SendMessage(string msg){
            if(_shared.connections.TryGetValue(Context.ConnectionId, out UserConnection conn)){
                await Clients.Group(conn.Chatroom).SendAsync("ReceiveSpecificMessage", conn.Username, msg);
            }
        }
    }
}