using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Internship;
using Internship.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace Internship.DataAccess.Hubs
{
	public class ChatHub : Hub
	{
        private readonly ApplicationDbContext _db;
        public ChatHub(ApplicationDbContext db)
        {
            _db = db;
        }
        //public async Task SendMessageToAll(string user,  string message)
		//{
		//	await Clients.All.SendAsync("MessageReceived", user , message);
		//}
		public async Task SendMessageToReceiver(string sender, string receiver, string message)
		{
            var userId = _db.Users.FirstOrDefault(u=>u.Email.ToLower()==receiver.ToLower()).Id;
            if(!string.IsNullOrEmpty(userId)) 
            {
                await Clients.User(userId).SendAsync("MessageReceived", sender, message);
            }
        }
    }
}
