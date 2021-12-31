using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SignaRl.Server.Hubs;
using SignaRl.Server.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SignaRl.Server.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHubContext<MyHub> _hubContext;

        public HomeController(ILogger<HomeController> logger
            , IHubContext<MyHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// 页面发送消息按钮调用
        /// </summary>
        public async void SendMsg()
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", "Controller Action", $"消息：{DateTime.Now.ToShortDateString()}");
        }
    }
}
