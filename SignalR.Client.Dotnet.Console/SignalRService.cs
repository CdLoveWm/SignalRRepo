using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Client.Dotnet.ConsoleClient
{
    /// <summary>
    /// SignalR服务
    /// </summary>
    public class SignalRService
    {
        public HubConnection connection { get; set; }
        public SignalRService()
        {
            InitConnection();
        }

        private void InitConnection()
        {
            // 建立连接
            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/myhub")
                .WithAutomaticReconnect()
                .Build();
            #region 事件绑定
            // 重连事件
            connection.Reconnecting += Reconnecting;
            // 重连成功事件
            connection.Reconnected += Reconnected;
            // 连接关闭事件
            connection.Closed += ConnectionClosed;
            #endregion

            // 请求监听收到消息
            connection.On<string, string>("ReceiveMessage", ReceiveMessage);
            // 启动
            connection.StartAsync().Wait();
        }

        #region 事件
        /// <summary>
        /// 连接断开
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Task ConnectionClosed(System.Exception arg)
        {
            System.Console.WriteLine(arg.Message);
            System.Console.WriteLine("连接关闭...");
            return Task.CompletedTask;
        }
        /// <summary>
        /// 重连成功
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Task Reconnected(string arg)
        {
            System.Console.WriteLine(arg);
            System.Console.WriteLine("重连成功...");
            return Task.CompletedTask;
        }
        /// <summary>
        /// 链接重连时触发事件
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Task Reconnecting(System.Exception arg)
        {
            System.Console.WriteLine(arg.Message);
            System.Console.WriteLine("正在重新连接...");
            return Task.CompletedTask;
        }
        #endregion

        #region 消息处理
        /// <summary>
        /// 收到消息
        /// </summary>
        /// <param name="clientName"></param>
        /// <param name="message"></param>
        public void ReceiveMessage(string clientName, string message)
        {
            System.Console.WriteLine($"接收到消息-->clientName：{clientName}，-->message：{message}");
        }

        /// <summary>
        /// 消息发送
        /// </summary>
        public void SendMessage(string message)
        {
            try
            {
                connection.InvokeAsync("SendMessage", "dotnet blazor client", message);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 打印连接ID信息
        /// </summary>
        /// <returns></returns>
        public async void ShowConnections()
        {
            try
            {
                var connectIds = await connection.InvokeAsync<List<string>>("GetConnections");
                Console.WriteLine($"已连接ID为：{string.Join("\n\t", connectIds)}");
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
