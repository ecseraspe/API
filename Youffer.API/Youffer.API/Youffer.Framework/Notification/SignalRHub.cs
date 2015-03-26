// ---------------------------------------------------------------------------------------------------
// <copyright file="SignalRHub.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-12-10</date>
// <summary>
//     The SignalRHub class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Framework.Notification
{
    using System.Threading.Tasks;
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;
    using Youffer.Resources.ViewModel;

    /// <summary>
    /// Signal R Hub Class
    /// </summary>
    [HubName("chat")]
    public class SignalRHub : Hub
    {
        /// <summary>
        /// Send Message to Company from User
        /// </summary>
        /// <param name="clientId"> The client Id. </param>
        /// <param name="message"> The message </param>
        public void SendMessage(string clientId, MessagesDto message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<SignalRHub>();
            context.Clients.Group(clientId).addMessage(message);
        }

        /// <summary>
        /// Sends the credit update message.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="balance">The balance.</param>
        public void SendCreditUpdateMessage(string clientId, UserBalanceModelDto balance)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<SignalRHub>();
            context.Clients.Group(clientId).addCreditUpdateMessage(balance);
        }

        /// <summary>
        /// Joins the room.
        /// </summary>
        /// <param name="roomName">Name of the room.</param>
        /// <returns> Task obj</returns>
        public Task JoinRoom(string roomName)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<SignalRHub>();
            return context.Groups.Add(Context.ConnectionId, roomName);
        }

        /// <summary>
        /// Leaves the room.
        /// </summary>
        /// <param name="roomName">Name of the room.</param>
        /// <returns> Task obj</returns>
        public Task LeaveRoom(string roomName)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<SignalRHub>();
            return context.Groups.Remove(Context.ConnectionId, roomName);
        }
    }
}
