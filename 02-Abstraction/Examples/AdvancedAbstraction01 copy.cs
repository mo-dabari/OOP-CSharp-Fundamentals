/*
 * ShapeCalculator.cs
 * ============================================
 * مثال متقدم: نظام حساب مساحة الأشكال الهندسية
 * 
 * هذا الملف يوضح:
 * - Abstract Classes للسلوك المشترك
 * - Virtual Methods للتطبيقات المختلفة
 * - Polymorphism العملي في الحسابات
 * - معالجة مجموعة من الأشكال المختلفة
 * - Pattern الـ Factory مع Abstraction
 * 
 * التشبيه: كل شكل له طريقة مختلفة لحساب المساحة
 * لكن جميعها تتبع نفس الواجهة
 */

using System;
using System.Collections.Generic;

namespace OOP_CSharp_Fundamentals
{
    // ============================================
    // فئة مجردة: الشكل (Shape)
    // ============================================
    
    /// <summary>
    /// فئة مجردة تمثل أي شكل هندسي
    /// 
    /// الخصائص:
    /// - جميع الأشكال لها مساحة وحجم محيط
    /// - لكن طريقة الحساب مختلفة لكل شكل
    /// - لذا نجعل الحساب abstract
    /// </summary>
    public abstract class Shape
    {
        // خصائص مشتركة
        public string Name { get; set; }
        public string Color { get; set; }
        
        protected Shape(string name, string color)
        {
            Name = name;
            Color = color;
        }
        
        // 🔴 Abstract Methods - يجب تطبيقها
        /// <summary>
        /// حساب مساحة الشكل
        /// </summary>
        public abstract double CalculateArea();
        
        /// <summary>
        /// حساب محيط الشكل
        /// </summary>
        public abstract double CalculatePerimeter();
        
        // 🟢 Normal Method - موجود في جميع الأشكال
        public virtual void Display()
        {
            Console.WriteLine($"🔷 الشكل: {Name}");
            Console.WriteLine($"   اللون: {Color}");
            Console.WriteLine($"   المساحة: {CalculateArea():F2} وحدة²");
            Console.WriteLine($"   المحيط: {CalculatePerimeter():F2} وحدة");
        }
        
        // دالة مساعدة
        public virtual string GetInfo()
        {
            return $"{Name} ({Color}) - المساحة: {CalculateArea():F2}";
        }
    }
    
    
    // ============================================
    // المربع (Square)
    // ============================================
    
    /// <summary>
    /// مربع - شكل به أربع أضلاع متساوية
    /// </summary>
    public class Square : Shape
    {
        public double SideLength { get; set; }
        
        public Square(double sideLength, string color = "أحمر") 
            : base("مربع", color)
        {
            SideLength = sideLength;
        }
        
        public override double CalculateArea()
        {
            return SideLength * SideLength;
        }
        
        public override double CalculatePerimeter()
        {
            return 4 * SideLength;
        }
        
        public override void Display()
        {
            Console.WriteLine($"▢  {Name}");
            Console.WriteLine($"   طول الضلع: {SideLength} وحدة");
            base.Display();
        }
    }
    
    
    // ============================================
    // المستطيل (Rectangle)
    // ============================================
    
