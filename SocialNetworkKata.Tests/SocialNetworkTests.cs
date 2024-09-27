using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace SocialNetworkKata.Tests
{
    public class SocialNetworkTests
    {
        // Posting: Alice can publish messages to a personal timeline
        SocialNetwork _sut;
        public SocialNetworkTests()
        {
            _sut = new SocialNetwork();
        }
        [Fact]
        public void AsAliceShouldPostMessageOnTimeLine()
        {
            string message = "This is a message";
            Person alice = new Person("Alice");
            bool messagePosted = _sut.Post(alice,message);
            Assert.True(messagePosted);
        }

        [Fact]
        public void AsBobShouldReadAlicePostOnTimeLine()
        {
            string message = "This is a message";
            Person alice = new Person("Alice");
            bool messagePosted = _sut.Post(alice, message);
            Assert.True(messagePosted);

            Person bob = new Person("Bob");
            TimeLine aliceTimeLine = _sut.Read(alice);
            Assert.Contains(message,aliceTimeLine.Messages.Select(m => m.Msg));
        }

        [Fact]
        public void AsCharlieShouldSubscribeToAliceAndBobTimeLineAndSeeAggregateListOfAllSubscriptions()
        {
            string aliceMessage = "This is Alice Message";
            string bobMessage = "This is Bob message";
            Person alice = new Person("Alice");
            Person bob = new Person("Bob");
            Person charlie = new Person("Charlie");
            bool messagePosted = _sut.Post(alice, aliceMessage);
            Assert.True(messagePosted);

            
            TimeLine aliceTimeLine = _sut.Read(alice);
            Assert.Contains(aliceMessage, aliceTimeLine.Messages.Select(m => m.Msg));

            
            messagePosted = _sut.Post(bob, bobMessage);
            Assert.True(messagePosted);

            TimeLine bobTimeLine = _sut.Read(bob);

            
            bool subscribedToAliceTimeLine = _sut.Subscribe(charlie, aliceTimeLine);
            Assert.True(subscribedToAliceTimeLine);

            bool subscribedToBobTimeLine =  _sut.Subscribe(charlie, bobTimeLine);
            Assert.True(subscribedToBobTimeLine);

            List<TimeLine> subscriptions = new List<TimeLine>();
            subscriptions = _sut.ListSubscriptions(charlie);
            Assert.Equal(2, subscriptions.Count());
        }
    }
}