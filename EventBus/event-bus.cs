using System;
using System.Collections.Generic;

// Подія, яка використовується в системі
public class MessageEvent
{
    public string Message { get; set; }
}

// Клас, який представляє Event Bus
public class EventBus
{
    private readonly Dictionary<Type, List<Action<object>>> _eventHandlers;

    public EventBus()
    {
        _eventHandlers = new Dictionary<Type, List<Action<object>>>();
    }

    public void Publish<T>(T @event)
    {
        Type eventType = typeof(T);

        if (_eventHandlers.ContainsKey(eventType))
        {
            List<Action<object>> handlers = _eventHandlers[eventType];

            foreach (var handler in handlers)
            {
                handler(@event);
            }
        }
    }

    public void Subscribe<T>(Action<T> handler)
    {
        Type eventType = typeof(T);

        if (!_eventHandlers.ContainsKey(eventType))
        {
            _eventHandlers[eventType] = new List<Action<object>>();
        }

        _eventHandlers[eventType].Add(e => handler((T)e));
    }
}

// Компонент, який підписується на подію
public class EventSubscriber
{
    public EventSubscriber(EventBus eventBus)
    {
        eventBus.Subscribe<MessageEvent>(HandleMessageEvent);
    }

    private void HandleMessageEvent(MessageEvent messageEvent)
    {
        Console.WriteLine($"Received message: {messageEvent.Message}");
    }
}

// Компонент, який публікує подію
public class EventPublisher
{
    private readonly EventBus _eventBus;

    public EventPublisher(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void PublishMessage(string message)
    {
        var messageEvent = new MessageEvent { Message = message };
        _eventBus.Publish(messageEvent);
    }
}

// Використання
public class Program
{
    public static void Main()
    {
        var eventBus = new EventBus();
        var subscriber = new EventSubscriber(eventBus);
        var publisher = new EventPublisher(eventBus);

        publisher.PublishMessage("Hello, world!");
    }
}