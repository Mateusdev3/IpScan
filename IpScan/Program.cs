using System;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;

namespace IpScan {
    class Program {
        static async Task Main(string[] args) {
           
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Insira o Ip desejado: ");
            string ip = Console.ReadLine();
            Console.Write("Insira a porta inicial: ");
            int portaini = int.Parse(Console.ReadLine());
            Console.Write("Insira a porta final: ");
            int portafin = int.Parse(Console.ReadLine());
            Console.WriteLine($"Escaneando {ip} em busca de portas abertas.......");
            Console.ForegroundColor = ConsoleColor.White;
            string res = "";
            for (int port = portaini; port <= portafin; port++)
            {
                if (await Scan(ip, port))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"Porta TCP Aberta encontrada : {port}   ");
                    res = res + " " + port;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"TCP fechada  : {port}   ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nEscaneamento concluído.");
            Console.WriteLine($"Portas abertas encontradas:{res} ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadLine();
        }
       static async Task<bool> Scan(string ip, int porta, int time = 50) {
            using (TcpClient tcpClient = new TcpClient())
            {
                try
                {
                    var task = tcpClient.ConnectAsync(ip, porta);
                    if (await Task.WhenAny(task, Task.Delay(time)) == task)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch 
                {
     
                    return false;
                }
            }
        }
        }
    }