    public class Rectangle : Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }
        
        public Rectangle(double width, double height, string color = "أزرق")
            : base("مستطيل", color)
        {
            Width = width;
            Height = height;
        }
        
        public override double CalculateArea()
        {
            return Width * Height;
        }
        
        public override double CalculatePerimeter()
        {
            return 2 * (Width + Height);
        }
        
        public override void Display()
        {
            Console.WriteLine($"▭  {Name}");
            Console.WriteLine($"   العرض: {Width} وحدة");
            Console.WriteLine($"   الارتفاع: {Height} وحدة");
            base.Display();
        }
    }
    
    
    // ============================================
    // الدائرة (Circle)
    // ============================================
    
    public class Circle : Shape
    {
        public double Radius { get; set; }
        
        public Circle(double radius, string color = "أصفر")
            : base("دائرة", color)
        {
            Radius = radius;
        }
        
        public override double CalculateArea()
        {
            return Math.PI * Radius * Radius;
        }
        
        public override double CalculatePerimeter()
        {
            return 2 * Math.PI * Radius;
        }
        
        public override void Display()
        {
            Console.WriteLine($"●  {Name}");
            Console.WriteLine($"   نصف القطر: {Radius} وحدة");
            base.Display();
        }
    }
    
    
    // ============================================
    // المثلث (Triangle)
    // ============================================
    
    public class Triangle : Shape
    {
        public double SideA { get; set; }
        public double SideB { get; set; }
        public double SideC { get; set; }
        
        public Triangle(double sideA, double sideB, double sideC, string color = "أخضر")
            : base("مثلث", color)
        {
            SideA = sideA;
            SideB = sideB;
            SideC = sideC;
        }
        
        public override double CalculateArea()
        {
            // صيغة هيرون (Heron's Formula)
            double s = (SideA + SideB + SideC) / 2;
            return Math.Sqrt(s * (s - SideA) * (s - SideB) * (s - SideC));
        }
        
        public override double CalculatePerimeter()
        {
            return SideA + SideB + SideC;
        }
        
        public override void Display()
        {
            Console.WriteLine($"△  {Name}");
            Console.WriteLine($"   الضلع A: {SideA} وحدة");
            Console.WriteLine($"   الضلع B: {SideB} وحدة");
            Console.WriteLine($"   الضلع C: {SideC} وحدة");
            base.Display();
        }
    }
    
    
    // ============================================
    // الإهليج (Ellipse)
    // ============================================
    
    public class Ellipse : Shape
    {
        public double MajorAxis { get; set; }  // المحور الأكبر
        public double MinorAxis { get; set; }  // المحور الأصغر
        
        public Ellipse(double majorAxis, double minorAxis, string color = "بنفسجي")
            : base("إهليج", color)
        {
            MajorAxis = majorAxis;
            MinorAxis = minorAxis;
        }
        
        public override double CalculateArea()
        {
            return Math.PI * (MajorAxis / 2) * (MinorAxis / 2);
        }
        
        public override double CalculatePerimeter()
        {
            // تقريب بسيط للمحيط
            double a = MajorAxis / 2;
            double b = MinorAxis / 2;
            return Math.PI * (3 * (a + b) - Math.Sqrt((3 * a + b) * (a + 3 * b)));
        }
        
        public override void Display()
        {
            Console.WriteLine($"⬭  {Name}");
            Console.WriteLine($"   المحور الأكبر: {MajorAxis} وحدة");
            Console.WriteLine($"   المحور الأصغر: {MinorAxis} وحدة");
            base.Display();
        }
    }
    
    
    // ============================================
    // متر الأشكال (ShapeMeter)
    // ============================================
    
    /// <summary>
    /// فئة تتعامل مع مجموعة من الأشكال المختلفة
    /// توضح قوة Polymorphism
    /// </summary>
    public class ShapeMeter
    {
        private List<Shape> shapes = new();
        
        public void AddShape(Shape shape)
        {
            shapes.Add(shape);
        }
        
        public void DisplayAllShapes()
        {
            Console.WriteLine("\n📐 جميع الأشكال:");
            Console.WriteLine("════════════════════════════════════");
            
            foreach (var shape in shapes)
            {
                shape.Display();
                Console.WriteLine();
            }
        }
        
        public double GetTotalArea()
        {
            double total = 0;
            foreach (var shape in shapes)
            {
                total += shape.CalculateArea();
            }
            return total;
        }
        
        public double GetTotalPerimeter()
        {
            double total = 0;
            foreach (var shape in shapes)
            {
                total += shape.CalculatePerimeter();
            }
            return total;
        }
        
        public void PrintStatistics()
        {
            Console.WriteLine("\n📊 الإحصائيات:");
            Console.WriteLine("════════════════════════════════════");
            Console.WriteLine($"عدد الأشكال: {shapes.Count}");
            Console.WriteLine($"المساحة الإجمالية: {GetTotalArea():F2} وحدة²");
            Console.WriteLine($"المحيط الإجمالي: {GetTotalPerimeter():F2} وحدة");
        }
        
        public Shape GetLargestShape()
        {
            Shape largest = null;
            double maxArea = 0;
            
            foreach (var shape in shapes)
            {
                if (shape.CalculateArea() > maxArea)
                {
                    maxArea = shape.CalculateArea();
                    largest = shape;
                }
            }
            
            return largest;
        }
        
        public void PrintLargestShape()
        {
            var largest = GetLargestShape();
            if (largest != null)
            {
                Console.WriteLine($"\n🏆 أكبر شكل: {largest.GetInfo()}");
            }
        }
    }
    
    
    // ============================================
    // Factory Pattern - لإنشاء الأشكال
    // ============================================
    
    /// <summary>
    /// Factory لإنشاء الأشكال بناءً على اسمها
    /// يوضح مدى سهولة التوسع
    /// </summary>
    public class ShapeFactory
    {
        public static Shape CreateShape(string shapeType, params double[] dimensions)
        {
            return shapeType.ToLower() switch
            {
                "square" => new Square(dimensions[0]),
                "rectangle" => new Rectangle(dimensions[0], dimensions[1]),
                "circle" => new Circle(dimensions[0]),
                "triangle" => new Triangle(dimensions[0], dimensions[1], dimensions[2]),
                "ellipse" => new Ellipse(dimensions[0], dimensions[1]),
                _ => throw new ArgumentException($"نوع شكل غير معروف: {shapeType}")
            };
        }
    }
    
    
    // ============================================
    // Program - الاستخدام
    // ============================================
    
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("  نظام حساب الأشكال الهندسية - مثال على Abstraction");
            Console.WriteLine("═══════════════════════════════════════════════════════════\n");
            
            // ============================================
            // 1️⃣ إنشاء أشكال مختلفة
            // ============================================
            Console.WriteLine("1️⃣  إنشاء الأشكال:");
            Console.WriteLine("────────────────────────────────────\n");
            
            ShapeMeter meter = new();
            
            // مربع
            meter.AddShape(new Square(5, "أحمر"));
            
            // مستطيل
            meter.AddShape(new Rectangle(4, 6, "أزرق"));
            
            // دائرة
            meter.AddShape(new Circle(3, "أصفر"));
            
            // مثلث
            meter.AddShape(new Triangle(3, 4, 5, "أخضر"));
            
            // إهليج
            meter.AddShape(new Ellipse(6, 4, "بنفسجي"));
            
            // عرض جميع الأشكال
            meter.DisplayAllShapes();
            
            // ============================================
            // 2️⃣ الحسابات الإجمالية
            // ============================================
            meter.PrintStatistics();
            meter.PrintLargestShape();
            
            // ============================================
            // 3️⃣ Polymorphism في العمل
            // ============================================
            Console.WriteLine("\n3️⃣  توضيح Polymorphism:");
            Console.WriteLine("════════════════════════════════════");
            Console.WriteLine("""
            
            نفس الكود يعمل مع أشكال مختلفة:
            """);
            
            Shape[] shapesArray = new Shape[]
            {
                new Square(4),
                new Circle(2),
                new Triangle(3, 4, 5)
            };
            
            Console.WriteLine("\nحساب المساحة للجميع:");
            foreach (var shape in shapesArray)
            {
                // نفس الدالة، لكن تحسب بطرق مختلفة!
                Console.WriteLine($"  • {shape.Name}: {shape.CalculateArea():F2} وحدة²");
            }
            
            // ============================================
            // 4️⃣ Factory Pattern
            // ============================================
            Console.WriteLine("\n4️⃣  استخدام Factory Pattern:");
            Console.WriteLine("════════════════════════════════════\n");
            
            var shape1 = ShapeFactory.CreateShape("square", 5);
            var shape2 = ShapeFactory.CreateShape("circle", 3);
            var shape3 = ShapeFactory.CreateShape("rectangle", 4, 6);
            
            Console.WriteLine($"✅ تم إنشاء: {shape1.GetInfo()}");
            Console.WriteLine($"✅ تم إنشاء: {shape2.GetInfo()}");
            Console.WriteLine($"✅ تم إنشاء: {shape3.GetInfo()}");
            
            // ============================================
            // الفوائد
            // ============================================
            Console.WriteLine("\n═══════════════════════════════════════════════════════════");
            Console.WriteLine("  ✨ الفوائد التي حققناها:");
            Console.WriteLine("═══════════════════════════════════════════════════════════\n");
            
            Console.WriteLine("""
            1️⃣  السهولة:
                - إضافة شكل جديد سهل جداً
                - فقط ننشئ فئة جديدة ترث من Shape
            
            2️⃣  المرونة:
                - نفس الكود يعمل مع أشكال جديدة
                - لا نحتاج تغيير ShapeMeter
            
            3️⃣  الوضوح:
                - كل شكل يحتوي على منطقه الخاص
                - سهل الفهم والصيانة
            
            4️⃣  إعادة الاستخدام:
                - يمكن استخدام Shape في أماكن أخرى
                - يعمل مع أي شكل جديد تلقائياً
            
            5️⃣  الأمان:
                - التفاصيل معقدة (صيغ رياضية) مخفية
                - المستخدم يستخدم واجهة بسيطة فقط
            """);
            
            // ============================================
            // مقارنة مع بدون Abstraction
            // ============================================
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("  📊 مقارنة:");
            Console.WriteLine("═══════════════════════════════════════════════════════════\n");
            
            Console.WriteLine("""
            ❌ بدون Abstraction:
               if (shapeType == "square") area = side * side;
               else if (shapeType == "circle") area = PI * r * r;
               else if (shapeType == "rectangle") area = w * h;
               // 100+ سطر كود مع كل حالة جديدة!
            
            ✅ مع Abstraction:
               area = shape.CalculateArea();
               // كود بسيط جداً يعمل مع أي شكل!
            """);
            
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("  ✅ انتهى المثال");
            Console.WriteLine("═══════════════════════════════════════════════════════════\n");
        }
    }
}