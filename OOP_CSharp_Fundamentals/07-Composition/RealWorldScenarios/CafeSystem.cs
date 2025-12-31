namespace Composition.Exercises
{
    // ════════════════════════════════════════════════════════════
    // الحالة الواقعية: نظام مقهى (Complex Composition)
    // ════════════════════════════════════════════════════════════

    public class CoffeeMachine
    {
        private int waterLevel;
        private int beanLevel;

        public CoffeeMachine()
        {
            waterLevel = 100;
            beanLevel = 100;
        }

        public void MakeCoffee()
        {
            if (waterLevel > 0 && beanLevel > 0)
            {
                Console.WriteLine("☕ صنع القهوة...");
                waterLevel -= 20;
                beanLevel -= 15;
            }
        }
    }

    public class Refrigerator
    {
        private List<string> items = new();

        public void AddItem(string item)
        {
            items.Add(item);
            Console.WriteLine($"❄️  تم إضافة {item}");
        }

        public List<string> GetItems() => new List<string>(items);
    }

    public class Oven
    {
        private int temperature;

        public void HeatUp(int temp)
        {
            temperature = temp;
            Console.WriteLine($"🔥 الفرن يسخن إلى {temperature}°");
        }
    }

    public class Cafe
    {
        private CoffeeMachine coffeeMachine;
        private Refrigerator refrigerator;
        private Oven oven;
        private string name;

        public Cafe(string cafeName)
        {
            name = cafeName;
            coffeeMachine = new CoffeeMachine();
            refrigerator = new Refrigerator();
            oven = new Oven();
        }

        public void MakeCoffee()
        {
            Console.WriteLine($"\n☕ مقهى {name}:");
            coffeeMachine.MakeCoffee();
        }

        public void PrepareFood()
        {
            Console.WriteLine($"\n🍰 تحضير الطعام:");
            oven.HeatUp(180);
            var items = refrigerator.GetItems();
            if (items.Count > 0)
                Console.WriteLine($"استخدام: {items[0]}");
        }

        public void AddIngredientsToFridge(string item)
        {
            refrigerator.AddItem(item);
        }
    }
}
