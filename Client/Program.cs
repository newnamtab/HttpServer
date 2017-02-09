using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Program myprogram = new Program();
            myprogram.Run();
        }

        public void Run()
        {
            ServerHandler Server = new ServerHandler();
            Server.Run();
        }
    }
}
