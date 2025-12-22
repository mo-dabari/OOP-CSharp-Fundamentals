/*
 * DigitalLibrarySystem.cs
 * ============================================
 * حالة واقعية: نظام مكتبة رقمية متطور
 * 
 * السيناريو:
 * ────────
 * مكتبة رقمية توفر:
 * - عدة أنواع محتوى (كتب، مقاطع فيديو، بودكاست)
 * - عدة طرق وصول (قراءة، تنزيل، بث مباشر)
 * - نظام توصيات ذكي
 * - تتبع قراءات المستخدمين
 * 
 * هذا يوضح الاستخدام الفعلي للـ Abstraction
 * في تطبيق معقد واقعي
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_CSharp_Fundamentals
{
    // ════════════════════════════════════════════════════════════
    // الواجهات الأساسية
    // ════════════════════════════════════════════════════════════
    
    /// <summary>
    /// واجهة للمحتوى الرقمي
    /// جميع أنواع المحتوى يجب أن تطبقها
    /// </summary>
    public interface IDigitalContent
    {
        // المعلومات الأساسية
        string GetTitle();
        string GetAuthor();
        string GetDescription();
        int GetDuration();  // بالدقائق
        
        // الوصول والتحميل
        void Display();
        byte[] Download();
        
        // التصنيف
        string GetContentType();
        List<string> GetCategories();
        
        // التقييم
        void AddRating(int rating);
        double GetAverageRating();
    }
    
    /// <summary>
    /// واجهة لطرق الوصول المختلفة
    /// </summary>
    public interface IAccessMethod
    {
        void Access(IDigitalContent content, User user);
        string GetAccessType();
    }
    
    /// <summary>
    /// واجهة لنظام التوصيات
    /// </summary>
    public interface IRecommendationEngine
    {
        List<IDigitalContent> GetRecommendations(User user);
        void UpdateUserProfile(User user);
    }
    
    
    // ════════════════════════════════════════════════════════════
    // نماذج البيانات
    // ════════════════════════════════════════════════════════════
    
    public class User
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public List<string> ReadingHistory { get; set; } = new();
        public List<int> Ratings { get; set; } = new();
        public string PreferredCategory { get; set; }
        
        public User(string id, string name)
        {
            UserId = id;
            Name = name;
        }
        
        public void AddToHistory(string contentTitle)
        {
            ReadingHistory.Add(contentTitle);
        }
    }
    
    
    // ════════════════════════════════════════════════════════════
    // أنواع المحتوى المختلفة
    // ════════════════════════════════════════════════════════════
    
    /// <summary>
    /// الكتاب الإلكتروني
    /// </summary>
    public class EBook : IDigitalContent
    {
        private string title;
        private string author;
        private string description;
        private int pageCount;
        private List<string> categories;
        private List<int> ratings;
        
        public EBook(string title, string author, string description, int pages)
        {
            this.title = title;
            this.author = author;
            this.description = description;
            this.pageCount = pages;
            this.categories = new List<string>();
            this.ratings = new List<int>();
        }
        
        public string GetTitle() => title;
        public string GetAuthor() => author;
        public string GetDescription() => description;
        public int GetDuration() => pageCount * 2;  // تقريبي: صفحة في دقيقتين
        public string GetContentType() => "كتاب إلكتروني";
        
        public List<string> GetCategories() => categories;
        
        public void AddCategory(string category) => categories.Add(category);
        
        public void Display()
        {
            Console.WriteLine($"📖 الكتاب: {title}");
            Console.WriteLine($"   المؤلف: {author}");
            Console.WriteLine($"   عدد الصفحات: {pageCount}");
        }
        
        public byte[] Download()
        {
            // محاكاة تنزيل الكتاب
            return new byte[pageCount * 1024];  // تقريبي
        }
        
        public void AddRating(int rating) => ratings.Add(rating);
        
        public double GetAverageRating()
            => ratings.Count > 0 ? ratings.Average() : 0;
    }
    
    /// <summary>
    /// مقطع فيديو
    /// </summary>
    public class VideoContent : IDigitalContent
    {
        private string title;
        private string author;
        private string description;
        private int durationInMinutes;
        private int resolution;  // 1080, 720, 480
        private List<string> categories;
        private List<int> ratings;
        
        public VideoContent(string title, string author, int duration, int resolution = 1080)
        {
            this.title = title;
            this.author = author;
            this.durationInMinutes = duration;
            this.resolution = resolution;
            this.description = "";
            this.categories = new List<string>();
            this.ratings = new List<int>();
        }
        
        public string GetTitle() => title;
        public string GetAuthor() => author;
        public string GetDescription() => description;
        public int GetDuration() => durationInMinutes;
        public string GetContentType() => $"فيديو ({resolution}p)";
        public List<string> GetCategories() => categories;
        
        public void SetDescription(string desc) => description = desc;
        public void AddCategory(string category) => categories.Add(category);
        
        public void Display()
        {
            Console.WriteLine($"🎬 الفيديو: {title}");
            Console.WriteLine($"   المدة: {durationInMinutes} دقيقة");
            Console.WriteLine($"   الجودة: {resolution}p");
        }
        
        public byte[] Download()
        {
            // تنزيل الفيديو
            return new byte[durationInMinutes * 1024 * 64];  // تقريبي
        }
        
        public void AddRating(int rating) => ratings.Add(rating);
        public double GetAverageRating() 
            => ratings.Count > 0 ? ratings.Average() : 0;
    }
    
    /// <summary>
    /// البودكاست
    /// </summary>
    public class Podcast : IDigitalContent
    {
        private string title;
        private string host;
        private string description;
        private int episodeDuration;
        private int episodeNumber;
        private List<string> categories;
        private List<int> ratings;
        
        public Podcast(string title, string host, int duration, int episodeNum)
        {
            this.title = title;
            this.host = host;
            this.episodeDuration = duration;
            this.episodeNumber = episodeNum;
            this.description = "";
            this.categories = new List<string>();
            this.ratings = new List<int>();
        }
        
        public string GetTitle() => $"{title} - الحلقة {episodeNumber}";
        public string GetAuthor() => host;
        public string GetDescription() => description;
        public int GetDuration() => episodeDuration;
        public string GetContentType() => "بودكاست";
        public List<string> GetCategories() => categories;
        
        public void SetDescription(string desc) => description = desc;
        public void AddCategory(string category) => categories.Add(category);
        
        public void Display()
        {
            Console.WriteLine($"🎙️  البودكاست: {title}");
            Console.WriteLine($"   المضيف: {host}");
            Console.WriteLine($"   الحلقة: {episodeNumber}");
            Console.WriteLine($"   المدة: {episodeDuration} دقيقة");
        }
        
        public byte[] Download()
        {
            return new byte[episodeDuration * 1024 * 8];  // تقريبي
        }
        
        public void AddRating(int rating) => ratings.Add(rating);
        public double GetAverageRating()
            => ratings.Count > 0 ? ratings.Average() : 0;
    }
    
    
    // ════════════════════════════════════════════════════════════
    // طرق الوصول
    // ════════════════════════════════════════════════════════════
    
    /// <summary>
    /// القراءة المباشرة (بدون تنزيل)
    /// </summary>
    public class StreamAccess : IAccessMethod
    {
        public void Access(IDigitalContent content, User user)
        {
            Console.WriteLine($"▶️  بث مباشر: {content.GetTitle()}");
            Console.WriteLine($"   المستخدم: {user.Name}");
            user.AddToHistory(content.GetTitle());
        }
        
        public string GetAccessType() => "بث مباشر";
    }
    
    /// <summary>
    /// التنزيل الكامل
    /// </summary>
    public class DownloadAccess : IAccessMethod
    {
        public void Access(IDigitalContent content, User user)
        {
            Console.WriteLine($"⬇️  تنزيل: {content.GetTitle()}");
            var data = content.Download();
            Console.WriteLine($"   تم تنزيل {data.Length / 1024} كيلوبايت");
            user.AddToHistory(content.GetTitle());
        }
        
        public string GetAccessType() => "تنزيل";
    }
    
    /// <summary>
    /// النسخة المحفوظة (للوصول لاحقاً)
    /// </summary>
    public class OfflineAccess : IAccessMethod
    {
        public void Access(IDigitalContent content, User user)
        {
            Console.WriteLine($"💾 حفظ للوصول اللاحق: {content.GetTitle()}");
            Console.WriteLine($"   متاح في المكتبة الشخصية");
            user.AddToHistory(content.GetTitle());
        }
        
        public string GetAccessType() => "وصول بلا اتصال";
    }
    
    
    // ════════════════════════════════════════════════════════════
    // نظام التوصيات
    // ════════════════════════════════════════════════════════════
    
    public class SmartRecommendationEngine : IRecommendationEngine
    {
        private List<IDigitalContent> allContent;
        
        public SmartRecommendationEngine(List<IDigitalContent> content)
        {
            allContent = content;
        }
        
        public List<IDigitalContent> GetRecommendations(User user)
        {
            var recommended = new List<IDigitalContent>();
            
            if (string.IsNullOrEmpty(user.PreferredCategory))
            {
                // أول مرة - عرض محتوى متنوع
                return allContent.Take(3).ToList();
            }
            
            // التوصيات بناءً على التفضيل
            foreach (var content in allContent)
            {
                if (content.GetCategories().Contains(user.PreferredCategory) &&
                    !user.ReadingHistory.Contains(content.GetTitle()))
                {
                    recommended.Add(content);
                }
            }
            
            return recommended;
        }
        
        public void UpdateUserProfile(User user)
        {
            // تحديث ملف المستخدم بناءً على السلوك
            if (user.ReadingHistory.Count > 0)
            {
                // العثور على أكثر تصنيف متكرر
                var categories = new Dictionary<string, int>();
                foreach (var contentTitle in user.ReadingHistory)
                {
                    var content = allContent
                        .FirstOrDefault(c => c.GetTitle() == contentTitle);
                    if (content != null)
                    {
                        foreach (var cat in content.GetCategories())
                        {
                            if (categories.ContainsKey(cat))
                                categories[cat]++;
                            else
                                categories[cat] = 1;
                        }
                    }
                }
                
                if (categories.Count > 0)
                {
                    user.PreferredCategory = 
                        categories.OrderByDescending(x => x.Value).First().Key;
                }
            }
        }
    }
    
    
    // ════════════════════════════════════════════════════════════
    // المكتبة الرقمية
    // ════════════════════════════════════════════════════════════
    
    public class DigitalLibrary
    {
        private List<IDigitalContent> contents;
        private List<User> users;
        private IRecommendationEngine recommendationEngine;
        
        public DigitalLibrary()
        {
            contents = new List<IDigitalContent>();
            users = new List<User>();
            recommendationEngine = new SmartRecommendationEngine(contents);
        }
        
        public void AddContent(IDigitalContent content)
        {
            contents.Add(content);
            Console.WriteLine($"✅ تم إضافة: {content.GetTitle()}");
        }
        
        public void RegisterUser(User user)
        {
            users.Add(user);
            Console.WriteLine($"✅ تم تسجيل المستخدم: {user.Name}");
        }
        
        public void AccessContent(User user, IDigitalContent content, IAccessMethod method)
        {
            Console.WriteLine($"\n📚 {method.GetAccessType()}:");
            method.Access(content, user);
        }
        
        public void PrintRecommendations(User user)
        {
            recommendationEngine.UpdateUserProfile(user);
            var recommendations = recommendationEngine.GetRecommendations(user);
            
            Console.WriteLine($"\n💡 التوصيات لـ {user.Name}:");
            Console.WriteLine("────────────────────────────────");
            foreach (var content in recommendations)
            {
                Console.WriteLine($"  • {content.GetTitle()} ({content.GetContentType()})");
                Console.WriteLine($"    التقييم: {content.GetAverageRating():F1}");
            }
        }
        
        public void PrintUserProfile(User user)
        {
            Console.WriteLine($"\n👤 ملف المستخدم: {user.Name}");
            Console.WriteLine("════════════════════════════════");
            Console.WriteLine($"معرف المستخدم: {user.UserId}");
            Console.WriteLine($"عدد المقالات المقروءة: {user.ReadingHistory.Count}");
            if (!string.IsNullOrEmpty(user.PreferredCategory))
                Console.WriteLine($"التصنيف المفضل: {user.PreferredCategory}");
            
            if (user.ReadingHistory.Count > 0)
            {
                Console.WriteLine("السجل:");
                foreach (var item in user.ReadingHistory)
                    Console.WriteLine($"  • {item}");
            }
        }
        
        public void PrintCatalog()
        {
            Console.WriteLine("\n📚 فهرس المكتبة:");
            Console.WriteLine("════════════════════════════════");
            foreach (var content in contents)
            {
                Console.WriteLine($"  • {content.GetTitle()}");
                Console.WriteLine($"    النوع: {content.GetContentType()}");
                Console.WriteLine($"    المؤلف: {content.GetAuthor()}");
            }
        }
    }
    
    
    // ════════════════════════════════════════════════════════════
    // Program - تشغيل النظام
    // ════════════════════════════════════════════════════════════
    
    class LibraryProgram
    {
        static void RunLibrarySystem()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            Console.WriteLine("╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║          نظام مكتبة رقمية متطور - حالة واقعية          ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝\n");
            
            // إنشاء المكتبة
            var library = new DigitalLibrary();
            
            // ─────────────────────────────────────────
            // إضافة المحتوى
            // ─────────────────────────────────────────
            Console.WriteLine("📖 إضافة المحتوى:");
            Console.WriteLine("════════════════════════════════\n");
            
            var book1 = new EBook("تعلم البرمجة بـ C#", "أحمد محمود", 
                "شرح شامل للبرمجة", 450);
            book1.AddCategory("البرمجة");
            book1.AddCategory("تعليم");
            library.AddContent(book1);
            
            var book2 = new EBook("الذكاء الاصطناعي", "فاطمة علي",
                "مقدمة للذكاء الاصطناعي", 350);
            book2.AddCategory("تكنولوجيا");
            book2.AddCategory("تعليم");
            library.AddContent(book2);
            
            var video = new VideoContent("شرح الخوارزميات", "محمد حسن", 120, 1080);
            video.SetDescription("شرح مفصل للخوارزميات الأساسية");
            video.AddCategory("البرمجة");
            library.AddContent(video);
            
            var podcast = new Podcast("بودكاست التقنية", "سارة أحمد", 45, 1);
            podcast.SetDescription("نقاش عن أحدث التطورات التقنية");
            podcast.AddCategory("تكنولوجيا");
            library.AddContent(podcast);
            
            library.PrintCatalog();
            
            // ─────────────────────────────────────────
            // تسجيل المستخدمين
            // ─────────────────────────────────────────
            Console.WriteLine("\n\n👥 تسجيل المستخدمين:");
            Console.WriteLine("════════════════════════════════\n");
            
            var user1 = new User("001", "علي محمد");
            var user2 = new User("002", "نور الدين");
            
            library.RegisterUser(user1);
            library.RegisterUser(user2);
            
            // ─────────────────────────────────────────
            // الوصول للمحتوى بطرق مختلفة
            // ─────────────────────────────────────────
            Console.WriteLine("\n\n📚 الوصول للمحتوى:");
            Console.WriteLine("════════════════════════════════\n");
            
            library.AccessContent(user1, book1, new StreamAccess());
            library.AccessContent(user1, video, new DownloadAccess());
            library.AccessContent(user2, book2, new StreamAccess());
            library.AccessContent(user2, podcast, new OfflineAccess());
            
            // ─────────────────────────────────────────
            // التقييم والتوصيات
            // ─────────────────────────────────────────
            Console.WriteLine("\n\n⭐ التقييمات:");
            Console.WriteLine("════════════════════════════════\n");
            
            book1.AddRating(5);
            book1.AddRating(4);
            video.AddRating(5);
            
            Console.WriteLine($"تقييم '{book1.GetTitle()}': {book1.GetAverageRating():F1}/5");
            Console.WriteLine($"تقييم '{video.GetTitle()}': {video.GetAverageRating():F1}/5");
            
            // ─────────────────────────────────────────
            // التوصيات الذكية
            // ─────────────────────────────────────────
            library.PrintRecommendations(user1);
            library.PrintRecommendations(user2);
            
            // ─────────────────────────────────────────
            // ملفات المستخدمين
            // ─────────────────────────────────────────
            library.PrintUserProfile(user1);
            library.PrintUserProfile(user2);
            
            // ─────────────────────────────────────────
            // الملخص
            // ─────────────────────────────────────────
            Console.WriteLine("\n\n═══════════════════════════════════════════════════════════");
            Console.WriteLine("  ✨ مميزات Abstraction في هذا النظام:");
            Console.WriteLine("═══════════════════════════════════════════════════════════\n");
            
            Console.WriteLine("""
            1️⃣  تعدد الأنواع (Polymorphism):
                - محتوى واحد = فئات مختلفة (كتاب، فيديو، بودكاست)
                - معالجة موحدة لجميع الأنواع
            
            2️⃣  المرونة (Flexibility):
                - إضافة نوع محتوى جديد سهلة
                - إضافة طريقة وصول جديدة سهلة
            
            3️⃣  الصيانة (Maintainability):
                - كل نوع مسؤول عن نفسه
                - سهل العثور على الأخطاء
            
            4️⃣  إعادة الاستخدام (Reusability):
                - المكتبة تعمل مع أي محتوى
                - نفس النمط في نظام آخر
            
            5️⃣  التوسع (Extensibility):
                - نظام التوصيات يعمل تلقائياً
                - إضافة ميزات جديدة بدون تعديل القديم
            """);
            
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("  ✅ انتهت حالة المكتبة الرقمية");
            Console.WriteLine("═══════════════════════════════════════════════════════════\n");
        }
    }
}