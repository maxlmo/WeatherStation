using Moq;
using Prism.Events;

namespace WeatherStation.Test.MockExtensions
{
    public static class EventAggregatorMockExtensions
    {
        public static Mock<IEventAggregator> ReturnsEvent<TEventType>(this Mock<IEventAggregator> mock, TEventType eventType) 
            where TEventType : EventBase, new()
        {
            mock.Setup(m => m.GetEvent<TEventType>()).Returns(eventType);
            return mock;
        }
    }
}