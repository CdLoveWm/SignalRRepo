using Microsoft.AspNetCore.SignalR;
using SignaRl.Server.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignaRl.Server.Hubs
{
    public class MyHub: Hub
    {
        private readonly IHubServices _hubServices;
        public MyHub(IHubServices hubServices)
        {
            _hubServices = hubServices;
        }
        /// <summary>
        /// 连接成功
        /// </summary>
        /// <returns></returns>
        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"连接成功：{Context.ConnectionId}");
            _hubServices.AddClient(Context.ConnectionId, "");
            return base.OnConnectedAsync();
        }
        /// <summary>
        /// 连接断开
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine($"断开连接：{Context.ConnectionId}");
            _hubServices.RemvoeClient(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="clientName"></param>
        /// <param name="message"></param>
        public async void SendMessage(string clientName, object message)
        {
            Console.WriteLine($"收到消息：{message}");
            await Clients.All.SendAsync("ReceiveMessage", clientName, message);
        }
        /// <summary>
        /// 获取客户端个数
        /// </summary>
        /// <returns></returns>
        public List<string> GetConnections()
        {
            return _hubServices.GetConnections();
        }
    }
}
