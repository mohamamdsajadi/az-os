using System;
using System.IO;
using System.Text.Json;

struct Person
{
    public string Name { get; set; }
    public int Id { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
}

class Program
{
    private const string FilePath = "person.json";

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Serialize");
            Console.WriteLine("2. Deserialize ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    SerializePerson();
                    break;
                case "2":
                    DeserializePerson();
                    break;
                case "3":
                    return;
            }

            Console.WriteLine();
        }
    }

    static void SerializePerson()
    {
        Console.Write("Name: ");
        string name = Console.ReadLine();

        Console.Write("ID: ");
        int id = int.Parse(Console.ReadLine());

        Console.Write("Email: ");
        string email = Console.ReadLine();

        Console.Write("Address: ");
        string address = Console.ReadLine();

        Person person = new Person
        {
            Name = name,
            Id = id,
            Email = email,
            Address = address
        };

        string json = JsonSerializer.Serialize(person);
        File.WriteAllText(FilePath, json);

        Console.WriteLine("Person serialized successfully");
    }

    static void DeserializePerson()
    {
        string json = File.ReadAllText(FilePath);
        Person person = JsonSerializer.Deserialize<Person>(json);

        Console.WriteLine($"Name: {person.Name}");
        Console.WriteLine($"ID: {person.Id}");
        Console.WriteLine($"Email: {person.Email}");
        Console.WriteLine($"Address: {person.Address}");
    }
}