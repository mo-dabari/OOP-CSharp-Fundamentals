namespace PublicExamples.ZooSystem.AbstractClasses
{
    public abstract class Enclosure
    {
        private List<Animal> _animals;
        public string Name { get; }

        public double Width { get; }
        public double Hight { get; }
        public double Size { get; }
        public bool IsOpen { get; }
        public bool HasRoof { get; }

        public IReadOnlyList<Animal> Animals { get; }

        public Enclosure(string name, double width, double hight, bool isOpen, bool hasRoof)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(width, nameof(width));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(hight, nameof(hight));

            Name = name;
            Width = width;
            Hight = hight;
            Size = width * hight;
            IsOpen = isOpen;
            HasRoof = hasRoof;
            _animals = new();
            Animals = _animals.AsReadOnly();
        }

        public void AddAnimal(Animal animal)
        {
            ArgumentNullException.ThrowIfNull(animal, nameof(animal));

            if (_animals.Contains(animal))
                throw new InvalidOperationException("The animal currently added is in this enclosure.");

            _animals.Add(animal);

        }
        public void RemoveAnimal(Animal animal)
        {
            ArgumentNullException.ThrowIfNull(animal, nameof(animal));
            if (_animals.Count == 0)
                throw new InvalidOperationException("No animals to remove from this enclosure.");

            if (!_animals.Contains(animal))
                throw new InvalidOperationException("The specified animal does not exist in this enclosure.");

            _animals.Remove(animal);
        }
        public abstract void Clean();
    }
}
