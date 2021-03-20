using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using SimpleTcp;
using JBChatServer.ServerAPI;
using JBAddons;
using JB.JBIni;

namespace JBChatServer
{
    class Program
    {
        static IniFile ini = new IniFile("ServerSettings.ini");

        static string PORT = ini.Read("Port", "General");
        static string IP = ini.Read("IP", "General");
        static SimpleTcpServer server = new SimpleTcpServer(IP + ":" + PORT);

        static int connectedClients = 0;
        static int maxClients = ini.ReadInt("MaxClients", "General");

        static int CheckCommand(string command)
        {
            switch (command)
            {
                case "clear":
                    Console.Clear();
                    Console.WriteLine("Started server, on IP " + IP + " on port " + PORT);
                    return 1;
                case "end":
                    JBConsole.ColoredMessage(ConsoleColor.Yellow, "Are you sure? yes/no");
                    string confirm = Console.ReadLine();
                    if(confirm == "yes")
                    {
                        JBConsole.ColoredMessage(ConsoleColor.Red, "Endding Server...");
                        server.Dispose();
                        return 2;
                    }
                    else if(confirm == "no")
                    {
                        return -2;
                    }
                    return -2;
                case "broadcast":
                    Console.WriteLine("What do you want to send");
                    string message = Console.ReadLine();
                    JBMessages.BroadcastAsync(server, message);
                    return 3;
                case "stats":
                    SimpleTcpStatistics statistics = new SimpleTcpStatistics();
                    Console.WriteLine(statistics.ToString());
                    return 4;
                case "help":
                    JBConsole.ColoredMessage(ConsoleColor.Cyan, Commands.HelpMessage());
                    return 5;
                default:
                    JBConsole.ColoredMessage(ConsoleColor.Yellow, "Unkown command!");
                    return 0;
            }
        }

        static void Main(string[] args)
        {
            // set events
            server.Events.ClientConnected += ClientConnected;
            server.Events.ClientDisconnected += ClientDisconnected;
            server.Events.DataReceived += DataReceived;

            // let's go!
            server.Start();
            Console.WriteLine("Started server, on IP " + IP + " on port " + PORT);

            while (true)
            {
                Console.WriteLine("Please type command: ");
                var message = Console.ReadLine();
                if(CheckCommand(message) == 2)
                {
                    break;
                }

                Thread.Sleep(500);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void ClientConnected(object sender, ClientConnectedEventArgs e)
        {
            connectedClients += 1;
            if(connectedClients>=maxClients)
            {
                server.SendAsync(e.IpPort, "This server is full!");
                server.DisconnectClient(e.IpPort);
            }
            else
            {
                Console.WriteLine("[" + e.IpPort + "] client connected");
            }
            
        }

        static void ClientDisconnected(object sender, ClientDisconnectedEventArgs e)
        {
            
            Console.WriteLine("[" + e.IpPort + "] client disconnected: " + e.Reason.ToString());
        }

        static void DataReceived(object sender, DataReceivedEventArgs e)
        {
            string clientSending = e.IpPort;
            Console.WriteLine("[" + e.IpPort + "] says: " + Encoding.UTF8.GetString(e.Data));
            IEnumerable<string> clients = server.GetClients();
            foreach (string client in clients)
            {
                server.SendAsync(client, "[" + clientSending + "]" + " says: " + Encoding.UTF8.GetString(e.Data));
            }
        }
    }
}
