using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace SocialNetworkKata.Tests
{
    public class SocialNetworkTests
    {
        // Posting: Alice can publish messages to a personal timeline
        SocialNetwork _sut;
        User _alice;
        User _bob ;
        User _charlie;
        User _mallory;
        public SocialNetworkTests()
        {
            _sut = new SocialNetwork();
            _alice = new User("Alice");
            _bob = new User("Bob");
            _charlie = new User("Charlie");
            _mallory = new User("Mallory");
            _sut.AddUser(_alice);
            _sut.AddUser(_bob);
            _sut.AddUser(_charlie);
            _sut.AddUser(_mallory);
        }
        [Fact]
        public void AsAliceShouldPostMessageOnTimeLine()
        {
            string message = "This is a message";
            Message messagePosted = _sut.Post(new Message(_alice,message));
            Assert.NotEmpty(messagePosted.Text);
        }

        [Fact]
        public void AsBobShouldReadAlicePostOnTimeLine()
        {
            string message = "This is a message";
            
            Message messagePosted = _sut.Post(new Message(_alice,message));
            Assert.NotEmpty(messagePosted.Text);

            _sut.AddUser(_bob);
            TimeLine aliceTimeLine = _sut.Read(_alice);
            Assert.Contains(message,aliceTimeLine.Messages.Select(m => m.Text));
        }
        [Fact]
        public void AsCharlieShouldSubscribeToAliceAndBobTimeLineAndSeeAggregateListOfAllSubscriptions()
        {
            string aliceMessage = "This is Alice Message";
            string bobMessage = "This is Bob message";
            Message messagePosted = _sut.Post(new Message(_alice,aliceMessage));
            Assert.NotEmpty(messagePosted.Text);

            messagePosted = _sut.Post(new Message(_bob,bobMessage));
            Assert.NotEmpty(messagePosted.Text);


            bool subscribedToAliceTimeLine = _sut.Follow(_charlie, _alice);
            Assert.True(subscribedToAliceTimeLine);

            bool subscribedToBobTimeLine = _sut.Follow(_charlie, _bob);
            Assert.True(subscribedToBobTimeLine);

            List<TimeLine> subscriptions = new List<TimeLine>();
            subscriptions = _sut.ListSubscriptions(_charlie);
            Assert.Equal(2, subscriptions.Count());
        }

        [Fact]
        public void AsBobShouldMentionCharlie()
        {
            string bobMessage = "This is Bob message to @Charlie";
            _sut.Post(new Message(_charlie,"testetette"));
            Message messagePosted = _sut.Post(new Message(_bob, bobMessage));
            Assert.NotEmpty(messagePosted.Text);

            Assert.Single(_charlie.Mentions);
        }

        [Fact]
        public void AsBobShouldAddClickableLinksToMessage()
        {
            string bobMessage = "This is Bob message <a href=\"https:\\\\google.fr\"></a>";
            Message messagePosted = _sut.Post(new Message(_bob, bobMessage));
            Assert.NotEmpty(messagePosted.Text);

            Assert.Single(messagePosted.Links);
        }

        [Fact]
        public void AsMalloryShouldSendPrivateMessageToAlice()
        {
            string malloryMessage = "Hi Alice, this is private between us";
             _sut.DirectMessage(_alice, new Message(_mallory,malloryMessage));
            Assert.Single(_alice.PrivateMessages);
        }
    }
}