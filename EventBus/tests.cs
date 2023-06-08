using System;
using Xunit;

namespace EventBus.Tests
{
    public class EventBusTests
    {
        [Fact]
        public void EventPublisher_PublishMessage_SubscriberReceivesMessage()
        {
            // Arrange
            var eventBus = new EventBus();
            var subscriber = new EventSubscriber(eventBus);
            var publisher = new EventPublisher(eventBus);
            var receivedMessage = "";

            // Act
            subscriber.MessageReceived += (sender, args) =>
            {
                receivedMessage = args.Message;
            };

            publisher.PublishMessage("Hello, world!");

            // Assert
            Assert.Equal("Hello, world!", receivedMessage);
        }

        [Fact]
        public void EventPublisher_PublishMessage_NoSubscribers_NoExceptions()
        {
            // Arrange
            var eventBus = new EventBus();
            var publisher = new EventPublisher(eventBus);

            // Act and Assert (no exceptions should be thrown)
            publisher.PublishMessage("Hello, world!");
        }
    }
}