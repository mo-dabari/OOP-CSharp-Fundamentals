/*
 * PolymorphicCollections.cs
 * ════════════════════════════════════════════════════════════
 * مثال متقدم: التعامل مع قوائم Polymorphim
 *
 * يوضح:
 * - قوائم من الأنواع المختلفة
 * - Filtering و Searching
 * - Aggregation و Transformation
 * - Pattern Matching مع Polymorphism
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace Polymorphism.Examples.Advanced
{
    // ════════════════════════════════════════════════════════════
    // نماذج البيانات
    // ════════════════════════════════════════════════════════════

    /// <summary>
    /// المركبة - الأب
    /// </summary>
    public abstract class Vehicle
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Year { get; set; }

        public Vehicle(string name, decimal price, int year)
        {
            Name = name;
            Price = price;
            Year = year;
        }

        public abstract double GetFuelConsumption();  // لتر/100 كم
        public abstract void StartEngine();
        public abstract void DisplayInfo();
    }

    /// <summary>
    /// سيارة
    /// </summary>
    public class Car : Vehicle
    {
        public int Doors { get; set; }

        public Car(string name, decimal price, int year, int doors)
            : base(name, price, year)
        {
            Doors = doors;
        }

        public override double GetFuelConsumption() => 8.5;  // 8.5 لتر/100كم

        public override void StartEngine()
        {
            Console.WriteLine($"🚗 سيارة {Name} بدأت محرك البنزين");
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"🚗 سيارة: {Name} | السعر: {Price:C} | الأبواب: {Doors}");
        }
    }

    /// <summary>
    /// دراجة نارية
    /// </summary>
    public class Motorcycle : Vehicle
    {
        public bool HasStorage { get; set; }

        public Motorcycle(string name, decimal price, int year, bool storage)
            : base(name, price, year)
        {
            HasStorage = storage;
        }

        public override double GetFuelConsumption() => 4.0;  // 4 لتر/100كم

        public override void StartEngine()
        {
            Console.WriteLine($"🏍️  دراجة {Name} بدأت");
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"🏍️  دراجة: {Name} | السعر: {Price:C}");
        }
    }

    /// <summary>
    /// شاحنة
    /// </summary>
    public class Truck : Vehicle
    {
        public int Capacity { get; set; }  // بالطن

        public Truck(string name, decimal price, int year, int capacity)
            : base(name, price, year)
        {
            Capacity = capacity;
        }

        public override double GetFuelConsumption() => 15.0;  // 15 لتر/100كم

        public override void StartEngine()
        {
            Console.WriteLine($"🚚 شاحنة {Name} بدأت محرك الديزل");
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"🚚 شاحنة: {Name} | السعر: {Price:C} | السعة: {Capacity}ط");
        }
    }

    /// <summary>
    /// سيارة كهربائية
    /// </summary>
    public class ElectricCar : Vehicle
    {
        public int BatteryCapacity { get; set; }  // بـ kWh

        public ElectricCar(string name, decimal price, int year, int battery)
            : base(name, price, year)
        {
            BatteryCapacity = battery;
        }

        public override double GetFuelConsumption() => 0.0;  // بدون وقود!

        public override void StartEngine()
        {
            Console.WriteLine($"⚡ سيارة كهربائية {Name} بدأت بصمت");
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"⚡ سيارة كهربائية: {Name} | السعر: {Price:C} | البطارية: {BatteryCapacity}kWh");
        }
    }

    // ════════════════════════════════════════════════════════════
    // نظام إدارة المركبات
    // ════════════════════════════════════════════════════════════

    public class Dealership
    {
        private List<Vehicle> vehicles = new();

        public void AddVehicle(Vehicle vehicle)
        {
            vehicles.Add(vehicle);
        }

        // ─────────────────────────────────────────
        // العرض
        // ─────────────────────────────────────────

        public void DisplayAllVehicles()
        {
            Console.WriteLine("\n📋 جميع المركبات:");
            foreach (var vehicle in vehicles)
            {
                vehicle.DisplayInfo();
            }
        }

        public void StartAllVehicles()
        {
            Console.WriteLine("\n🔧 بدء جميع المركبات:");
            foreach (var vehicle in vehicles)
            {
                vehicle.StartEngine();
            }
        }

        // ─────────────────────────────────────────
        // التصفية (Filtering)
        // ─────────────────────────────────────────

        public void DisplayCarsOnly()
        {
            Console.WriteLine("\n🚗 السيارات فقط:");
            var cars = vehicles.OfType<Car>();
            foreach (var car in cars)
            {
                car.DisplayInfo();
            }
        }

        public void DisplayByType<T>() where T : Vehicle
        {
            Console.WriteLine($"\n📍 النوع: {typeof(T).Name}");
            var filtered = vehicles.OfType<T>();
            if (filtered.Count() == 0)
            {
                Console.WriteLine("لا توجد مركبات من هذا النوع");
                return;
            }
            foreach (var vehicle in filtered)
            {
                vehicle.DisplayInfo();
            }
        }

        public void DisplayExpensiveVehicles(decimal minPrice)
        {
            Console.WriteLine($"\n💰 المركبات التي تزيد عن {minPrice:C}:");
            var expensive = vehicles.Where(v => v.Price >= minPrice);
            foreach (var vehicle in expensive)
            {
                vehicle.DisplayInfo();
            }
        }

        public void DisplayVehiclesByYear(int year)
        {
            Console.WriteLine($"\n📅 المركبات من سنة {year}:");
            var byYear = vehicles.Where(v => v.Year == year);
            foreach (var vehicle in byYear)
            {
                vehicle.DisplayInfo();
            }
        }

        // ─────────────────────────────────────────
        // الحسابات (Aggregations)
        // ─────────────────────────────────────────

        public decimal GetAveragePrice()
        {
            return vehicles.Count == 0 ? 0 : vehicles.Average(v => v.Price);
        }

        public decimal GetTotalPrice()
        {
            return vehicles.Sum(v => v.Price);
        }

        public void PrintStatistics()
        {
            Console.WriteLine("\n📊 الإحصائيات:");
            Console.WriteLine($"  عدد المركبات: {vehicles.Count}");
            Console.WriteLine($"  السعر الإجمالي: {GetTotalPrice():C}");
            Console.WriteLine($"  متوسط السعر: {GetAveragePrice():C}");
            Console.WriteLine($"  أرخص مركبة: {vehicles.MinBy(v => v.Price)?.Name}");
            Console.WriteLine($"  أغلى مركبة: {vehicles.MaxBy(v => v.Price)?.Name}");
        }

        public void PrintFuelConsumptionReport()
        {
            Console.WriteLine("\n⛽ تقرير استهلاك الوقود:");
            var orderedByConsumption = vehicles.OrderBy(v => v.GetFuelConsumption());
            foreach (var vehicle in orderedByConsumption)
            {
                Console.WriteLine($"  {vehicle.Name}: {vehicle.GetFuelConsumption():F2} لتر/100كم");
            }
        }

        // ─────────────────────────────────────────
        // البحث و المطابقة
        // ─────────────────────────────────────────

        public Vehicle FindByName(string name)
        {
            return vehicles.FirstOrDefault(v => v.Name == name);
        }

        public List<Vehicle> FindByPriceRange(decimal min, decimal max)
        {
            return vehicles.Where(v => v.Price >= min && v.Price <= max).ToList();
        }

        // ─────────────────────────────────────────
        // Pattern Matching
        // ─────────────────────────────────────────

        public void DisplayVehicleDetails(Vehicle vehicle)
        {
            Console.WriteLine($"\n🔍 تفاصيل المركبة:");

            switch (vehicle)
            {
                case Car car:
                    Console.WriteLine($"  نوع: سيارة");
                    Console.WriteLine($"  الأبواب: {car.Doors}");
                    break;

                case Motorcycle bike:
                    Console.WriteLine($"  نوع: دراجة نارية");
                    Console.WriteLine($"  خزان: {(bike.HasStorage ? "نعم" : "لا")}");
                    break;

                case Truck truck:
                    Console.WriteLine($"  نوع: شاحنة");
                    Console.WriteLine($"  السعة: {truck.Capacity}ط");
                    break;

                case ElectricCar eCar:
                    Console.WriteLine($"  نوع: سيارة كهربائية");
                    Console.WriteLine($"  البطارية: {eCar.BatteryCapacity}kWh");
                    break;

                default:
                    Console.WriteLine("  نوع غير معروف");
                    break;
            }

            Console.WriteLine($"  السعر: {vehicle.Price:C}");
            Console.WriteLine($"  السنة: {vehicle.Year}");
            Console.WriteLine($"  استهلاك الوقود: {vehicle.GetFuelConsumption():F2} لتر/100كم");
        }

        // ─────────────────────────────────────────
        // التحويل (Transformation)
        // ─────────────────────────────────────────

        public void PrintVehiclesByType()
        {
            Console.WriteLine("\n🗂️  المركبات مجمعة حسب النوع:");

            var groupedByType = vehicles
                .GroupBy(v => v.GetType().Name)
                .OrderByDescending(g => g.Count());

            foreach (var group in groupedByType)
            {
                Console.WriteLine($"\n{group.Key} ({group.Count()}):");
                foreach (var vehicle in group)
                {
                    Console.WriteLine($"  • {vehicle.Name} - {vehicle.Price:C}");
                }
            }
        }

        public void PrintSortedByPrice(bool ascending = true)
        {
            Console.WriteLine($"\n💰 مرتبة حسب السعر ({(ascending ? "من الأقل" : "من الأعلى")}):");
            var sorted = ascending
                ? vehicles.OrderBy(v => v.Price)
                : vehicles.OrderByDescending(v => v.Price);

            foreach (var vehicle in sorted)
            {
                Console.WriteLine($"  {vehicle.Name}: {vehicle.Price:C}");
            }
        }
    }
}
