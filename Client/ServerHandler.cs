using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class ServerHandler
    {

        string IPadressString = "webservicedemo.datamatiker-skolen.dk";
        private TcpClient klient = null;
        private NetworkStream stream = null;
        private StreamReader reader = null;
        private StreamWriter writer = null;
        bool running = true;
        public ServerHandler()
        {
            klient = new TcpClient(IPadressString, 80);
            stream = klient.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
            writer.AutoFlush = true;
        }

        public void Run()
        {

            // SEND BESKED TIL SERVER
            string message1 = "GET /RegneWcfService.svc/RESTjson/Add?a=1&b=2 HTTP/1.1";
            string message2 = "Host: webservicedemo.datamatiker-skolen.dk";
            
            writer.WriteLine(message1);
            writer.WriteLine(message2);
            writer.WriteLine();
            writer.Flush();
            
            int contentLength = 0;

            string input;
            do
            {
                input = reader.ReadLine();
                string[] keyValue = input.Split(' ');
                if (keyValue[0]== "Content-Length:")
                {
                    contentLength = int.Parse(keyValue[1]);
                }

            } while (input.Length > 0);
            string resultString = "";

            for (;0 < contentLength; --contentLength)
            {
                char result = (char)reader.Read();
                resultString += result;
               
            }
            Console.WriteLine("Result : < " + resultString + " >");


            //if (tempLine.Contains("Content-Length:"))
            //{
            //    contentLength = int.Parse(tempLine.Substring(15));

            //}

            //ResponseList.Add(tempLine);





            // HUSK AT LUKKE WRITER, READER, STREAM OG SOCKET
            reader.Close();
            writer.Close();
            stream.Close();
            klient.Close();

            Console.ReadKey();
        }
    }
}
