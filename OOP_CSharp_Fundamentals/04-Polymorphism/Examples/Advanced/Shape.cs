
using System;
using System.Collections.Generic;
using System.Linq;

namespace Polymorphism.Examples.Advanced
{

    // ════════════════════════════════════════════════════════════
    // الواجهات
    // ════════════════════════════════════════════════════════════

    /// <summary>
    /// واجهة يمكن حسابها (مساحة، محيط)
    /// </summary>
    public interface ICalculable
    {
        double CalculateArea();
        double CalculatePerimeter();
    }

    /// <summary>
    /// واجهة يمكن رسمها
    /// </summary>
    public interface IDrawable
    {
        void Draw();
        string GetColor();
    }

    /// <summary>
    /// واجهة يمكن مقارنتها
    /// </summary>
    public interface IComparable
    {
        int CompareTo(object obj);
    }

    // ════════════════════════════════════════════════════════════
    // الأشكال (مع تعدد الواجهات)
    // ════════════════════════════════════════════════════════════

    /// <summary>
    /// الشكل الأساسي
    /// </summary>
    public abstract class Shape : ICalculable, IDrawable, IComparable
    {
        public string name {get;}
        protected string color;

        public Shape(string name, string color)
        {
            this.name = name;
            this.color = color;
        }

        // من ICalculable
        public abstract double CalculateArea();
        public abstract double CalculatePerimeter();

        // من IDrawable
        public virtual void Draw()
        {
            Console.WriteLine($"🎨 رسم {name}");
        }

        public string GetColor() => color;

        // من IComparable
        public virtual int CompareTo(object obj)
        {
            if (obj is Shape s)
                return this.CalculateArea().CompareTo(s.CalculateArea());
            throw new ArgumentException("الكائن ليس Shape");
        }

        public virtual void PrintInfo()
        {
            Console.WriteLine($"الشكل: {name}");
            Console.WriteLine($"اللون: {color}");
            Console.WriteLine($"المساحة: {CalculateArea():F2}");
            Console.WriteLine($"المحيط: {CalculatePerimeter():F2}");
        }
    }

    /// <summary>
    /// دائرة
    /// </summary>
    public class Circle : Shape
    {
        private double radius;

        public Circle(string name, string color, double radius)
            : base(name, color)
        {
            this.radius = radius;
        }

        public override double CalculateArea()
        {
            return Math.PI * radius * radius;
        }

        public override double CalculatePerimeter()
        {
            return 2 * Math.PI * radius;
        }

        public override void Draw()
        {
            Console.WriteLine($"🔵 رسم دائرة {name} باللون {color}");
        }
    }

    /// <summary>
    /// مربع
    /// </summary>
    public class Square : Shape
    {
        private double side;

        public Square(string name, string color, double side)
            : base(name, color)
        {
            this.side = side;
        }

        public override double CalculateArea()
        {
            return side * side;
        }

        public override double CalculatePerimeter()
        {
            return 4 * side;
        }

        public override void Draw()
        {
            Console.WriteLine($"🟩 رسم مربع {name} باللون {color}");
        }
    }

    /// <summary>
    /// مستطيل
    /// </summary>
    public class Rectangle : Shape
    {
        private double width, height;

        public Rectangle(string name, string color, double width, double height)
            : base(name, color)
        {
            this.width = width;
            this.height = height;
        }

        public override double CalculateArea()
        {
            return width * height;
        }

        public override double CalculatePerimeter()
        {
            return 2 * (width + height);
        }

        public override void Draw()
        {
            Console.WriteLine($"🟨 رسم مستطيل {name} باللون {color}");
        }
    }

    /// <summary>
    /// مثلث
    /// </summary>
    public class Triangle : Shape
    {
        private double side1, side2, side3;

        public Triangle(string name, string color, double a, double b, double c)
            : base(name, color)
        {
            side1 = a;
            side2 = b;
            side3 = c;
        }

        public override double CalculateArea()
        {
            // صيغة هيرون
            double s = (side1 + side2 + side3) / 2;
            return Math.Sqrt(s * (s - side1) * (s - side2) * (s - side3));
        }

        public override double CalculatePerimeter()
        {
            return side1 + side2 + side3;
        }

        public override void Draw()
        {
            Console.WriteLine($"🔺 رسم مثلث {name} باللون {color}");
        }
    }


    // ════════════════════════════════════════════════════════════
    // الأنظمة (استخدام Polymorphism)
    // ════════════════════════════════════════════════════════════

    /// <summary>
    /// نظام إدارة الأشكال
    /// </summary>
    public class ShapeManager
    {
        private List<Shape> shapes = new();

        public void AddShape(Shape shape)
        {
            shapes.Add(shape);
        }

        public void DrawAll()
        {
            Console.WriteLine("\n🎨 رسم جميع الأشكال:");
            foreach (var shape in shapes)
            {
                shape.Draw();
            }
        }

        public void CalculateTotal()
        {
            Console.WriteLine("\n📐 الحسابات:");
            double totalArea = 0;
            double totalPerimeter = 0;

            foreach (var shape in shapes)
            {
                totalArea += shape.CalculateArea();
                totalPerimeter += shape.CalculatePerimeter();
            }

            Console.WriteLine($"إجمالي المساحة: {totalArea:F2}");
            Console.WriteLine($"إجمالي المحيط: {totalPerimeter:F2}");
        }

        public void SortByArea()
        {
            Console.WriteLine("\n📊 ترتيب الأشكال حسب المساحة:");
            var sorted = shapes.OrderBy(s => s.CalculateArea()).ToList();

            for (int i = 0; i < sorted.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {sorted[i].name} - {sorted[i].CalculateArea():F2}");
            }
        }

        public void PrintAllInfo()
        {
            Console.WriteLine("\n📋 معلومات جميع الأشكال:");
            foreach (var shape in shapes)
            {
                shape.PrintInfo();
                Console.WriteLine("────────────────────");
            }
        }
    }
}
