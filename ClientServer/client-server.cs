// Server
// ------

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class Server
{
    private TcpListener _listener;
    private bool _isRunning;

    public void Start()
    {
        _listener = new TcpListener(IPAddress.Any, 8888);
        _listener.Start();
        _isRunning = true;

        Console.WriteLine("Server started. Waiting for connections...");

        while (_isRunning)
        {
            var client = _listener.AcceptTcpClient();
            var thread = new Thread(HandleClient);
            thread.Start(client);
        }
    }

    public void Stop()
    {
        _isRunning = false;
        _listener.Stop();
    }

    private void HandleClient(object clientObj)
    {
        var client = (TcpClient)clientObj;

        var stream = client.GetStream();

        var data = new byte[1024];
        var bytesRead = stream.Read(data, 0, data.Length);
        var request = Encoding.ASCII.GetString(data, 0, bytesRead);
        Console.WriteLine("Received request: " + request);

        var response = "Hello from server!";
        var responseData = Encoding.ASCII.GetBytes(response);
        stream.Write(responseData, 0, responseData.Length);
        Console.WriteLine("Sent response: " + response);

        client.Close();
    }
}

// Client
// ------

using System;
using System.Net.Sockets;
using System.Text;

public class Client
{
    public void Start()
    {
        var client = new TcpClient("localhost", 8888);
        var stream = client.GetStream();

        var request = "Hello from client!";
        var requestData = Encoding.ASCII.GetBytes(request);
        stream.Write(requestData, 0, requestData.Length);
        Console.WriteLine("Sent request: " + request);

        var data = new byte[1024];
        var bytesRead = stream.Read(data, 0, data.Length);
        var response = Encoding.ASCII.GetString(data, 0, bytesRead);
        Console.WriteLine("Received response: " + response);

        client.Close();
    }
}

// Usage
// -----

public class Program
{
    public static void Main()
    {
        var server = new Server();

        var serverThread = new Thread(server.Start);
        serverThread.Start();

        var client = new Client();
        client.Start();

        server.Stop();
        serverThread.Join();
    }
}