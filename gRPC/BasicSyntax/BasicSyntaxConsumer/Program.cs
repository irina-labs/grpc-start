// See https://aka.ms/new-console-template for more information
using Google.Protobuf.WellKnownTypes;
using MyExample.AddressBook;

Console.WriteLine("Hello, World!");
var addressbook = new AddressBook();

addressbook.People.Add(new Person()
{
    Name = "John Doe",
    Id = 1,
    Email = "",
    LastUpdated = Timestamp.FromDateTime(DateTime.UtcNow),
    Phones = {
        new Person.Types.PhoneNumber() {
        Number = "123456789",
        Type = Person.Types.PhoneType.Mobile
        } }

});
addressbook.People.Add(new Person() { Name = "John Doe", Id = 2, Email = "" });