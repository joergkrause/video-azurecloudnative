using System.Net;
using System.Net.Sockets;
using WebSocketSharp;
using System.Text;
using System.Text.RegularExpressions;
using WebSocketSharp.Server;

namespace Heise.Course.SocketServerTest;

class Server {

  public class EchoSocketServer : WebSocketBehavior {
    protected override void OnMessage(MessageEventArgs e) {
      Console.WriteLine($"Received a message: {e.Data}");
      var msg = e.Data;
      Send(msg);
      Console.WriteLine("Sent back");
    }
  }

  public class DataSocketServer : WebSocketBehavior {

    protected override void OnClose(CloseEventArgs e) {
      base.OnClose(e);
    }

  }

  public static void Main() {

    Console.WriteLine("Socket Server Test");
    var wssv = new WebSocketServer("ws://127.0.0.1:8080");
    wssv.AddWebSocketService<EchoSocketServer>("/Echo");
    Console.WriteLine("Start");
    wssv.Start();
    Console.WriteLine("Started");
    Console.ReadKey(true);
    wssv.Stop();
  }

}