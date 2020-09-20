using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _01HTTPServerDemo
{
    class Program
    {
        const string NewLine = "\r\n";

        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;



            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 80);

            tcpListener.Start();

            while (true)
            {
                TcpClient client = await tcpListener.AcceptTcpClientAsync();

                await ProcessClientAsync(client);
            }


        }

        public static async Task ProcessClientAsync(TcpClient client)
        {
            using (var stream = client.GetStream())
            {
                byte[] buffer = new byte[1000000000];

                var lenght = await stream.ReadAsync(buffer, 0, buffer.Length);

                string requestString = Encoding.UTF8.GetString(buffer, 0, lenght);

                Console.WriteLine(requestString);

                string html = $"<h1>Hello from NikiServer {DateTime.Now}</h1>" +
                    $"<form action=/tweet method=post><input name=username /><input name=password />" +
                    $"<input type=submit /></form>";

                string htmlTest = await File.ReadAllTextAsync(@"..\..\..\index.html");



                string response = string.Empty;

                if (requestString.Contains("style.css"))
                {
                    string cssFromFile = await File.ReadAllTextAsync(@"..\..\..\style.css");

                     response = "HTTP/1.1 200 OK" + NewLine +
                    "Server: KitaecaServer 2020" + NewLine +
                    "Content-Type: text/css; charset=utf-8" + NewLine +                   
                    "Content-Lenght: " + cssFromFile.Length + NewLine +
                    NewLine +
                    cssFromFile + NewLine
                    ;
                }
                else
                {
                     response = "HTTP/1.1 200 OK" + NewLine +
                    "Server: KitaecaServer 2020" + NewLine +  
                    "Content-Type: text/html; charset=utf-8" + NewLine +
                    // "Content-Disposition: attachment; filename=kitaeca.txt" + NewLine +
                    //"Content-Lenght: " + html.Length + NewLine +
                    NewLine +
                    htmlTest + NewLine
                    ;
                }



                byte[] responseBytes = Encoding.UTF8.GetBytes(response);

                await stream.WriteAsync(responseBytes);



                Console.WriteLine(new string('=', 70));
            }
        }

        public static async Task ReadData()
        {
            //string url = "https://softuni.bg/courses/csharp-web-basics";
            HttpClient httpClient = new HttpClient();

            //var response = await httpClient.GetAsync(url);
            //Console.WriteLine(response.StatusCode);
            //Console.WriteLine(string.Join(Environment.NewLine,
            //response.Headers.Select(x => x.Key + ": " + x.Value.First())));

            string htmlTest = await File.ReadAllTextAsync(@"..\..\..\index.html");
            Console.WriteLine(htmlTest);
        }
    }
}
