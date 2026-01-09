namespace PublicExample.ZooSystem.ValueObjects
{
    public class Address
    {
        public string Country { get; }
        public string Governorate { get; }
        public string City { get; }
        public string StreetName { get; }
        public short BuildingNumber { get; }


        public Address(string country, string governorate, string city, string streetName, short buildingNumber)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(country, nameof(country));
            ArgumentException.ThrowIfNullOrWhiteSpace(governorate, nameof(governorate));
            ArgumentException.ThrowIfNullOrWhiteSpace(city, nameof(city));
            ArgumentException.ThrowIfNullOrWhiteSpace(streetName, nameof(streetName));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(buildingNumber, nameof(buildingNumber));

            Country = country;
            Governorate = governorate;
            City = city;
            StreetName = streetName;
            BuildingNumber = buildingNumber;
        }
    }
}
