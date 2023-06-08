using System;
using System.Collections.Generic;

// Клас, що представляє вузол (пір) у мережі P2P
public class PeerNode
{
    public string Name { get; }
    private readonly List<PeerNode> _connectedPeers;

    public PeerNode(string name)
    {
        Name = name;
        _connectedPeers = new List<PeerNode>();
    }

    public void Connect(PeerNode peer)
    {
        _connectedPeers.Add(peer);
        peer._connectedPeers.Add(this);
    }

    public void SendMessage(string message)
    {
        Console.WriteLine($"{Name}: Sending message '{message}'");

        // Відправити повідомлення всім підключеним вузлам
        foreach (var peer in _connectedPeers)
        {
            peer.ReceiveMessage(Name, message);
        }
    }

    public void ReceiveMessage(string sender, string message)
    {
        Console.WriteLine($"{Name}: Received message '{message}' from {sender}");
    }
}

// Використання
public class Program
{
    public static void Main()
    {
        // Створення вузлів (пір)
        var peerA = new PeerNode("Peer A");
        var peerB = new PeerNode("Peer B");
        var peerC = new PeerNode("Peer C");

        // Встановлення з'єднань між вузлами
        peerA.Connect(peerB);
        peerB.Connect(peerC);

        // Відправка повідомлень
        peerA.SendMessage("Hello from Peer A");
        peerC.SendMessage("Greetings from Peer C");
    }
}