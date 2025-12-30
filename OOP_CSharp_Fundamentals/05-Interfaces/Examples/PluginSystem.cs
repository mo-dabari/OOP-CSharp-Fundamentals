/*
 * PluginSystem.cs
 * ════════════════════════════════════════════════════════════
 * مثال واقعي: نظام الإضافات (Plugin System)
 *
 * يوضح:
 * - كيفية بناء نظام قابل للتوسع
 * - إضافة ميزات جديدة بدون تعديل الكود القديم
 * - معالجة الصور مع إضافات مختلفة
 * - تطبيق حقيقي لـ Interfaces
 */

using System;
using System.Collections.Generic;

namespace Interfaces.Examples
{
    // ════════════════════════════════════════════════════════════
    // نموذج البيانات
    // ════════════════════════════════════════════════════════════

    /// <summary>
    /// الصورة
    /// </summary>
    public class Image
    {
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Format { get; set; }

        public Image(string name, int width, int height, string format)
        {
            Name = name;
            Width = width;
            Height = height;
            Format = format;
        }

        public override string ToString()
        {
            return $"صورة: {Name} ({Width}x{Height}) - {Format}";
        }
    }


    // ════════════════════════════════════════════════════════════
    // الواجهات (Interfaces) - العقود
    // ════════════════════════════════════════════════════════════

    /// <summary>
    /// واجهة الإضافة (Plugin Interface)
    /// كل إضافة يجب أن تطبق هذه الواجهة
    /// </summary>
    public interface IImagePlugin
    {
        string GetName();
        string GetDescription();
        void Process(Image image);
    }


    // ════════════════════════════════════════════════════════════
    // الإضافات المختلفة (Plugins)
    // ════════════════════════════════════════════════════════════

    /// <summary>
    /// إضافة تحويل الأبيض والأسود
    /// </summary>
    public class BlackAndWhitePlugin : IImagePlugin
    {
        public string GetName() => "تحويل لأبيض وأسود";

        public string GetDescription() => "تحويل الصورة إلى أبيض وأسود";

        public void Process(Image image)
        {
            Console.WriteLine($"🎨 تطبيق البرنامج: {GetName()}");
            Console.WriteLine($"   على الصورة: {image.Name}");
            Console.WriteLine($"   ✅ تم تحويل الصورة إلى أبيض وأسود!");
        }
    }

    /// <summary>
    /// إضافة تكبير الصورة
    /// </summary>
    public class ResizePlugin : IImagePlugin
    {
        private double scale;

        public ResizePlugin(double scale = 1.5)
        {
            this.scale = scale;
        }

        public string GetName() => $"تكبير الصورة ({scale}x)";

        public string GetDescription() => $"تكبير الصورة بمعامل {scale}";

        public void Process(Image image)
        {
            int newWidth = (int)(image.Width * scale);
            int newHeight = (int)(image.Height * scale);

            Console.WriteLine($"🔍 تطبيق البرنامج: {GetName()}");
            Console.WriteLine($"   الحجم الأصلي: {image.Width}x{image.Height}");
            Console.WriteLine($"   الحجم الجديد: {newWidth}x{newHeight}");
            Console.WriteLine($"   ✅ تم تكبير الصورة بنجاح!");
        }
    }

    /// <summary>
    /// إضافة إضافة فلتر
    /// </summary>
    public class FilterPlugin : IImagePlugin
    {
        private string filterName;

        public FilterPlugin(string name = "Sepia")
        {
            filterName = name;
        }

        public string GetName() => $"تطبيق فلتر {filterName}";

        public string GetDescription() => $"تطبيق فلتر {filterName} على الصورة";

        public void Process(Image image)
        {
            Console.WriteLine($"🎭 تطبيق البرنامج: {GetName()}");
            Console.WriteLine($"   على: {image.Name}");
            Console.WriteLine($"   الفلتر: {filterName}");
            Console.WriteLine($"   ✅ تم تطبيق الفلتر بنجاح!");
        }
    }

    /// <summary>
    /// إضافة إضافة العلامة المائية
    /// </summary>
    public class WatermarkPlugin : IImagePlugin
    {
        private string watermarkText;

        public WatermarkPlugin(string text = "© My Company")
        {
            watermarkText = text;
        }

        public string GetName() => "إضافة علامة مائية";

