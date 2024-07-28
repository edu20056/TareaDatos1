using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Server
{
    IPHostEntry host;
    IPAddress ipAddr;
    IPEndPoint endPoint;

    Socket server;

    List<Socket> ListaDeSockets = new List<Socket>();

    public Server(string ip, int port) //Crea la ip, endpoint y cantidad de cupos para clientes.
    {
        host = Dns.GetHostEntry(ip);
        ipAddr = host.AddressList[0]; 
        endPoint = new IPEndPoint(ipAddr, port);
        server = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        server.Bind(endPoint);
        server.Listen(5);
    }
    public void StartServer() // Inicia el servidor con Thread para recibir diferentes clientes
    {
        while(true)
        {
            Console.WriteLine("Esperando conexión de cliente....");
            Socket client = server.Accept(); // Acepta la conexión del cliente
            ListaDeSockets.Add(client); 

            Thread hilo = new Thread(ClientConnection);
            hilo.Start(client);
            Console.WriteLine("Un cliente se ha conectado");
        }
    }
    public void ClientConnection(object cli) // Conecta el Socket de entrada para enviar mensaje.
    {
        string message;
        Socket client = (Socket)cli;
        while(true)
        {
            try 
            {
                byte[] buffer = new byte[1024];
                int bytesReceived = client.Receive(buffer); 
                if (bytesReceived >= 0) //Si llegó el mensaje
                {
                    message = BytesAStr(buffer, bytesReceived); 
                    Console.WriteLine("Se recibió el mensaje: " + message); 
                    EnviarMensajeSocket(ListaDeSockets, message, client);
                }
            }
            catch (SocketException)
            {
                Console.WriteLine("Se desconectó un usuario!"); //Se cerró forzosamente la terminar de un usario
                break;
            }
        }
    }

    public void EnviarMensajeSocket(List<Socket> lista, string mensaje, Socket enviador) //Revisa la lista de instancias Socket que se conectaron
    //Esto con el propósito de reenviar este mensajes a todas las intancias menos la que envío el mensaje
    {
        foreach (Socket socket in lista)
        {
            if (socket.Connected && enviador != socket)
            {
                try
                {
                    byte[] data = Encoding.ASCII.GetBytes(mensaje);
                    socket.Send(data);
                    Console.WriteLine("Mensaje enviado.");
                }
                catch (SocketException ex)
                {
                    Console.WriteLine($"Error al enviar mensaje: {ex.Message}");
                }
            }
        }
    }

    public string BytesAStr(byte[] bytes, int length) //Pasa de bytes[] a string
    {
        return Encoding.ASCII.GetString(bytes, 0, length); 
    }
}
