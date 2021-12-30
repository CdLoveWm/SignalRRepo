
using System.Collections.Generic;

namespace SignaRl.Server.IServices
{
    public interface IHubServices
    {
        /// <summary>
        /// 添加客户端
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="clientName"></param>
        public void AddClient(string connectionId, string clientName) { }
        /// <summary>
        /// 移除客户端
        /// </summary>
        /// <param name="connectionId"></param>
        bool RemvoeClient(string connectionId);
        /// <summary>
        /// 获取连接ID信息
        /// </summary>
        /// <returns></returns>
        List<string> GetConnections();
    }
}
