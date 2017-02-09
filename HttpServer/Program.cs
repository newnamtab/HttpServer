using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HttpServer
{
    class Program
    {
        private TcpListener server = null;

        static void Main(string[] args)
        {
            Program myProgram = new Program();
            myProgram.Run();
        }
        public void Run()
        {

            server = new TcpListener(IPAddress.Any, 20001);
            server.Start();
            

            Console.WriteLine("********** SERVER STARTED **********");
            Listeners();

        }
        private void Listeners()
        {

            int acceptClients = 0;

            while (acceptClients <= 5)
            {
                Socket clientSocket = server.AcceptSocket();
                if (clientSocket.Connected)
                {
                    ClientHandler handler = new ClientHandler(clientSocket);
                    handler.Run();
                }
                //Thread clientTråd = new Thread(handler.Run);
                //    clientTråd.Start();
                //    acceptClients++;

               

            }

        }
    }
}
