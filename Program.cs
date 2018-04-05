using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UdpTest
{
    class UDP
    {
        UdpClient udp;
        Thread thread;

        public void Start(string port)
        {
            udp = new UdpClient(int.Parse(port));
            thread = new Thread(new ThreadStart(this.ThreadMethod));
            thread.Start();
        }

        public void Close()
        {
            udp.Close();
            thread.Abort();
        }

        public void SendMessage(string ipWithPort, string message)
        {
            var splited = ipWithPort.Split(':');

            var data = Encoding.UTF8.GetBytes(message);

            try
            {
                udp.Send(data, data.Length, splited[0], int.Parse(splited[1]));
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void ThreadMethod()
        {
            while (true)
            {
                IPEndPoint remoteEP = null;
                try
                {
                    byte[] data = udp.Receive(ref remoteEP);
                    string text = Encoding.ASCII.GetString(data);
                    Console.WriteLine(text);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
    class Program
    {
        //static void Main(string[] args)
        //{
        //    var s = new UDP();
        //    s.Start("25565");

        //    var s2 = new UDP();
        //    s2.Start("0");
        //    s2.SendMessage("127.0.0.1:25563", "hogehoge");
        //    s2.SendMessage("127.0.0.1:25565", "hogehoge");

        //    for(int i = 0; i < 2; ++i)
        //    {
        //        Thread.Sleep(1000);
        //        s2.SendMessage("127.0.0.1:25565", "fuga");
        //    }

        //    s.Close();

        //    Thread.Sleep(1000);
        //    s2.SendMessage("127.0.0.1:25565", "fuga");


        //    Thread.Sleep(1000);
        //    s.Start("25565");

        //    Thread.Sleep(1000);
        //    s2.SendMessage("127.0.0.1:25565", "sleep");

        //    Thread.Sleep(1000);

        //    s.Close();
        //    s2.Close();
        //}

        static void Main(string[] args)
        {
            var s = new UDP();
            s.Start("25565");

            while(true)
            {
                Thread.Sleep(1000);
            }
        }
    }
}
