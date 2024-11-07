using System.IO.Pipes;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.VisualBasic;


namespace lesson1;

internal class Chat
{
    public static void Server()
    {
        IPEndPoint localEP = new IPEndPoint(IPAddress.Any, 0);
        UdpClient udp = new UdpClient(13245);
        System.Console.WriteLine("Server wait message from client");

        while(true)
        {
            try
            {
                byte[] buffer = udp.Receive(ref localEP);
                string str1 = Encoding.UTF8.GetString(buffer);

                Message? message = Message.FromJson(str1);
                if(message != null)
                {
                    System.Console.WriteLine(message.ToString());
                    Message newmessage = new Message("Server", "Message is received");
                    string js = newmessage.ToJson();
                    byte[] bytes = Encoding.UTF8.GetBytes(js);
                    udp.Send(bytes, localEP);
                }
                else
                    System.Console.WriteLine("Uncorrect message");

            }
            catch(Exception e) {System.Console.WriteLine(e.Message);}
        }
    }
    public static void Client(string nickname)
    {
        IPEndPoint localEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);
        UdpClient udp = new UdpClient();

        while(true)
        {
            System.Console.WriteLine("Enter message");
            string? text = Console.ReadLine();
            if(string.IsNullOrEmpty(text))
                break;
            Message newmessage = new Message(nickname, text);
            string js = newmessage.ToJson();
            byte[] bytes = Encoding.UTF8.GetBytes(js);
            udp.Send(bytes, localEP);

            byte[] buffer = udp.Receive(ref localEP);
            string str1 = Encoding.UTF8.GetString(buffer);
            Message? message = Message.FromJson(str1);
            System.Console.WriteLine(message);
        }
    }
}