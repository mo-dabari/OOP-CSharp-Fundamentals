namespace Composition.Exercises
{
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }

        public Address(string street, string city)
        {
            Street = street;
            City = city;
        }

        public override string ToString() => $"{Street}, {City}";
    }

    public class UniversityStudent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        private Address address;  // Composition

        public UniversityStudent(int id, string name, Address addr)
        {
            Id = id;
            Name = name;
            address = addr;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"الطالب: {Name} (ID: {Id})");
            Console.WriteLine($"العنوان: {address}");
        }
    }
}
