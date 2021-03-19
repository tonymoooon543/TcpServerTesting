using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleTcp;

namespace JBChatServer.ServerAPI
{
    public class JBMessages
    {
        public static bool Broadcast(SimpleTcpServer server, string message, DataReceivedEventArgs e)
        {
            if(message == string.Empty && server == null)
            {
                return false;
            }
            else
            {
                IEnumerable<string> clients = server.GetClients();
                foreach (string client in clients)
                {
                    server.Send(client, message);
                }
                return true;
            }
        }

        public static bool BroadcastAsync(SimpleTcpServer server, string message)
        {
            if (message == string.Empty && server == null)
            {
                return false;
            }
            else
            {
                IEnumerable<string> clients = server.GetClients();
                foreach (string client in clients)
                {
                    server.SendAsync(client, message);
                }
                return true;
            }
        }
    }
}
