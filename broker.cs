using System;
using System.Collections.Generic;

// Інтерфейс повідомлення
public interface IMessage
{
    void Execute();
}

// Конкретне повідомлення
public class ConcreteMessageA : IMessage
{
    public void Execute()
    {
        Console.WriteLine("Executing ConcreteMessageA");
    }
}

// Інший конкретний тип повідомлення
public class ConcreteMessageB : IMessage
{
    public void Execute()
    {
        Console.WriteLine("Executing ConcreteMessageB");
    }
}

// Брокер
public class MessageBroker
{
    private readonly Dictionary<string, List<IMessage>> _messageHandlers;

    public MessageBroker()
    {
        _messageHandlers = new Dictionary<string, List<IMessage>>();
    }

    public void Subscribe(string messageType, IMessage messageHandler)
    {
        if (!_messageHandlers.ContainsKey(messageType))
        {
            _messageHandlers[messageType] = new List<IMessage>();
        }

        _messageHandlers[messageType].Add(messageHandler);
    }

    public void Publish(string messageType)
    {
        if (_messageHandlers.ContainsKey(messageType))
        {
            foreach (var messageHandler in _messageHandlers[messageType])
            {
                messageHandler.Execute();
            }
        }
    }
}

// Використання
public class Program
{
    public static void Main()
    {
        // Створення брокера
        var messageBroker = new MessageBroker();

        // Створення повідомлень
        var messageA1 = new ConcreteMessageA();
        var messageA2 = new ConcreteMessageA();
        var messageB1 = new ConcreteMessageB();

        // Підписка на повідомлення типу "A"
        messageBroker.Subscribe("A", messageA1);
        messageBroker.Subscribe("A", messageA2);

        // Підписка на повідомлення типу "B"
        messageBroker.Subscribe("B", messageB1);

        // Публікація повідомлень
        messageBroker.Publish("A");
        messageBroker.Publish("B");
    }
}