        public string GetDescription() => $"إضافة النص: {watermarkText}";

        public void Process(Image image)
        {
            Console.WriteLine($"💧 تطبيق البرنامج: {GetName()}");
            Console.WriteLine($"   النص: {watermarkText}");
            Console.WriteLine($"   على الصورة: {image.Name}");
            Console.WriteLine($"   ✅ تمت إضافة العلامة المائية!");
        }
    }

    /// <summary>
    /// إضافة ضغط الصورة
    /// </summary>
    public class CompressionPlugin : IImagePlugin
    {
        private int quality;

        public CompressionPlugin(int quality = 80)
        {
            this.quality = quality;
        }

        public string GetName() => "ضغط الصورة";

        public string GetDescription() => $"ضغط بجودة {quality}%";

        public void Process(Image image)
        {
            Console.WriteLine($"📦 تطبيق البرنامج: {GetName()}");
            Console.WriteLine($"   الجودة: {quality}%");
            Console.WriteLine($"   الحجم قبل: {image.Width * image.Height / 1024} KB");
            int compressedSize = (image.Width * image.Height * quality) / (100 * 1024);
            Console.WriteLine($"   الحجم بعد: {compressedSize} KB");
            Console.WriteLine($"   ✅ تم ضغط الصورة بنجاح!");
        }
    }

    /// <summary>
    /// إضافة تدوير الصورة
    /// </summary>
    public class RotatePlugin : IImagePlugin
    {
        private int degrees;

        public RotatePlugin(int deg = 90)
        {
            degrees = deg;
        }

        public string GetName() => $"تدوير الصورة {degrees}°";

        public string GetDescription() => $"تدوير الصورة بزاوية {degrees} درجة";

        public void Process(Image image)
        {
            Console.WriteLine($"🔄 تطبيق البرنامج: {GetName()}");
            Console.WriteLine($"   الأبعاد الأصلية: {image.Width}x{image.Height}");
            if (degrees == 90 || degrees == 270)
            {
                Console.WriteLine($"   الأبعاد الجديدة: {image.Height}x{image.Width}");
            }
            Console.WriteLine($"   ✅ تم تدوير الصورة بنجاح!");
        }
    }


    // ════════════════════════════════════════════════════════════
    // مدير الإضافات (Plugin Manager)
    // ════════════════════════════════════════════════════════════

    /// <summary>
    /// إدارة الإضافات المتاحة
    /// </summary>
    public class PluginManager
    {
        private List<IImagePlugin> plugins = new();

        public void RegisterPlugin(IImagePlugin plugin)
        {
            plugins.Add(plugin);
            Console.WriteLine($"✅ تم تسجيل الإضافة: {plugin.GetName()}");
        }

        public void ListAvailablePlugins()
        {
            Console.WriteLine("\n📋 الإضافات المتاحة:");
            Console.WriteLine("════════════════════════════════");
            for (int i = 0; i < plugins.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {plugins[i].GetName()}");
                Console.WriteLine($"   {plugins[i].GetDescription()}");
            }
        }

        public void ApplyPlugin(int index, Image image)
        {
            if (index >= 0 && index < plugins.Count)
            {
                plugins[index].Process(image);
            }
            else
            {
                Console.WriteLine("❌ الإضافة غير موجودة!");
            }
        }

        public void ApplyAllPlugins(Image image)
        {
            Console.WriteLine($"\n⚙️  تطبيق جميع الإضافات على: {image.Name}");
            Console.WriteLine("════════════════════════════════");
            foreach (var plugin in plugins)
            {
                plugin.Process(image);
                Console.WriteLine();
            }
        }
    }

    /// <summary>
    /// معالج الصور - يستخدم الإضافات
    /// </summary>
    public class ImageProcessor
    {
        private PluginManager pluginManager;

        public ImageProcessor(PluginManager manager)
        {
            pluginManager = manager;
        }

        public void ProcessImage(Image image, List<int> pluginIndices)
        {
            Console.WriteLine($"\n🖼️  معالجة الصورة: {image}");
            Console.WriteLine("════════════════════════════════");

            foreach (var index in pluginIndices)
            {
                pluginManager.ApplyPlugin(index, image);
                Console.WriteLine();
            }

            Console.WriteLine("✅ انتهت معالجة الصورة!");
        }
    }
}
