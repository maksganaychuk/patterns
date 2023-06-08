using NUnit.Framework;
using System.Net.Sockets;
using System.Text;
using System.Threading;

[TestFixture]
public class ServerTests
{
    private const int Port = 8888;
    private Server _server;
    private Thread _serverThread;

    [SetUp]
    public void Setup()
    {
        _server = new Server();
        _serverThread = new Thread(_server.Start);
        _serverThread.Start();
        Thread.Sleep(100); // Дочекатися запуску сервера
    }

    [TearDown]
    public void TearDown()
    {
        _server.Stop();
        _serverThread.Join();
    }

    [Test]
    public void Start_WhenClientConnects_ReceivesRequestAndSendsResponse()
    {
        // Arrange
        using (var client = new TcpClient())
        {
            client.Connect("localhost", Port);
            var stream = client.GetStream();
            var request = "Hello from client!";
            var requestData = Encoding.ASCII.GetBytes(request);

            // Act
            stream.Write(requestData, 0, requestData.Length);

            var responseBytes = new byte[1024];
            var bytesRead = stream.Read(responseBytes, 0, responseBytes.Length);
            var response = Encoding.ASCII.GetString(responseBytes, 0, bytesRead);

            // Assert
            Assert.AreEqual("Hello from server!", response);
        }
    }
}
