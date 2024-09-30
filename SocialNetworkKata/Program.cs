using SocialNetworkKata;

var sn = new SocialNetwork();
var greg = new User("Greg");
var elodie = new User("Elodie");
var bob = new User("Bob");
sn.AddUser(greg);
sn.AddUser(elodie);
sn.AddUser(bob);

sn.Post(greg, new Message("Bonjour, c'est Greg"));
sn.Post(elodie, new Message("Hello, c'est Elo"));
sn.Post(bob, new Message("Yo, c'est Bob"));
sn.Post(bob, new Message("Comment allez-vous @Elodie et @Greg"));
var messageWithLink = sn.Post(greg, new Message("This is Bob message <a href=\"https:\\\\google.fr\"></a>"));
sn.Follow(greg, elodie);
sn.Follow(greg, bob);

sn.DirectMessage(greg, elodie, new Message("This is a secret"));
var subs = sn.ListSubscriptions(greg);
foreach( var tl in subs)
{
    foreach(var msg in tl.Messages)
    {
        Console.WriteLine(msg.Text);
    }
    
}
foreach( var mention in greg.Mentions)
{
    Console.WriteLine($"{greg.Name} mentionné par:{mention.user.Name} dans le message {mention.message.Text}");
}
foreach(var link in messageWithLink.Links)
{
    Console.WriteLine($"lien : {link}");
}
foreach(var pm in elodie.PrivateMessages)
{
    Console.WriteLine($"pm for Elodie User: {pm.MessageSender.Name} Msg: {pm.Message.Text}");
}