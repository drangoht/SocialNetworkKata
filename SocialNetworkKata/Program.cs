using SocialNetworkKata;

var sn = new SocialNetwork();
var greg = new User("Greg");
var elodie = new User("Elodie");
var bob = new User("Bob");
sn.AddUser(greg);
sn.AddUser(elodie);
sn.AddUser(bob);

sn.Post(new Message(greg,"Bonjour, c'est Greg"));
sn.Post( new Message(greg, "Hello, c'est Elo"));
sn.Post( new Message(bob,"Yo, c'est Bob"));
sn.Post( new Message(bob,"Comment allez-vous @Elodie et @Greg"));
var messageWithLink = sn.Post(new Message(greg,"This is Bob message <a href=\"https:\\\\google.fr\"></a>"));
sn.Follow(greg, elodie);
sn.Follow(greg, bob);

sn.DirectMessage(elodie, new Message(greg, "This is a secret"));
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
    Console.WriteLine($"{greg.Name} mentionné par:{mention.Owner.Name} dans le message {mention.Text}");
}
foreach(var link in messageWithLink.Links)
{
    Console.WriteLine($"lien : {link}");
}
foreach(var pm in elodie.PrivateMessages)
{
    Console.WriteLine($"pm for Elodie User: {pm.Owner.Name} Msg: {pm.Text}");
}