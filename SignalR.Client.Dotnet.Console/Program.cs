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
