using SignaRl.Server.IServices;
using System.Collections.Generic;
using System.Linq;

namespace SignaRl.Server.Services
{
    public class HubServices : IHubServices
    {
        private static Dictionary<string, string> clients = new Dictionary<string, string>();
        /// <summary>
        /// 添加客户端
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="clientName"></param>
        public void AddClient(string connectionId, string clientName)
        {
            if(!clients.ContainsKey(connectionId))
                clients.Add(connectionId, clientName);
        }
        /// <summary>
        /// 移除客户端
        /// </summary>
        /// <param name="connectionId"></param>
        public bool RemvoeClient(string connectionId)
        {
            if (!clients.ContainsKey(connectionId)) return false;
            clients.Remove(connectionId);
            return true;
        }
        /// <summary>
        /// 获取连接ID信息
        /// </summary>
        /// <returns></returns>
        public List<string> GetConnections()
        {
            return clients.Keys.ToList();
        }
    }
}
