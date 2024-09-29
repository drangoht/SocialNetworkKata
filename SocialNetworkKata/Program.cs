using SocialNetworkKata;

var sn = new SocialNetwork();
var greg = new User("Greg");
var elodie = new User("Elodie");
var bob = new User("Bob");
sn.AddUser(greg);
sn.AddUser(elodie);
sn.AddUser(bob);

sn.Post(greg, "Bonjour, c'est Greg");
sn.Post(elodie, "Hello, c'est Elo");
sn.Post(bob, "Yo, c'est Bob");
sn.Follow(greg, elodie);
sn.Follow(greg, bob);

var subs = sn.ListSubscriptions(greg);
foreach( var tl in subs)
{
    foreach(var msg in tl.Messages)
    {
        Console.WriteLine(msg);
    }
    
}
