using NUnit.Framework;
using Moq;
using System.Collections.Generic;

[TestFixture]
public class MessageBrokerTests
{
    [Test]
    public void Subscribe_AddsMessageHandlerToList()
    {
        // Arrange
        string messageType = "A";
        IMessage messageHandler = Mock.Of<IMessage>();
        MessageBroker messageBroker = new MessageBroker();

        // Act
        messageBroker.Subscribe(messageType, messageHandler);

        // Assert
        List<IMessage> handlers = messageBroker.GetHandlersForType(messageType);
        Assert.Contains(messageHandler, handlers);
    }

    [Test]
    public void Publish_ExecutesMessageHandlersForMessageType()
    {
        // Arrange
        string messageType = "A";
        IMessage messageHandler1 = Mock.Of<IMessage>();
        IMessage messageHandler2 = Mock.Of<IMessage>();
        MessageBroker messageBroker = new MessageBroker();
        messageBroker.Subscribe(messageType, messageHandler1);
        messageBroker.Subscribe(messageType, messageHandler2);

        // Act
        messageBroker.Publish(messageType);

        // Assert
        Mock.Get(messageHandler1).Verify(m => m.Execute(), Times.Once);
        Mock.Get(messageHandler2).Verify(m => m.Execute(), Times.Once);
    }

    [Test]
    public void Publish_DoesNotExecuteMessageHandlersForUnknownMessageType()
    {
        // Arrange
        string messageType = "A";
        IMessage messageHandler = Mock.Of<IMessage>();
        MessageBroker messageBroker = new MessageBroker();
        messageBroker.Subscribe(messageType, messageHandler);

        // Act
        messageBroker.Publish("B");

        // Assert
        Mock.Get(messageHandler).Verify(m => m.Execute(), Times.Never);
    }
}