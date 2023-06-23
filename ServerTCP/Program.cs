using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerTCP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string ip = "127.0.0.1";
            const int port = 8080;

            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
            try
            {
                tcpSocket.Bind(tcpEndPoint);
                tcpSocket.Listen(5);

                while (true)
                {
                    var listener = tcpSocket.Accept();
                    var buffer = new byte[256];
                    var size = 0;
                    string data = null;

                    size = listener.Receive(buffer);

                    data += Encoding.UTF8.GetString(buffer, 0, size);

                    Console.WriteLine(data);

                    listener.Send(Encoding.UTF8.GetBytes("Success"));

                    if (data.IndexOf(">>End") > 1)
                    {
                        Console.WriteLine("Server shutdowned");
                        break;
                    }

                    listener.Shutdown(SocketShutdown.Both);
                    listener.Close();
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}
