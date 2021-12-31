using System;

namespace SignalR.Client.Dotnet.ConsoleClient
{
    internal class Program
    {
        static void Main(string[] args)
        {

            SignalRService signalRService = new SignalRService();
            while (true)
            {
                string message = Console.ReadLine();
                // 这只是为了调用不同的方法做的区分，没有任何实质意义
                switch (message)
                {
                    case "id":
                        signalRService.ShowConnections();
                        break;
                    default:
                        signalRService.SendMessage(message);
                        break;
                }
            }
        }
    }
}
