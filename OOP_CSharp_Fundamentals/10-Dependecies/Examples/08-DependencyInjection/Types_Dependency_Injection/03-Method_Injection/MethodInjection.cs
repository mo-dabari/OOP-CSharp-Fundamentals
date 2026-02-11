// ====================================
// 3️⃣ Method Injection - استخدام نادر جداً
// ====================================
namespace MethodInjection
{
    public interface IExportFormat
    {
        string Export(List<Product> products);
    }

    public class CsvExportFormat : IExportFormat
    {
        public string Export(List<Product> products)
        {
            return "CSV: " + string.Join(",", products.Select(p => p.Name));
        }
    }

    public class JsonExportFormat : IExportFormat
    {
        public string Export(List<Product> products)
        {
            return "JSON: [" + string.Join(",", products.Select(p => $"{{name:'{p.Name}'}}")) + "]";
        }
    }

    public class XmlExportFormat : IExportFormat
    {
        public string Export(List<Product> products)
        {
            return "XML: <products>" + string.Join("", products.Select(p => $"<product>{p.Name}</product>")) + "</products>";
        }
    }

    // ⚠️ Method Injection
    public class ReportService
    {
        public void GenerateReport(List<Product> products)
        {
            Console.WriteLine("Generating report...");
            // Business logic here
        }

        // ⚠️ Dependency يتم Inject مع كل استدعاء
        public string ExportReport(List<Product> products, IExportFormat format)
        {
            Console.WriteLine($"Exporting with {format.GetType().Name}");
            return format.Export(products);
        }
    }

    // ✅ متى نستخدم Method Injection:
    public class UseCases
    {
        public static void Demo()
        {
            Console.WriteLine("\n=== Method Injection Use Cases ===\n");

            var reportService = new ReportService();
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product A" },
                new Product { Id = 2, Name = "Product B" }
            };

            // ✅ الـ Format يختلف مع كل استدعاء
            var csv = reportService.ExportReport(products, new CsvExportFormat());
            Console.WriteLine(csv);

            var json = reportService.ExportReport(products, new JsonExportFormat());
            Console.WriteLine(json);

            var xml = reportService.ExportReport(products, new XmlExportFormat());
            Console.WriteLine(xml);

            Console.WriteLine("\n✅ استخدم Method Injection لما الـ Dependency تتغير مع كل Call\n");
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    // ❌ مشاكل Method Injection:
    // 1. تعقيد على الـ Client - لازم يجهّز الـ Dependency مع كل call
    // 2. Method signature ممكن تكبر
    // 3. مش مناسب للـ Business Logic العادي
}
