using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
class Cliente
{
    IPHostEntry host;
    IPAddress ipAddr;
    IPEndPoint endPoint;
    Socket client;
    public Cliente(string ip, int port)
    {
        host = Dns.GetHostEntry(ip);
        ipAddr = host.AddressList[0];
        endPoint = new IPEndPoint(ipAddr, port);
        client = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
    }
    public void StartClient()
    {
        client.Connect(endPoint);
    }
    public void Send(string msg)
    {
        byte[] bytesMSG = Encoding.ASCII.GetBytes(msg);
        client.Send(bytesMSG);
    }
    public string BytesAStr(byte[] x)
    {
        string m = Encoding.ASCII.GetString(x); 
        return m;
    }
    public byte[] StrABytes(string x)
    {
        byte[] n  = Encoding.ASCII.GetBytes(x);
        return n;
    }
    public string Recibo()
    {
        byte[] buffer = new byte[1024];
        return BytesAStr(buffer);
    }
    public void Receive()
    {
        while(true) 
        {
            try
            {
                byte[] buffer = new byte[1024];
                int bytesReceived = client.Receive(buffer);
                Console.WriteLine(Encoding.ASCII.GetString(buffer, 0, bytesReceived));
                Console.Out.Flush();
            }
            catch (Exception)
            {
                Console.WriteLine("Error!");
                break;
            }
        }
    }
}