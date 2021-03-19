using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleTcp;

namespace JBChatServer
{
    public class Commands
    {
        static public string HelpMessage()
        {
            string helpMessage = "----------------------------Commands----------------------------\nclear - Clears the console\nend - Shuts down the server\nbroadcast - Send a message to everyone connected on the server";
            return helpMessage;
        }
    }
}
