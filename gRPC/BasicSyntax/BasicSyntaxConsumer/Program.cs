// See https://aka.ms/new-console-template for more information



using Google.Protobuf.WellKnownTypes;
using MyExample.AddressBook;
using static MyExample.AddressBook.Person.Types;
Console.WriteLine("Hello!");

var addressbook = new AddressBook();


addressbook.People.Add(new Person()
{
    Name = "John Doe",
    Id = 1,
    Email = "",
    LastUpdated = Timestamp.FromDateTime(DateTime.UtcNow),
    Phones = {
        new PhoneNumber() {
        Number = "123456789",
        Type = PhoneType.Mobile
        }
    }

});


addressbook.People.Add(new Person() { Name = "John Doe", Id = 2, Email = "" });
Console.WriteLine(addressbook);