using NUnit.Framework;

[TestFixture]
public class PeerNodeTests
{
    [Test]
    public void Connect_TwoPeers_PeersAreConnected()
    {
        // Arrange
        var peerA = new PeerNode("Peer A");
        var peerB = new PeerNode("Peer B");

        // Act
        peerA.Connect(peerB);

        // Assert
        CollectionAssert.Contains(peerA.GetConnectedPeers(), peerB);
        CollectionAssert.Contains(peerB.GetConnectedPeers(), peerA);
    }

    [Test]
    public void SendMessage_ValidInput_SendsMessageToConnectedPeers()
    {
        // Arrange
        var peerA = new PeerNode("Peer A");
        var peerB = new PeerNode("Peer B");
        var peerC = new PeerNode("Peer C");
        peerA.Connect(peerB);
        peerA.Connect(peerC);
        peerB.Connect(peerC);
        string message = "Hello from Peer A";

        // Act
        peerA.SendMessage(message);

        // Assert
        // Assert that each connected peer received the message
        AssertConsoleOutputContains(peerB, message);
        AssertConsoleOutputContains(peerC, message);
    }

    // Helper method to assert that the console output contains a specific message
    private void AssertConsoleOutputContains(PeerNode peer, string message)
    {
        StringAssert.Contains(message, GetConsoleOutput(peer));
    }

    // Helper method to get the console output produced by a peer
    private string GetConsoleOutput(PeerNode peer)
    {
        var consoleWriter = new ConsoleOutputWriter();
        Console.SetOut(consoleWriter);
        peer.SendMessage(""); // Trigger console output
        return consoleWriter.GetOutput();
    }
}

// Helper class to capture the console output
public class ConsoleOutputWriter : StringWriter
{
    private readonly StringBuilder _output = new StringBuilder();

    public override void WriteLine(string value)
    {
        _output.AppendLine(value);
    }

    public string GetOutput()
    {
        return _output.ToString().Trim();
    }
}