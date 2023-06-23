using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace ClientTCP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string ip = "127.0.0.1";
            const int port = 8080;

            try
            {
                SendMessage(port, ip);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally 
            {
                Console.WriteLine();
            }

        }

        static void SendMessage(int port, string ip)
        {
            

            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            Console.WriteLine("Enter the message:");
            var message = Console.ReadLine();

            var data = Encoding.UTF8.GetBytes(message);

            tcpSocket.Connect(tcpEndPoint);
            tcpSocket.Send(data);

            var buffer = new byte[256];
            var size = 0;


            size = tcpSocket.Receive(buffer);
            Console.WriteLine(Encoding.UTF8.GetString(buffer, 0, size));

            if (message.IndexOf(">>End") == -1)
                SendMessage(port, ip);


            tcpSocket.Shutdown(SocketShutdown.Both);
            tcpSocket.Close();
        }
    }
}
