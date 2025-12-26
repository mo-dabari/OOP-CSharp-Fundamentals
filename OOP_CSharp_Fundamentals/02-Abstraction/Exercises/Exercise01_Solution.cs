using System;
using System.Collections.Generic;

namespace Abstraction.Exercises
{
    public interface IVehicle
    {
        void StartEngine();
        void StopEngine();
        int GetMaxSpeed();
        double GetFuelConsumption();
        string GetVehicleType();
    }
    
    public class Car : IVehicle
    {
        public void StartEngine()
        {
            Console.WriteLine("🚗 محرك السيارة يعمل: براااام!");
        }
        
        public void StopEngine()
        {
            Console.WriteLine("🚗 محرك السيارة توقف");
        }
        
        public int GetMaxSpeed() => 200;
        public double GetFuelConsumption() => 8.0;
        public string GetVehicleType() => "سيارة";
    }
    
    public class Motorcycle : IVehicle
    {
        public void StartEngine()
        {
            Console.WriteLine("🏍️  محرك الدراجة يعمل: ووووووم!");
        }
        
        public void StopEngine()
        {
            Console.WriteLine("🏍️  محرك الدراجة توقف");
        }
        
        public int GetMaxSpeed() => 150;
        public double GetFuelConsumption() => 25.0;
        public string GetVehicleType() => "دراجة نارية";
    }
    
    public class Bus : IVehicle
    {
        public void StartEngine()
        {
            Console.WriteLine("🚌 محرك الحافلة يعمل: بررررم!");
        }
        
        public void StopEngine()
        {
            Console.WriteLine("🚌 محرك الحافلة توقف");
        }
        
        public int GetMaxSpeed() => 120;
        public double GetFuelConsumption() => 6.0;
        public string GetVehicleType() => "حافلة";
    }
    
    public class VehicleManager
    {
        private List<IVehicle> vehicles = new();
        
        public void AddVehicle(IVehicle vehicle)
        {
            vehicles.Add(vehicle);
        }
        
        public void StartAllVehicles()
        {
            Console.WriteLine("\n🚀 بدء جميع المركبات:");
            foreach (var vehicle in vehicles)
                vehicle.StartEngine();
        }
        
        public void StopAllVehicles()
        {
            Console.WriteLine("\n⛔ إيقاف جميع المركبات:");
            foreach (var vehicle in vehicles)
                vehicle.StopEngine();
        }
        
        public double GetAverageMaxSpeed()
        {
            if (vehicles.Count == 0) return 0;
            double total = 0;
            foreach (var vehicle in vehicles)
                total += vehicle.GetMaxSpeed();
            return total / vehicles.Count;
        }
        
        public double GetAverageFuelConsumption()
        {
            if (vehicles.Count == 0) return 0;
            double total = 0;
            foreach (var vehicle in vehicles)
                total += vehicle.GetFuelConsumption();
            return total / vehicles.Count;
        }
        
        public void PrintReport()
        {
            Console.WriteLine("\n📊 تقرير المركبات:");
            Console.WriteLine("════════════════════════════════");
            Console.WriteLine($"عدد المركبات: {vehicles.Count}");
            Console.WriteLine($"متوسط أقصى سرعة: {GetAverageMaxSpeed():F1} كم/س");
            Console.WriteLine($"متوسط استهلاك الوقود: {GetAverageFuelConsumption():F1} كم/لتر");
            Console.WriteLine("\nتفاصيل المركبات:");
            foreach (var vehicle in vehicles)
            {
                Console.WriteLine($"  • {vehicle.GetVehicleType()}: " +
                    $"السرعة {vehicle.GetMaxSpeed()} كم/س، " +
                    $"استهلاك {vehicle.GetFuelConsumption()} كم/لتر");
            }
        }
    }

}