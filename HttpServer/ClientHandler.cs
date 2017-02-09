using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer
{
    class ClientHandler
    {
        private Socket _clientSocket;
        private NetworkStream stream;
        private StreamWriter writer;
        private StreamReader reader;

        public ClientHandler(Socket insocket)
        {
            this._clientSocket = insocket;
        }
        public void Run()
        {

            if (_clientSocket.Connected)
            {
                Console.WriteLine("Client " + _clientSocket.RemoteEndPoint + " loggede på");

                stream = new NetworkStream(_clientSocket);
                writer = new StreamWriter(stream);
                reader = new StreamReader(stream);
                writer.AutoFlush = true;

                // Kommunikér
                //outgoing message
                //sendMessageToClient("Hello " + _clientSocket.RemoteEndPoint + ". How about a nice game of chess?");


                StartDialog();

                // HUSK AT LUKKE WRITER, READER, STREAMS OG SOCKET!
                reader.Close();
                writer.Close();
                stream.Close();

            }

            _clientSocket.Close();

        }

        private void StartDialog()
        {


            // incoming message
            string messageFromKlient = getMessageFromClient();
            Console.WriteLine(messageFromKlient);
            if (messageFromKlient == "GET /service HTTP/1.1")
            {
                string msgToSend = "responsemessage";
                int lenght = msgToSend.Length;
                sendMessageToClient("HTTP / 1.1 200 OK");
                sendMessageToClient("Content-Type: text/plain");
                sendMessageToClient("Content - Length: " + lenght);
                sendMessageToClient("");
                writer.Write(msgToSend);
            }
            if (messageFromKlient == "GET /date HTTP/1.1")
            {
                string msgToSend = DateTime.Now.ToString("dd:MM:yyyy");
                int lenght = msgToSend.Length;
                sendMessageToClient("HTTP / 1.1 200 OK");
                sendMessageToClient("Content-Type: text/plain");
                sendMessageToClient("Content - Length: " + lenght);
                sendMessageToClient("");
                writer.Write(msgToSend);
            }
            if (messageFromKlient == "GET /klokken HTTP/1.1")
            {
                string msgToSend = DateTime.Now.ToString("HH:mm");
                int lenght = msgToSend.Length;
                sendMessageToClient("HTTP / 1.1 200 OK");
                sendMessageToClient("Content-Type: text/plain");
                sendMessageToClient("Content - Length: " + lenght);
                sendMessageToClient("");
                writer.Write(msgToSend);
            }

        }

        private string getMessageFromClient()
        {
            try
            {
                return reader.ReadLine();
            }
            catch
            {
                return null;
            }
        }

        private void sendMessageToClient(string msg)
        {
            writer.WriteLine(msg);
            writer.Flush();
        }
    }
}
