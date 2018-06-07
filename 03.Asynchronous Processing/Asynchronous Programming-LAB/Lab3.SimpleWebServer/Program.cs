using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.SimpleWebServer
{
    class Program
    {
        static void Main()
        {
            // Пример за прост Web Server, получаващ Request, принти го на конзолата и отговаря:
            // Но защо принти и "аааааааааааааааааааааааааааааааааааа" в тялото???

            // Create a TcpListener object a start the connection task:
            int port = 1300;
            IPAddress address = IPAddress.Parse("127.0.0.1");
            TcpListener listener = new TcpListener(address, port);
            listener.Start();

            Console.WriteLine($"Server started/");
            Console.WriteLine($"Listening to TCP clients at 127.0.0.1:{port}");

            //var task = Task.Run(() => ConnectWithTcpClient(listener));
            //task.Wait();
            // or
            Task.Run(async () => await ConnectWithTcpClient(listener)).Wait();
        }

        private static async Task ConnectWithTcpClient(TcpListener listener)
        {
            while (true)
            {
                //1. Wait a browser to connect:
                Console.WriteLine("Waiting for client...");
                var client = await listener.AcceptTcpClientAsync();
                Console.WriteLine("Client connected.");

                //2. Read request and print it on the console:
                byte[] request = new byte[1024];
                await client.GetStream().ReadAsync(request, 0, request.Length);

                var message = Encoding.ASCII.GetString(request);
                Console.WriteLine(message);

                //3. Send a greeting to the client:
                byte[] data = Encoding.ASCII.GetBytes("Hello from server!");
                await client.GetStream().WriteAsync(data, 0, data.Length);

                //4. Close the connection:
                Console.WriteLine("Closing connection.");
                client.GetStream().Dispose();
            }

        }
    }
}
