namespace Polymorphism.Exercises
{

    public interface IFood
    {
        string GetName();
        double GetCalories();      // سعرات حرارية
        double GetProtein();       // بروتين (جرام)
        double GetFat();           // دهون (جرام)
    }

    public class Meat : IFood
    {
        private string name;
        private double weight;  // بالجرام

        public Meat(string name, double weight)
        {
            this.name = name;
            this.weight = weight;
        }

        public string GetName() => name;
        public double GetCalories() => weight * 1.65;   // 165 كالوري لكل 100 جرام
        public double GetProtein() => weight * 0.26;    // 26 جرام بروتين لكل 100 جرام
        public double GetFat() => weight * 0.05;        // 5 جرام دهون
    }

    public class Vegetable : IFood
    {
        private string name;
        private double weight;

        public Vegetable(string name, double weight)
        {
            this.name = name;
            this.weight = weight;
        }

        public string GetName() => name;
        public double GetCalories() => weight * 0.25;   // 25 كالوري لكل 100 جرام
        public double GetProtein() => weight * 0.025;   // 2.5 جرام
        public double GetFat() => weight * 0.002;       // 0.2 جرام
    }

    public class Fruit : IFood
    {
        private string name;
        private double weight;

        public Fruit(string name, double weight)
        {
            this.name = name;
            this.weight = weight;
        }

        public string GetName() => name;
        public double GetCalories() => weight * 0.52;   // 52 كالوري لكل 100 جرام
        public double GetProtein() => weight * 0.005;   // 0.5 جرام
        public double GetFat() => weight * 0.003;       // 0.3 جرام
    }

    public class NutritionTracker
    {
        private List<IFood> meals = new();

        public void AddFood(IFood food)
        {
            meals.Add(food);
            Console.WriteLine($"✅ تمت إضافة {food.GetName()}");
        }

        public void PrintDailyNutrition()
        {
            Console.WriteLine("\n📊 التغذية اليومية:");
            double totalCalories = 0;
            double totalProtein = 0;
            double totalFat = 0;

            foreach (var food in meals)
            {
                totalCalories += food.GetCalories();
                totalProtein += food.GetProtein();
                totalFat += food.GetFat();
            }

            Console.WriteLine($"  الوجبات: {meals.Count}");
            Console.WriteLine($"  السعرات الحرارية: {totalCalories:F1}");
            Console.WriteLine($"  البروتين: {totalProtein:F1}جم");
            Console.WriteLine($"  الدهون: {totalFat:F1}جم");
        }

        public void PrintMealBreakdown()
        {
            Console.WriteLine("\n📋 تفصيل الوجبات:");
            foreach (var food in meals)
            {
                Console.WriteLine($"  {food.GetName()}:");
                Console.WriteLine($"    - السعرات: {food.GetCalories():F1}");
                Console.WriteLine($"    - البروتين: {food.GetProtein():F1}جم");
                Console.WriteLine($"    - الدهون: {food.GetFat():F1}جم");
            }
        }
    }
}
