using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace OOP_CSharp_Fundamentals
{
    public abstract class Vehicle
    {

        public string Color {get;}
        public string Brand {get;}
        public double Speed {get;}

        public Vehicle(string color , string brand , double speed)
        {
            if(string.IsNullOrWhiteSpace(color, brand))
                throw new ArgumentNullException("Must Be Required color, brand");

            if(speed <= 40)
                throw new InvalidEnumArgumentException("Invalid Date Must Be Larger Than 40");

            Color = color;
            Brand = brand;
            Speed = speed;
        }

        public virtual void Start() => Console.WriteLine($"{brand} ({color}) يبدأ التشغيل");

        public virtual void Stop() => Console.WriteLine($"{brand} يتوقف");
            
        public virtual double GetSpeed() => speed;

        public virtual string GetInfo() => $"{brand} ({color})";
    }


    public class Car : Vehicle
    {
        public byte NumberOfDoors { get; }
        public Car(string color , string brand , double speed , byte numberOfDoors)
        : base(color, brand, speed)
        {
            if(numberOfDoors < 2)
                throw new ArgumentException("Must Be Number Of Doors 2 Or 4");

            NumberOfDoors = numberOfDoors;
        }

        public override void Start()
        {
            Console.WriteLine($"🚗 سيارة {brand}: برووووم!");
        }

        public override void Stop()
        {
            base.Stop();
            Console.WriteLine($" الأبواب مغلقة");
        }

        public override string GetInfo()
        {
            return base.GetInfo() + $" - {numberOfDoors} أبواب";
        }
    }


    public class Motorcycle : Vehicle
    {
        public bool HasStorage { get; }
        public Motorcycle(string color , string brand , double speed , bool hasStorage)
        : base(color, brand, speed)
        {
            HasStorage = hasStorage;
        }

        public override void Start()
        {
            Console.WriteLine($"🏍️  دراجة {brand}: ووووووووم!");
        }

        public override void Stop()
        {
            base.Stop();
            Console.WriteLine($"   {(hasStorage ? "خزان مملوء" : "بدون خزان")}");
        }
    }


    public class Truck : Vehicle
    {
        public double loadCapacity { get; }
        public Truck(string color , string brand , double speed , byte loadCapacity)
        : base(color, brand, speed)
        {
            if(loadCapacity <= 0)
                throw new ArgumentException("Must Be load Capacity Grater Than 0");

            LoadCapacity = loadCapacity;
        }

        public override void Start()
        {
            Console.WriteLine($"🚚 شاحنة {brand}: بررررررم!");
        }

        public void LoadCargo(decimal weight)
        {
            if(weight <= 0)
                throw new ArgumentException("Must Be weight Grater Than 0 and smaller than load Capacity or Equle");

            if (weight <= loadCapacity)
                Console.WriteLine($"📦 تحميل البضاعة: {weight} طن");
            else
                Console.WriteLine($"❌ وزن البضاعة أكثر من الحد ({loadCapacity} طن)");
        }
    }


        public class ElectricCar : Car
    {
        public short BatteryCapacity { get; }
        public ElectricCar(string color , string brand , double speed , byte numberOfDoors , short batteryCapacity)
        : base(color, brand, speed, numberOfDoors)
        {
            if(batteryCapacity <= 0)
                throw new ArgumentException("Must Be Battery Capacity Grater Than 0");

            BatteryCapacity = batteryCapacity;
        }

        public override void Start()
        {
            Console.WriteLine($"⚡ سيارة كهربائية {brand}: وووووم هادئ!");
        }
        
        public void Charge()
        {
            Console.WriteLine($"🔌 شحن البطارية: {batteryCapacity}%");
        }
        
        public override string GetInfo()
        {
            return base.GetInfo() + $" - بطارية {batteryCapacity}%";
        }
    }


    public class FleetManager
    {
        private readonly List<Vehicle> _vehicles = new();
        public IReadOnlyList<Vehicle> ReadOnlyVehicles;

        public FleetManager()
        {
            ReadOnlyVehicles = _vehicles;
        }

        public void AddVehicles(Vehicle vehicle)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(vehicle);

            _vehicles.Add(vehicle);
        }

        public void StartAll()
        {
            Console.WriteLine("\n🚗 بدء جميع المركبات:");
            foreach (var vehicle in vehicles)
                vehicle.Start();
        }

        public void StopAll()
        {
            Console.WriteLine("\n⛔ إيقاف جميع المركبات:");
            foreach (var vehicle in vehicles)
                vehicle.Stop();
        }

        public void PrintSpeedReport()
        {
            Console.WriteLine("\n📊 تقرير السرعات:");
            foreach (var vehicle in vehicles)
                Console.WriteLine($"  • {vehicle.GetInfo()}: {vehicle.GetSpeed()} كم/س");
        }

        public void PrintFleetInfo()
        {
            Console.WriteLine("\n🚗 معلومات الأسطول:");
            foreach (var vehicle in vehicles)
                Console.WriteLine($"  • {vehicle.GetInfo()}");
        }
    }
}