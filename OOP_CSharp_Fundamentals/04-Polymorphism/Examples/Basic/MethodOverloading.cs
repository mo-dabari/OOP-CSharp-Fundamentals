namespace Polymorphism.Examples.Basic
{
    // ════════════════════════════════════════════════════════════
    // 1. METHOD OVERLOADING (Compile-time Polymorphism)
    // ════════════════════════════════════════════════════════════

    /// <summary>
    /// آلة حاسبة - مثال على Method Overloading
    /// </summary>
    public class Calculator
    {
        // Overload 1: عددين صحيحين
        public int Add(int a, int b)
        {
            Console.WriteLine($"➕ جمع عددين صحيحين: {a} + {b}");
            return a + b;
        }

        // Overload 2: عددين عشريين
        public double Add(double a, double b)
        {
            Console.WriteLine($"➕ جمع عددين عشريين: {a} + {b}");
            return a + b;
        }

        // Overload 3: ثلاثة أعداد
        public int Add(int a, int b, int c)
        {
            Console.WriteLine($"➕ جمع ثلاثة أعداد: {a} + {b} + {c}");
            return a + b + c;
        }

        // Overload 4: قائمة من الأعداد
        public int Add(params int[] numbers)
        {
            int sum = 0;
            foreach (var num in numbers)
                sum += num;
            Console.WriteLine($"➕ جمع {numbers.Length} أرقام");
            return sum;
        }

        // Overload 5: السلاسل النصية
        public string Add(string a, string b)
        {
            Console.WriteLine($"➕ ربط نصين");
            return a + b;
        }
    }
}
