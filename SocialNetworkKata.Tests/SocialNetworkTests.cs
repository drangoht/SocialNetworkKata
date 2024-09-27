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
        [Theory]
        [InlineData("This is a message")]
        public void AsAliceShouldPostMessageOnTimeLine(string message)
        {
            Person alice = new Person("Alice");
            bool messagePosted = _sut.Post(alice,message);
            Assert.True(messagePosted);
        }

        [Theory]
        [InlineData("This is a message")]
        public void AsBobShouldShouldReadAlicePostOnTimeLine(string message)
        {
            Person alice = new Person("Alice");
            bool messagePosted = _sut.Post(alice, message);
            Assert.True(messagePosted);

            Person bob = new Person("Bob");
            TimeLine aliceTimeLine = _sut.Read(alice);
            Assert.Contains(message,aliceTimeLine.Messages.Select(m => m.Msg));
        }
    }
}