using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        string puerto; //Estas primeras lines toman el parámetro de entrada y si sí existe el puerto será este, 
        //sino esperará a que se escriba el puerto
        if (args.Length > 0)
        {
            puerto = args[0];
        }
        else
        {
            puerto = Console.ReadLine();
        }

        int number = Math.Abs(int.Parse(puerto)); //Se pasa de string a int y el valor absoluto es porque se indicó que se debí poner el
        // puerto de la forma "-puerto"

        IPHostEntry identificador = Dns.GetHostEntry("localhost");
        IPAddress ipIdentificador = identificador.AddressList[0];

        Colocador(number, ipIdentificador);
    }

    static bool PuertoEnUso(int port, IPAddress x) //Revisa si en el puerto de entrada hay un servidor presente
    {
        bool inUse = false;
        try
        {
            TcpListener tcpListener = new TcpListener(x, port);
            tcpListener.Start();
            Console.WriteLine("Se conecta a puerto: {0}", port);
            tcpListener.Stop();
        }
        catch (SocketException)
        {
            inUse = true;
        }
        return inUse;
    }

    static void Colocador(int number, IPAddress ipIdentificador) //Si no hay servidor lo crea, por otra parte si hay servidor crea cliente.
    {
        if (PuertoEnUso(number, ipIdentificador))
        {
            Console.WriteLine("Se inicia como cliente.");
            Cliente cliente = new Cliente("localhost", number);
            cliente.StartClient();

            // Iniciar el hilo de recepción una vez fuera del bucle
            Thread receiveThread = new Thread(cliente.Receive);
            receiveThread.Start();

            while (true)
            {
                string texto = Console.ReadLine();
                cliente.Send(texto);
            }
        }
        else
        {
            Console.WriteLine("Se inicia como servidor.");
            Server servidor = new Server("localhost", number);
            servidor.StartServer();
        }
    }
}
