using PublicExample.ZooSystem.ValueObjects;
using PublicExamples.ZooSystem.AbstractClasses;

namespace PublicExamples.ZooSystem
{
    public class Zoo
    {
        private List<Enclosure> _enclosures;

        public string Name { get; }
        public double GardenArea { get; }

        public Address Address { get; }
        public IReadOnlyList<Enclosure> Enclosures;

        public Zoo(string name, double gardenArea, Address address)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(gardenArea, nameof(gardenArea));
            ArgumentNullException.ThrowIfNull(address, nameof(address));

            Name = name;
            GardenArea = gardenArea;
            Address = address;
            _enclosures = new();
            Enclosures = _enclosures.AsReadOnly();
        }

        public void AddEnclosure(Enclosure enclosure)
        {
            ArgumentNullException.ThrowIfNull(enclosure, nameof(enclosure));
            _enclosures.Add(enclosure);

        }

        public void RemoveEnclosure(Enclosure enclosure)
        {
            ArgumentNullException.ThrowIfNull(enclosure, nameof(enclosure));

            if (_enclosures.Count == 0)

                throw new InvalidOperationException("There is no enclosure to remove it from these containers.");


            _enclosures.Remove(enclosure);

        }
    }
}
