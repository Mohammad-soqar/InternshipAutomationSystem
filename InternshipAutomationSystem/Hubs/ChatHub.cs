using Microsoft.AspNetCore.SignalR;
using Internship.DataAccess.Data;
using Microsoft.AspNetCore.Authorization;

namespace InternshipAutomationSystem.Hubs
{
	public class ChatHub : Hub
	{
		private readonly ApplicationDbContext _db;
        public ChatHub(ApplicationDbContext db)
        {
			_db = db;
        }

        public async Task SendMessageToAll(string user, string message)
		{
			await Clients.All.SendAsync("MessageReceived", user, message);
		}

		[Authorize]
		public async Task SendMessageToReceiver(string sender, string receiver, string message)
		{
			var userId = _db.Users.FirstOrDefault(u => u.Id == sender).Id;

			if(!string.IsNullOrEmpty(userId)) 
			{
				await Clients.User(userId).SendAsync("MessageReceived", sender, message);
			}
		}
	}
}
