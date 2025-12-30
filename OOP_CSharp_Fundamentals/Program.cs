
using Encapsulation.RealWorldScenarios;
using Inheritance.RealWorldScenarios;
using Interfaces.Examples;
using Interfaces.Exercises;
using Polymorphism.Examples.Advanced;
using Polymorphism.Exercises;
using Polymorphism.RealWorldScenarios.OOP_CSharp_Fundamentals;

namespace OOP_CSharp_Fundamentals
{
    class Program
    {
        static void Main(string[] args)
        {
            #region (Encapsulation)
            #region Exercises
            /**
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║           تمارين عملية على الكبسولة (Encapsulation)    ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝\n");

            // ─────────────────────────────────────────
            // التمرين 1: نظام الحسابات البنكية
            // ─────────────────────────────────────────
            Console.WriteLine("\n" + new string('═', 60));
            Console.WriteLine("  التمرين 1: نظام إدارة الحسابات البنكية");
            Console.WriteLine(new string('═', 60) + "\n");

            try
            {
                var customer = new Encapsulation.Exercises.Customer(1, "أحمد محمود", "ahmed@example.com");
                Console.WriteLine($"✅ {customer}");

                var account = new Encapsulation.Exercises.BankAccount("1001-5000-2024", 5000);
                Console.WriteLine($"✅ حساب جديد: {account.AccountNumber}\n");

                account.PrintBalance();
                account.Deposit(2000);
                account.Withdraw(1000);
                account.PrintBalance();
                account.PrintTransactions();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ خطأ: {ex.Message}");
            }

            // ─────────────────────────────────────────
            // التمرين 2: نظام الموارد البشرية
            // ─────────────────────────────────────────
            Console.WriteLine("\n\n" + new string('═', 60));
            Console.WriteLine("  التمرين 2: نظام إدارة الموارد البشرية");
            Console.WriteLine(new string('═', 60) + "\n");

            try
            {
                var dept = new Encapsulation.Exercises.Department("تطوير الويب");

                var emp1 = new Encapsulation.Exercises.Employee("فاطمة علي", 5000, "123-45-6789");
                var emp2 = new Encapsulation.Exercises.Employee("محمد حسن", 5500, "987-65-4321");
                var emp3 = new Encapsulation.Exercises.Employee("سارة أحمد", 4800, "555-55-5555");

                dept.AddEmployee(emp1);
                dept.AddEmployee(emp2);
                dept.AddEmployee(emp3);

                dept.DisplayAllEmployees();
                dept.PrintPayrollReport();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ خطأ: {ex.Message}");
            }

            // ─────────────────────────────────────────
            // التمرين 3: نظام الأمان وكلمات المرور
            // ─────────────────────────────────────────
            Console.WriteLine("\n\n" + new string('═', 60));
            Console.WriteLine("  التمرين 3: نظام الأمان وكلمات المرور");
            Console.WriteLine(new string('═', 60) + "\n");

            try
            {
                var user = new Encapsulation.Exercises.User("ali_hassan", "Strong@Pass123");
                Console.WriteLine($"✅ تم إنشاء مستخدم: {user.Username}\n");

                Console.WriteLine("🔐 محاولات الدخول:");
                user.VerifyPassword("Wrong123!");
                user.VerifyPassword("WrongPass@1");
                user.VerifyPassword("Wrong@Pass2");
                user.VerifyPassword("Strong@Pass123");

                user.PrintLoginHistory();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ خطأ: {ex.Message}");
            }

            Console.WriteLine("\n" + new string('═', 60));
            Console.WriteLine("  ✅ انتهت جميع التمارين");
            Console.WriteLine(new string('═', 60) + "\n");
            **/
            #endregion

            #region Real World Scenarios
            /**
                Console.OutputEncoding = System.Text.Encoding.UTF8;

                Console.WriteLine("╔═══════════════════════════════════════════════════════════╗");
                Console.WriteLine("║       نظام مكتبة رقمية متكامل - حالة واقعية            ║");
                Console.WriteLine("╚═══════════════════════════════════════════════════════════╝\n");

                // ─────────────────────────────────────────
                // إنشاء النظام
                // ─────────────────────────────────────────
                var library = new LibraryManagementSystem();

                // إضافة الكتب
                Console.WriteLine("📚 إضافة الكتب:");
                Console.WriteLine("════════════════════════════════\n");

                library.AddBook(new Book("978-9760429649", "نزيف الحجر", "جمال الغيطاني", 1983, 5, 45));
                library.AddBook(new Book("978-9776139159", "أولاد حارتنا", "نجيب محفوظ", 1959, 8, 50));
                library.AddBook(new Book("978-9777621474", "الخيميائي", "باولو كويلو", 1988, 10, 40));
                library.AddBook(new Book("978-9770913169", "1984", "جورج أورويل", 1949, 7, 60));

                // إضافة الأعضاء
                Console.WriteLine("\n\n👥 تسجيل الأعضاء:");
                Console.WriteLine("════════════════════════════════\n");

                var member1 = new Member(1, "أحمد محمود", "ahmed@example.com");
                var member2 = new Member(2, "فاطمة علي", "fatima@example.com");
                var member3 = new Member(3, "محمد حسن", "mohammed@example.com");

                library.AddMember(member1);
                library.AddMember(member2);
                library.AddMember(member3);

                // ─────────────────────────────────────────
                // عمليات الاستعارة
                // ─────────────────────────────────────────
                Console.WriteLine("\n\n📖 عمليات الاستعارة:");
                Console.WriteLine("════════════════════════════════\n");

                library.BorrowBook(1, "978-9760429649");
                library.BorrowBook(1, "978-9777621474");
                library.BorrowBook(2, "978-9776139159");
                library.BorrowBook(3, "978-9770913169");

                // ─────────────────────────────────────────
                // العرض والتقارير
                // ─────────────────────────────────────────
                library.PrintLibraryStatistics();
                library.PrintAllBooks();

                // ─────────────────────────────────────────
                // الإرجاع
                // ─────────────────────────────────────────
                Console.WriteLine("\n\n📤 إرجاع الكتب:");
                Console.WriteLine("════════════════════════════════\n");

                library.ReturnBook(1, "978-9760429649");
                library.ReturnBook(2, "978-9776139159");

                // ─────────────────────────────────────────
                // سجلات الأعضاء
                // ─────────────────────────────────────────
                library.PrintMemberBorrowHistory(1);
                library.PrintAllMembers();

                // ─────────────────────────────────────────
                // الملخص
                // ─────────────────────────────────────────
                Console.WriteLine("\n\n═══════════════════════════════════════════════════════════");
                Console.WriteLine("  ✨ مميزات Encapsulation في المكتبة:");
                Console.WriteLine("═══════════════════════════════════════════════════════════\n");

                Console.WriteLine("""
                1️⃣  حماية البيانات الحساسة:
                    • ISBN و Title و Author محمية
                    • Balance والبيانات الشخصية محمية
                    • لا يمكن تعديل مباشرة من الخارج

                2️⃣  Validation قوي:
                    • التحقق من ISBN عند الإنشاء
                    • التحقق من الأسعار والأرقام
                    • التحقق من البريد الإلكتروني

                3️⃣  العمليات المنطقية:
                    • BorrowCopy() تقلل النسخ تلقائياً
                    • ReturnCopy() تزيد النسخ تلقائياً
                    • العمليات آمنة وموثوقة

                4️⃣  حسابات الغرامات:
                    • احتساب تلقائي للتأخير
                    • إضافة للرصيد تلقائياً
                    • بيانات دقيقة وآمنة

                5️⃣  التقارير الشاملة:
                    • إحصائيات المكتبة
                    • سجلات الأعضاء
                    • الكتب المتأخرة
                    • كل البيانات محمية

                6️⃣  الانعزال الكامل:
                    • التطبيق لا يعرف التفاصيل الداخلية
                    • تغيير الداخل لا يؤثر على الخارج
                    • نظام مرن وآمن

                هذا هو الـ Encapsulation الحقيقي!
                """);

                Console.WriteLine("═══════════════════════════════════════════════════════════");
                Console.WriteLine("  ✅ انتهت الحالة الواقعية");
                Console.WriteLine("═══════════════════════════════════════════════════════════\n");
            **/
            #endregion
            #endregion

            #region (Inheritance)
            #region Real World Scenarios => StudentManagementSystem
            /**
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║      نظام إدارة الطلاب والدرجات - حالة واقعية متقدمة    ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝\n");

            // إنشاء النظام
            var university = new UniversityManagementSystem();

            // ─────────────────────────────────────────
            // إضافة المواد الدراسية
            // ─────────────────────────────────────────
            Console.WriteLine("📚 إضافة المواد الدراسية:");
            Console.WriteLine("════════════════════════════════\n");

            var cs101 = new Course("CS101", "مقدمة البرمجة", 3);
            var cs201 = new Course("CS201", "هياكل البيانات", 4);
            var math101 = new Course("MATH101", "التفاضل والتكامل", 4);
            var physics101 = new Course("PHYS101", "الفيزياء الأساسية", 3);

            university.AddCourse(cs101);
            university.AddCourse(cs201);
            university.AddCourse(math101);
            university.AddCourse(physics101);

            // ─────────────────────────────────────────
            // تسجيل الطلاب
            // ─────────────────────────────────────────
            Console.WriteLine("\n\n👥 تسجيل الطلاب:");
            Console.WriteLine("════════════════════════════════\n");

            // طالب عام
            var undergrad = new Undergraduate("20230001", "أحمد محمود",
                "ahmed@uni.edu", 2, "علوم الحاسوب");
            university.AddStudent(undergrad);

            // طالب دراسات عليا
            var grad = new Graduate("20221001", "فاطمة علي",
                "fatima@uni.edu", Graduate.Degrees.Master, "الذكاء الاصطناعي");
            university.AddStudent(grad);

            // طالب تبادل
            var exchange = new Exchange("20240001", "محمد حسن",
                "mohammed@exchange.edu", "مصر", "جامعة القاهرة", 6);
            university.AddStudent(exchange);

            // ─────────────────────────────────────────
            // عمليات أكاديمية
            // ─────────────────────────────────────────
            Console.WriteLine("\n\n📝 العمليات الأكاديمية:");
            Console.WriteLine("════════════════════════════════\n");

            // تسجيل المواد
            undergrad.AddCourse(cs101);
            undergrad.AddCourse(math101);

            grad.AddCourse(cs201);

            exchange.AddCourse(cs101);
            exchange.AddCourse(physics101);

            // إضافة الدرجات
            Console.WriteLine("\n📊 إضافة الدرجات:");
            undergrad.AddGrade(cs101, 85);
            undergrad.AddGrade(math101, 90);

            grad.AddGrade(cs201, 92);

            exchange.AddGrade(cs101, 88);
            exchange.AddGrade(physics101, 82);

            // ─────────────────────────────────────────
            // عمليات خاصة
            // ─────────────────────────────────────────
            Console.WriteLine("\n\n🎓 عمليات خاصة:");
            Console.WriteLine("════════════════════════════════\n");

            undergrad.CompleteInternship(40);

            grad.PublishResearch("تطبيق الشبكات العصبية");
            grad.PresentAtConference("مؤتمر الذكاء الاصطناعي");

            exchange.GetCultureExchange();

            // ─────────────────────────────────────────
            // التقارير والإحصائيات
            // ─────────────────────────────────────────
            university.DisplayAllStudents();
            university.PrintStudentsByType();
            university.PrintAcademicReport();
            university.PrintTuitionReport();
            university.PrintStatistics();

            // ─────────────────────────────────────────
            // السجلات الأكاديمية
            // ─────────────────────────────────────────
            Console.WriteLine("\n\n📚 السجلات الأكاديمية:");
            Console.WriteLine("════════════════════════════════");

            undergrad.PrintTranscript();
            grad.PrintTranscript();
            exchange.PrintTranscript();

            // ملخص
            Console.WriteLine("\n════════════════════════════════════════════════════════════");
            Console.WriteLine("  ✨ الفوائد التي حققناها:");
            Console.WriteLine("════════════════════════════════════════════════════════════\n");

            Console.WriteLine("""
            1️⃣  تسلسل هرمي منطقي:
                Student (أب)
                ├── UndergraduateStudent
                ├── GraduateStudent
                └── ExchangeStudent

            2️⃣  سلوك مختلف لكل نوع:
                - Undergraduate: رسوم تخفيضية حسب المعدل
                - Graduate: رسوم أعلى، معدل أعلى
                - Exchange: معفى من الرسوم

            3️⃣  عمليات خاصة:
                - Undergraduate: التدريب والتخرج
                - Graduate: الأبحاث والمؤتمرات
                - Exchange: التبادل الثقافي

            4️⃣  إدارة موحدة:
                - قائمة واحدة لكل الأنواع
                - تقارير شاملة لكل نوع
                - إحصائيات عامة

            5️⃣  مرونة عالية:
                - إضافة نوع طالب جديد سهل
                - كل نوع له متطلبات خاصة
                - لا تضارب في الكود
            """);

            Console.WriteLine("════════════════════════════════════════════════════════════");
            Console.WriteLine("  ✅ انتهت الحالة الواقعية");
            Console.WriteLine("════════════════════════════════════════════════════════════\n");
            **/
            #endregion
            #endregion

            #region (Interfaces)
            #region Example => Basic Interface
            /**
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("  مثال بسيط على الواجهات (Interfaces)");
            Console.WriteLine("═══════════════════════════════════════════════════════════\n");

            // ─────────────────────────────────────────
            // 1. واجهة واحدة
            // ─────────────────────────────────────────
            Console.WriteLine("1️⃣  واجهة واحدة (IAnimal):");
            Console.WriteLine("────────────────────────────────────\n");

            IAnimal dog = new Dog();
            dog.MakeSound();
            Console.WriteLine($"النوع: {dog.GetSpecies()}");

            // ─────────────────────────────────────────
            // 2. الوراثة المتعددة من Interfaces
            // ─────────────────────────────────────────
            Console.WriteLine("\n\n2️⃣  الوراثة المتعددة (IAnimal + IMovable):");
            Console.WriteLine("────────────────────────────────────\n");

            var dog2 = new Dog();
            dog2.MakeSound();
            dog2.Move();
            Console.WriteLine($"السرعة: {dog2.GetSpeed()} كم/س");

            // ─────────────────────────────────────────
            // 3. 3 واجهات (Bird)
            // ─────────────────────────────────────────
            Console.WriteLine("\n\n3️⃣  3 واجهات (IAnimal + IMovable + IFlying):");
            Console.WriteLine("────────────────────────────────────\n");

            var bird = new Bird();
            bird.MakeSound();
            bird.Move();
            bird.TakeOff();
            Console.WriteLine($"الارتفاع: {bird.GetAltitude()} متر");
            bird.Land();

            // ─────────────────────────────────────────
            // 4. السباحة (Fish + Duck)
            // ─────────────────────────────────────────
            Console.WriteLine("\n\n4️⃣  واجهة السباحة (ISwimmable):");
            Console.WriteLine("────────────────────────────────────\n");

            var fish = new Fish();
            fish.MakeSound();
            fish.Swim();

            var duck = new Duck();
            duck.MakeSound();
            duck.Move();
            duck.TakeOff();
            duck.Swim();

            // ─────────────────────────────────────────
            // 5. Polymorphism مع Interfaces
            // ─────────────────────────────────────────
            Console.WriteLine("\n\n5️⃣  Polymorphism - قائمة من IAnimal:");
            Console.WriteLine("────────────────────────────────────\n");

            var zoo = new Zoo();
            zoo.AddAnimal(new Dog());
            zoo.AddAnimal(new Bird());
            zoo.AddAnimal(new Fish());
            zoo.AddAnimal(new Duck());

            zoo.MakeAllSounds();
            zoo.ShowAllSpecies();

            // ─────────────────────────────────────────
            // 6. Polymorphism مع IMovable
            // ─────────────────────────────────────────
            Console.WriteLine("\n\n6️⃣  Polymorphism - قائمة من IMovable:");
            Console.WriteLine("────────────────────────────────────\n");

            var traffic = new TrafficController();
            traffic.AddMovable(new Dog());
            traffic.AddMovable(new Car());
            traffic.AddMovable(new Bird());
            traffic.AddMovable(new Airplane());

            traffic.MoveAll();
            traffic.ShowSpeeds();

            // ─────────────────────────────────────────
            // 7. Polymorphism مع IFlying
            // ─────────────────────────────────────────
            Console.WriteLine("\n\n7️⃣  Polymorphism - قائمة من IFlying:");
            Console.WriteLine("────────────────────────────────────\n");

            var airControl = new AirTrafficControl();
            airControl.AddFlyer(new Bird());
            airControl.AddFlyer(new Duck());
            airControl.AddFlyer(new Airplane());

            airControl.TakeOffAll();
            airControl.LandAll();

            // ─────────────────────────────────────────
            // الملخص
            // ─────────────────────────────────────────
            Console.WriteLine("\n\n═══════════════════════════════════════════════════════════");
            Console.WriteLine("  ✨ الفوائد:");
            Console.WriteLine("═══════════════════════════════════════════════════════════\n");

            Console.WriteLine("""
            1️⃣  الوراثة المتعددة:
                - كلب يطبق IAnimal و IMovable
                - بطة تطبق 4 واجهات!

            2️⃣  المرونة الكبيرة:
                - سيارة و طائرة تطبق IMovable
                - لكنها ليست حيوانات
                - حرية كاملة في التصميم

            3️⃣  Polymorphism الفعال:
                - قائمة من IAnimal = جميع الحيوانات
                - قائمة من IMovable = جميع المتحركات
                - كود واحد، أنواع مختلفة

            4️⃣  فصل المخاوف:
                - IAnimal للحيوان فقط
                - IMovable للحركة فقط
                - كل واجهة لها مسؤولية واحدة

            5️⃣  سهولة الإضافة:
                - حيوان جديد؟ فئة جديدة فقط
                - توزن الواجهات المناسبة
                - لا تعديل على الكود الموجود
            """);

            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("  ✅ انتهى المثال");
            Console.WriteLine("═══════════════════════════════════════════════════════════\n");
            **/
            #endregion

            #region Example => Dependency Injection
            /**
                Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("  Dependency Injection مع Interfaces");
            Console.WriteLine("═══════════════════════════════════════════════════════════\n");

            // ─────────────────────────────────────────
            // 1. التطبيق 1: استخدام In-Memory Repository
            // ─────────────────────────────────────────
            Console.WriteLine("1️⃣  تطبيق 1: بدون قاعدة بيانات (In-Memory)");
            Console.WriteLine("────────────────────────────────────\n");

            var service1 = new UserService(
                new InMemoryUserRepository(),
                new SmtpEmailSender(),
                new ConsoleLogger(),
                new SimpleEncryption()
            );

            var user1 = new User { Id = 1, Name = "أحمد", Email = "ahmed@test.com", Password = "123456" };
            service1.RegisterUser(user1);

            service1.PrintAllUsers();

            // ─────────────────────────────────────────
            // 2. التطبيق 2: استخدام SQL Repository
            // ─────────────────────────────────────────
            Console.WriteLine("\n\n2️⃣  تطبيق 2: مع قاعدة بيانات SQL");
            Console.WriteLine("────────────────────────────────────\n");

            var service2 = new UserService(
                new SqlUserRepository(),
                new SmtpEmailSender(),
                new FileLogger(),
                new AdvancedEncryption()
            );

            var user2 = new User { Id = 2, Name = "فاطمة", Email = "fatima@test.com", Password = "securepass" };
            service2.RegisterUser(user2);

            // ─────────────────────────────────────────
            // 3. التطبيق 3: استخدام MongoDB
            // ─────────────────────────────────────────
            Console.WriteLine("\n\n3️⃣  تطبيق 3: مع MongoDB");
            Console.WriteLine("────────────────────────────────────\n");

            var service3 = new UserService(
                new MongoUserRepository(),
                new SmtpEmailSender(),
                new ConsoleLogger(),
                new SimpleEncryption()
            );

            var user3 = new User { Id = 3, Name = "محمد", Email = "mohammed@test.com", Password = "password123" };
            service3.RegisterUser(user3);

            // ─────────────────────────────────────────
            // 4. التطبيق 4: للاختبار (Mock Objects)
            // ─────────────────────────────────────────
            Console.WriteLine("\n\n4️⃣  تطبيق 4: للاختبار (بدون بريد حقيقي)");
            Console.WriteLine("────────────────────────────────────\n");

            var service4 = new UserService(
                new InMemoryUserRepository(),
                new MockEmailSender(),      // بريد وهمي!
                new ConsoleLogger(),
                new SimpleEncryption()
            );

            var user4 = new User { Id = 4, Name = "سارة", Email = "sara@test.com", Password = "test123" };
            service4.RegisterUser(user4);

            // ─────────────────────────────────────────
            // الملخص
            // ─────────────────────────────────────────
            Console.WriteLine("\n\n═══════════════════════════════════════════════════════════");
            Console.WriteLine("  ✨ الفوائد:");
            Console.WriteLine("═══════════════════════════════════════════════════════════\n");

            Console.WriteLine("""
            1️⃣  تغيير التطبيق بدون تعديل الكود:
                - نفس UserService مع تطبيقات مختلفة
                - Repository: In-Memory → SQL → MongoDB
                - EmailSender: SMTP → Mock
                - Logger: Console → File

            2️⃣  سهولة الاختبار:
                - MockEmailSender بدل البريد الحقيقي
                - InMemoryRepository بدل قاعدة البيانات
                - لا حاجة لخوادم حقيقية

            3️⃣  المرونة الكاملة:
                - إضافة تطبيق جديد سهل جداً
                - لا تعديل في UserService
                - فقط واجهة جديدة

            4️⃣  فصل المخاوف:
                - UserService لا تعرف التطبيق
                - كل تطبيق مستقل تماماً
                - تغيير سهل وآمن

            5️⃣  قابل للتوسع:
                - إضافة Logger جديد؟ فئة جديدة
                - Repository جديد؟ فئة جديدة
                - لا مشاكل، لا تضارب
            """);

            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("  ✅ انتهى المثال");
            Console.WriteLine("═══════════════════════════════════════════════════════════\n");
            **/
            #endregion

            #region Example => Plugin System
            /**
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("  نظام الإضافات (Plugin System) - مثال واقعي");
            Console.WriteLine("═══════════════════════════════════════════════════════════\n");

            // إنشاء مدير الإضافات
            var pluginManager = new PluginManager();

            // ─────────────────────────────────────────
            // تسجيل الإضافات
            // ─────────────────────────────────────────
            Console.WriteLine("📦 تسجيل الإضافات:");
            Console.WriteLine("════════════════════════════════\n");

            pluginManager.RegisterPlugin(new BlackAndWhitePlugin());
            pluginManager.RegisterPlugin(new ResizePlugin(1.5));
            pluginManager.RegisterPlugin(new FilterPlugin("Sepia"));
            pluginManager.RegisterPlugin(new WatermarkPlugin("© My Company"));
            pluginManager.RegisterPlugin(new CompressionPlugin(80));
            pluginManager.RegisterPlugin(new RotatePlugin(90));

            // ─────────────────────────────────────────
            // عرض الإضافات المتاحة
            // ─────────────────────────────────────────
            pluginManager.ListAvailablePlugins();

            // ─────────────────────────────────────────
            // إنشاء صورة
            // ─────────────────────────────────────────
            var image = new Image("landscape.jpg", 1920, 1080, "JPEG");

            // ─────────────────────────────────────────
            // 1. تطبيق إضافة واحدة
            // ─────────────────────────────────────────
            Console.WriteLine("\n\n1️⃣  تطبيق إضافة واحدة:");
            Console.WriteLine("════════════════════════════════\n");

            pluginManager.ApplyPlugin(0, image);  // Black and White

            // ─────────────────────────────────────────
            // 2. تطبيق عدة إضافات
            // ─────────────────────────────────────────
            Console.WriteLine("\n\n2️⃣  تطبيق عدة إضافات:");
            Console.WriteLine("════════════════════════════════\n");

            var processor = new ImageProcessor(pluginManager);
            var pipeline = new List<int> { 1, 3, 4 };  // Resize, Watermark, Compress
            processor.ProcessImage(image, pipeline);

            // ─────────────────────────────────────────
            // 3. إضافة إضافة جديدة (Runtime)
            // ─────────────────────────────────────────
            Console.WriteLine("\n\n3️⃣  إضافة إضافة جديدة (بدون تعديل الكود القديم):");
            Console.WriteLine("════════════════════════════════\n");

            // إضافة جديدة خاصة بنا!
            pluginManager.RegisterPlugin(new FilterPlugin("Vintage"));
            pluginManager.RegisterPlugin(new CompressionPlugin(90));

            pluginManager.ListAvailablePlugins();

            // ─────────────────────────────────────────
            // 4. تطبيق جميع الإضافات
            // ─────────────────────────────────────────
            Console.WriteLine("\n\n4️⃣  تطبيق جميع الإضافات:");
            Console.WriteLine("════════════════════════════════");

            pluginManager.ApplyAllPlugins(image);

            // ─────────────────────────────────────────
            // الملخص
            // ─────────────────────────────────────────
            Console.WriteLine("\n═══════════════════════════════════════════════════════════");
            Console.WriteLine("  ✨ الفوائد:");
            Console.WriteLine("═══════════════════════════════════════════════════════════\n");

            Console.WriteLine("""
            1️⃣  سهولة الإضافة:
                - إضافة جديدة = فئة جديدة فقط
                - ترث IImagePlugin
                - لا تعديل على الكود القديم

            2️⃣  المرونة الكاملة:
                - تطبيق إضافات مختلفة
                - ترتيب مختلف
                - توليفات لا نهائية

            3️⃣  العزل والاستقلالية:
                - كل إضافة مستقلة
                - لا تعتمد على بعضها
                - سهل الاختبار

            4️⃣  السهولة في الصيانة:
                - تعديل إضافة لا يؤثر على الباقي
                - حذف إضافة آمن تماماً
                - لا آثار جانبية

            5️⃣  قابل للتوسع للأبد:
                - إضافات غير محدودة
                - توسيع بدون حد
                - نمو سهل وآمن

            هذا هو النمط الحقيقي لـ Plugin Systems
            في تطبيقات مثل:
            - Visual Studio (Extensions)
            - Photoshop (Plugins)
            - Chrome (Extensions)
            - WordPress (Plugins)
            """);

            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("  ✅ انتهى المثال");
            Console.WriteLine("═══════════════════════════════════════════════════════════\n");
            **/
            #endregion

            #region Run Exercises
            /**
                 Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║           تمارين عملية على الواجهات (Interfaces)       ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝\n");

            // ─────────────────────────────────────────
            // التمرين 1: نظام الموسيقى
            // ─────────────────────────────────────────
            Console.WriteLine("\n" + new string('═', 60));
            Console.WriteLine("  التمرين 1: نظام تشغيل الموسيقى");
            Console.WriteLine(new string('═', 60) + "\n");

            var player = new MusicPlayer();
            player.AddTrack(new Song("أغنية 1", 180));
            player.AddTrack(new Podcast("بودكاست 1", 2400));
            player.AddTrack(new Audiobook("كتاب 1", 3600));

            player.PrintPlaylist();
            player.PlayAll();

            // ─────────────────────────────────────────
            // التمرين 2: نظام الدفع
            // ─────────────────────────────────────────
            Console.WriteLine("\n\n" + new string('═', 60));
            Console.WriteLine("  التمرين 2: نظام معالجة الدفع");
            Console.WriteLine(new string('═', 60) + "\n");

            var checkout = new Checkout();
            var card = new CreditCard("1234-5678", 1000);
            var paypal = new PayPal("user@example.com", 500);
            var googlePay = new GooglePay(750);

            checkout.ProcessOrder(100, card);
            checkout.ProcessOrder(200, paypal);
            checkout.ProcessOrder(150, googlePay);

            // اختبار الاسترجاع
            Console.WriteLine("\n💰 اختبار الاسترجاع:");
            ((IRefundable)card).RefundPayment(50);
            ((IRefundable)paypal).RefundPayment(75);

            // ─────────────────────────────────────────
            // التمرين 3: نظام الإشعارات
            // ─────────────────────────────────────────
            Console.WriteLine("\n\n" + new string('═', 60));
            Console.WriteLine("  التمرين 3: نظام الإشعارات المتعدد");
            Console.WriteLine(new string('═', 60) + "\n");

            var notificationService = new NotificationService();
            notificationService.RegisterNotification(
                new EmailNotification("user@example.com"));
            notificationService.RegisterNotification(
                new SMSNotification("0501234567"));
            notificationService.RegisterNotification(
                new PushNotification("user123"));

            notificationService.SendToAll("مرحباً بك في التطبيق!");

            Console.WriteLine("\n═══════════════════════════════════════════════════════════");
            Console.WriteLine("  ✅ انتهت جميع التمارين");
            Console.WriteLine("═══════════════════════════════════════════════════════════\n");
            **/
            #endregion

            #region Real World Scenarios => Ecommerce system
            /**
                Console.OutputEncoding = System.Text.Encoding.UTF8;

                Console.WriteLine("╔═══════════════════════════════════════════════════════════╗");
                Console.WriteLine("║          نظام تجارة إلكترونية متكامل - حالة واقعية      ║");
                Console.WriteLine("╚═══════════════════════════════════════════════════════════╝\n");

                // ─────────────────────────────────────────
                // التطبيق 1: Stripe + FedEx
                // ─────────────────────────────────────────
                Console.WriteLine("1️⃣  التطبيق 1: Stripe + FedEx");
                Console.WriteLine("════════════════════════════════\n");

                var store1 = new ECommerceStore(
                    new StripePaymentGateway(),
                    new FedexShippingProvider()
                );

                store1.AddNotificationChannel(new EmailNotificationChannel());
                store1.AddNotificationChannel(new SmsNotificationChannel());
                store1.AddReportGenerator(new SalesReportGenerator());

                var order1 = new Order(1);
                order1.Items.Add(new Product(1, "الكمبيوتر المحمول", 1000));
                order1.Items.Add(new Product(2, "الفأرة", 50));
                order1.Total = 1050;

                store1.ProcessOrder(order1, "stripe_token_123", "القاهرة، مصر", "user@example.com");

                // ─────────────────────────────────────────
                // التطبيق 2: PayPal + DHL
                // ─────────────────────────────────────────
                Console.WriteLine("\n\n2️⃣  التطبيق 2: PayPal + DHL");
                Console.WriteLine("════════════════════════════════\n");

                var store2 = new ECommerceStore(
                    new PayPalPaymentGateway(),
                    new DhlShippingProvider()
                );

                store2.AddNotificationChannel(new PushNotificationChannel());
                store2.AddReportGenerator(new CustomerReportGenerator());

                var order2 = new Order(2);
                order2.Items.Add(new Product(3, "الهاتف الذكي", 800));
                order2.Total = 800;

                store2.ProcessOrder(order2, "paypal@example.com", "الرياض، السعودية", "user123");

                // ─────────────────────────────────────────
                // التطبيق 3: Apple Pay + التوصيل المحلي
                // ─────────────────────────────────────────
                Console.WriteLine("\n\n3️⃣  التطبيق 3: Apple Pay + التوصيل المحلي");
                Console.WriteLine("════════════════════════════════\n");

                var store3 = new ECommerceStore(
                    new ApplePaymentGateway(),
                    new LocalDeliveryProvider()
                );

                store3.AddNotificationChannel(new EmailNotificationChannel());
                store3.AddNotificationChannel(new SmsNotificationChannel());
                store3.AddNotificationChannel(new PushNotificationChannel());
                store3.AddReportGenerator(new DetailedReportGenerator());

                var order3 = new Order(3);
                order3.Items.Add(new Product(4, "الكتاب", 30));
                order3.Items.Add(new Product(5, "الأقلام", 20));
                order3.Total = 50;

                store3.ProcessOrder(order3, "apple_device_id", "جدة، السعودية", "customer@test.com");

                // ─────────────────────────────────────────
                // التقارير النهائية
                // ─────────────────────────────────────────
                Console.WriteLine("\n\n" + new string('═', 60));
                Console.WriteLine("  📊 التقارير الشاملة");
                Console.WriteLine(new string('═', 60));

                store1.GenerateAllReports();
                store2.GenerateAllReports();
                store3.GenerateAllReports();

                // ─────────────────────────────────────────
                // الملخص
                // ─────────────────────────────────────────
                Console.WriteLine("\n\n" + new string('═', 60));
                Console.WriteLine("  ✨ الفوائد الحقيقية:");
                Console.WriteLine(new string('═', 60) + "\n");

                Console.WriteLine("""
                1️⃣  المرونة الكاملة:
                    - تغيير طريقة الدفع بسهولة
                    - تغيير مزود الشحن بسهولة
                    - إضافة قنوات إخطار جديدة
                    - إضافة مولدات تقارير جديدة

                2️⃣  الاستقلالية التامة:
                    - كل مكون مستقل تماماً
                    - لا تبعيات معقدة
                    - سهل الاختبار
                    - سهل الصيانة

                3️⃣  قابل للتوسع:
                    - إضافة Stripe جديدة؟ فئة جديدة
                    - إضافة UPS للشحن؟ فئة جديدة
                    - لا تعديل على الكود الموجود!

                4️⃣  حالات الاستخدام المرنة:
                    - كل متجر يستخدم توليفة مختلفة
                    - store1: Stripe + FedEx + Email
                    - store2: PayPal + DHL + Push
                    - store3: Apple + Local + الكل

                5️⃣  تطبيق حقيقي للـ SOLID:
                    - Single Responsibility
                    - Open/Closed Principle
                    - Liskov Substitution
                    - Interface Segregation
                    - Dependency Inversion

                هذا هو الواقع الحقيقي في التطبيقات الاحترافية!
                """);

                Console.WriteLine("═══════════════════════════════════════════════════════════");
                Console.WriteLine("  ✅ انتهت الحالة الواقعية");
                Console.WriteLine("═══════════════════════════════════════════════════════════\n");
            **/
            #endregion

            #endregion

            #region (Polymorphism)

            #region (Examples)

            #region Basic Polymorphism (MethodOverloading, Overriding, OperatorOverloading, Virtual_VS_New)
            /**
                Console.OutputEncoding = System.Text.Encoding.UTF8;

                Console.WriteLine("═══════════════════════════════════════════════════════════");
                Console.WriteLine("  مثال شامل على تعدد الأشكال (Polymorphism)");
                Console.WriteLine("═══════════════════════════════════════════════════════════\n");

                // ─────────────────────────────────────────
                // 1. Method Overloading
                // ─────────────────────────────────────────
                Console.WriteLine("1️⃣  Method Overloading (Compile-time):");
                Console.WriteLine("────────────────────────────────────\n");

                var calc = new Calculator();

                int result1 = calc.Add(5, 3);
                Console.WriteLine($"النتيجة: {result1}\n");

                double result2 = calc.Add(5.5, 3.3);
                Console.WriteLine($"النتيجة: {result2}\n");

                int result3 = calc.Add(1, 2, 3);
                Console.WriteLine($"النتيجة: {result3}\n");

                int result4 = calc.Add(1, 2, 3, 4, 5);
                Console.WriteLine($"النتيجة: {result4}\n");

                string result5 = calc.Add("Hello", " World");
                Console.WriteLine($"النتيجة: {result5}\n");

                // ─────────────────────────────────────────
                // 2. Method Overriding
                // ─────────────────────────────────────────
                Console.WriteLine("\n2️⃣  Method Overriding (Runtime):");
                Console.WriteLine("────────────────────────────────────\n");

                // Polymorphism الحقيقي!
                List<Animal> animals = new()
                {
                    new Dog("ماكس"),
                    new Cat("ميسي"),
                    new Duck("دونالد")
                };

                Console.WriteLine("🔊 أصوات الحيوانات:");
                foreach (var animal in animals)
                {
                    animal.MakeSound();
                }

                Console.WriteLine("\n🏃 حركة الحيوانات:");
                foreach (var animal in animals)
                {
                    animal.Move();
                }

                Console.WriteLine("\n😴 جميع الحيوانات تنام:");
                foreach (var animal in animals)
                {
                    animal.Sleep();  // نفس التطبيق للجميع
                }

                // ─────────────────────────────────────────
                // 3. Operator Overloading
                // ─────────────────────────────────────────
                Console.WriteLine("\n\n3️⃣  Operator Overloading:");
                Console.WriteLine("────────────────────────────────────\n");

                var frac1 = new Fraction(1, 2);
                var frac2 = new Fraction(1, 3);

                Console.WriteLine($"الكسر 1: {frac1}");
                Console.WriteLine($"الكسر 2: {frac2}\n");

                var sum = frac1 + frac2;
                Console.WriteLine($"الجمع: {frac1} + {frac2} = {sum}");

                var diff = frac1 - frac2;
                Console.WriteLine($"الطرح: {frac1} - {frac2} = {diff}");

                var product = frac1 * frac2;
                Console.WriteLine($"الضرب: {frac1} * {frac2} = {product}");

                var frac3 = new Fraction(2, 4);
                Console.WriteLine($"\nالمساواة: {frac1} == {frac3} هو {frac1 == frac3}");

                // ─────────────────────────────────────────
                // 4. الفرق بين Virtual و New
                // ─────────────────────────────────────────
                Console.WriteLine("\n\n4️⃣  الفرق بين Virtual و New:");
                Console.WriteLine("────────────────────────────────────\n");

                Doctor doctor = new Doctor();
                Engineer engineer = new Engineer();

                // يعملان معاً كـ Person
                Person p1 = doctor;
                Person p2 = engineer;

                Console.WriteLine("استدعاء Greet() كـ Person:");
                p1.Greet();  // 🏥 Doctor
                p2.Greet();  // 🙋 Person (لأن Engineer استخدمت new!)

                Console.WriteLine("\nاستدعاء Greet() مباشرة:");
                doctor.Greet();    // 🏥 Doctor
                engineer.Greet();  // ⚙️  Engineer

                // ─────────────────────────────────────────
                // الملخص
                // ─────────────────────────────────────────
                Console.WriteLine("\n\n═══════════════════════════════════════════════════════════");
                Console.WriteLine("  ✨ أنواع Polymorphism:");
                Console.WriteLine("═══════════════════════════════════════════════════════════\n");

                Console.WriteLine("""
                1️⃣  Compile-time Polymorphism:
                    ✅ Method Overloading
                        نفس الاسم، parameters مختلفة
                        المترجم يختار الدالة الصحيحة

                    ✅ Operator Overloading
                        تعريف معاني جديدة للمعاملات

                2️⃣  Runtime Polymorphism:
                    ✅ Method Overriding
                        كائن واحد، سلوكيات مختلفة
                        يتحدد الحل الصحيح عند التنفيذ

                3️⃣  الفرق بين Virtual و New:
                    ✅ virtual + override = polymorphism حقيقي
                    ❌ new = shadowing (لا polymorphism!)

                4️⃣  الفائدة الحقيقية:
                    - كود واحد، حالات مختلفة
                    - سهل الإضافة والتعديل
                    - كود أنظف وأوضح
                """);

                Console.WriteLine("═══════════════════════════════════════════════════════════");
                Console.WriteLine("  ✅ انتهى المثال");
                Console.WriteLine("═══════════════════════════════════════════════════════════\n");
            **/
            #endregion

            #region Advanced Polymorphism
            /**
                Console.OutputEncoding = System.Text.Encoding.UTF8;

                Console.WriteLine("╔═══════════════════════════════════════════════════════════╗");
                Console.WriteLine("║        مثال متقدم: Polymorphism في الأنظمة المعقدة      ║");
                Console.WriteLine("╚═══════════════════════════════════════════════════════════╝\n");

                // ─────────────────────────────────────────
                // 1. نظام الأشكال
                // ─────────────────────────────────────────
                Console.WriteLine("1️⃣  نظام إدارة الأشكال الهندسية:");
                Console.WriteLine("════════════════════════════════\n");

                var shapeManager = new ShapeManager();

                shapeManager.AddShape(new Circle("دائرة 1", "أحمر", 5));
                shapeManager.AddShape(new Square("مربع 1", "أزرق", 4));
                shapeManager.AddShape(new Rectangle("مستطيل 1", "أخضر", 6, 3));
                shapeManager.AddShape(new Triangle("مثلث 1", "أصفر", 3, 4, 5));
                shapeManager.AddShape(new Circle("دائرة 2", "برتقالي", 3));

                shapeManager.DrawAll();
                shapeManager.CalculateTotal();
                shapeManager.SortByArea();
                shapeManager.PrintAllInfo();

                // ─────────────────────────────────────────
                // 2. نظام إدارة الموظفين
                // ─────────────────────────────────────────
                Console.WriteLine("\n\n2️⃣  نظام إدارة الموظفين:");
                Console.WriteLine("════════════════════════════════\n");

                var hrSystem = new HRManagementSystem();

                hrSystem.AddEmployee(new Developer("أحمد", 5000, 10));
                hrSystem.AddEmployee(new Manager("فاطمة", 7000, 8));
                hrSystem.AddEmployee(new Designer("محمد", 4500, 15));
                hrSystem.AddEmployee(new Developer("نور", 5500, 7));
                hrSystem.AddEmployee(new Designer("سارة", 4000, 12));

                hrSystem.MakeEveryoneWork();
                hrSystem.PrintPayroll();
                hrSystem.PrintAllInfo();

                // معلومات إضافية
                Console.WriteLine($"\n📊 الموظف الأعلى راتباً: {hrSystem.GetHighestPaid().name}");
                Console.WriteLine($"💰 إجمالي الرواتب: {hrSystem.GetTotalPayroll():C}");

                // ─────────────────────────────────────────
                // الملخص
                // ─────────────────────────────────────────
                Console.WriteLine("\n\n═══════════════════════════════════════════════════════════");
                Console.WriteLine("  ✨ مميزات الـ Polymorphism المتقدم:");
                Console.WriteLine("═══════════════════════════════════════════════════════════\n");

                Console.WriteLine("""
                1️⃣  Multiple Interfaces:
                    • Shape ترث من 3 واجهات
                    • ICalculable, IDrawable, IComparable
                    • نفس الكائن، قدرات مختلفة

                2️⃣  قوائم Polymorphic:
                    • List<Shape> تحتوي على أنواع مختلفة
                    • DrawAll() تعمل مع جميع الأنواع
                    • لا حاجة لـ if/else!

                3️⃣  الحسابات المعقدة:
                    • كل شكل له حسابة مختلفة
                    • نفس الدالة CalculateArea()
                    • نتائج مختلفة!

                4️⃣  المقارنة والترتيب:
                    • نفس المنطق (CompareTo)
                    • لكن مقارنة ذكية
                    • تعمل مع جميع الأنواع

                5️⃣  الفصل الكامل:
                    • ShapeManager لا تعرف الأنواع
                    • HRManagementSystem منفصلة تماماً
                    • إضافة نوع جديد = فئة جديدة فقط

                6️⃣  إعادة استخدام الكود:
                    • منطق واحد (DrawAll)
                    • يعمل مع جميع الأشكال
                    • توسع بلا حد

                هذا هو الـ Polymorphism الحقيقي والفعال!
                """);

                Console.WriteLine("═══════════════════════════════════════════════════════════");
                Console.WriteLine("  ✅ انتهى المثال المتقدم");
                Console.WriteLine("═══════════════════════════════════════════════════════════\n");
                **/

            #endregion

            #region Collection
            /**
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║      قوائم Polymorphic: التعامل مع أنواع مختلفة        ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝\n");

            // ─────────────────────────────────────────
            // إنشاء المركبات
            // ─────────────────────────────────────────
            Console.WriteLine("🏪 إنشاء معرض سيارات:");
            Console.WriteLine("════════════════════════════════\n");

            var dealership = new Dealership();

            // إضافة سيارات مختلفة
            dealership.AddVehicle(new Polymorphism.Examples.Advanced.Car("تويوتا كامري", 25000, 2023, 4));
            dealership.AddVehicle(new Polymorphism.Examples.Advanced.Car("هونداي أكورد", 22000, 2022, 4));
            dealership.AddVehicle(new Motorcycle("هارلي ديفيدسون", 15000, 2023, true));
            dealership.AddVehicle(new Truck("فولفو FH", 50000, 2021, 20));
            dealership.AddVehicle(new ElectricCar("تسلا 3", 45000, 2023, 75));
            dealership.AddVehicle(new Polymorphism.Examples.Advanced.Car("بي إم دبليو 3", 30000, 2023, 4));
            dealership.AddVehicle(new Motorcycle("ياماها MT-07", 8000, 2022, false));
            dealership.AddVehicle(new ElectricCar("نيسان ليف", 35000, 2022, 60));
            dealership.AddVehicle(new Truck("مان TGX", 55000, 2023, 25));

            Console.WriteLine("✅ تمت إضافة 9 مركبات\n");

            // ─────────────────────────────────────────
            // 1. العرض
            // ─────────────────────────────────────────
            Console.WriteLine("\n1️⃣  العرض والتشغيل:");
            Console.WriteLine("════════════════════════════════");

            dealership.DisplayAllVehicles();
            dealership.StartAllVehicles();

            // ─────────────────────────────────────────
            // 2. التصفية
            // ─────────────────────────────────────────
            Console.WriteLine("\n\n2️⃣  التصفية والبحث:");
            Console.WriteLine("════════════════════════════════");

            dealership.DisplayCarsOnly();
            dealership.DisplayByType<ElectricCar>();
            dealership.DisplayExpensiveVehicles(40000);
            dealership.DisplayVehiclesByYear(2023);

            // ─────────────────────────────────────────
            // 3. الحسابات
            // ─────────────────────────────────────────
            Console.WriteLine("\n\n3️⃣  الحسابات والإحصائيات:");
            Console.WriteLine("════════════════════════════════");

            dealership.PrintStatistics();
            dealership.PrintFuelConsumptionReport();

            // ─────────────────────────────────────────
            // 4. البحث و Pattern Matching
            // ─────────────────────────────────────────
            Console.WriteLine("\n\n4️⃣  البحث و Pattern Matching:");
            Console.WriteLine("════════════════════════════════");

            var found = dealership.FindByName("تسلا 3");
            if (found != null)
            {
                dealership.DisplayVehicleDetails(found);
            }

            var priceRange = dealership.FindByPriceRange(20000, 30000);
            Console.WriteLine($"\n🔍 المركبات من 20,000 إلى 30,000:");
            foreach (var vehicle in priceRange)
            {
                Console.WriteLine($"  • {vehicle.Name}: {vehicle.Price:C}");
            }

            // ─────────────────────────────────────────
            // 5. التحويل والترتيب
            // ─────────────────────────────────────────
            Console.WriteLine("\n\n5️⃣  التحويل والترتيب:");
            Console.WriteLine("════════════════════════════════");

            dealership.PrintVehiclesByType();
            dealership.PrintSortedByPrice(ascending: false);

            // ─────────────────────────────────────────
            // الملخص
            // ─────────────────────────────────────────
            Console.WriteLine("\n\n" + new string('═', 60));
            Console.WriteLine("  ✨ الفوائس:");
            Console.WriteLine(new string('═', 60) + "\n");

            Console.WriteLine("""
            1️⃣  قائمة واحدة، أنواع مختلفة:
                • List<Vehicle> تحتوي على الكل
                • بدون حاجة لـ if/else

            2️⃣  LINQ مع Polymorphism:
                • OfType<T>() - اختيار نوع معين
                • Where() - تصفية حسب شروط
                • GroupBy() - تجميع ذكي
                • OrderBy() - ترتيب

            3️⃣  Pattern Matching:
                • switch مع الأنواع
                • حل أنيق وآمن
                • كود أقل وأوضح

            4️⃣  الحسابات المعقدة:
                • Sum(), Average(), Count()
                • تعمل مع جميع الأنواع
                • نتائج مفيدة

            5️⃣  المرونة الكاملة:
                • إضافة نوع جديد = فئة جديدة
                • بدون تعديل على Dealership
                • كود قابل للتوسع

            هذا هو الـ Polymorphism العملي والفعال!
            """);

            Console.WriteLine(new string('═', 60));
            Console.WriteLine("  ✅ انتهى المثال");
            Console.WriteLine(new string('═', 60) + "\n");
            **/
            #endregion
            #endregion

            #region Exercies
            /**
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║           تمارين عملية على تعدد الأشكال               ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝\n");

            // ─────────────────────────────────────────
            // التمرين 1: نظام الألعاب
            // ─────────────────────────────────────────
            Console.WriteLine("\n" + new string('═', 60));
            Console.WriteLine("  التمرين 1: نظام الألعاب والمعارك");
            Console.WriteLine(new string('═', 60) + "\n");

            var warrior = new Warrior("كونان");
            var mage = new Mage("غاندالف");
            var archer = new Archer("ليجولاس");

            Console.WriteLine("📊 إحصائيات الشخصيات:");
            warrior.DisplayStats();
            Console.WriteLine();
            mage.DisplayStats();
            Console.WriteLine();
            archer.DisplayStats();

            var battle = new BattleSystem();
            battle.StartBattle(warrior, mage);

            // ─────────────────────────────────────────
            // التمرين 2: نظام التغذية
            // ─────────────────────────────────────────
            Console.WriteLine("\n\n" + new string('═', 60));
            Console.WriteLine("  التمرين 2: نظام التغذية والسعرات");
            Console.WriteLine(new string('═', 60) + "\n");

            var tracker = new NutritionTracker();
            tracker.AddFood(new Meat("دجاج", 200));
            tracker.AddFood(new Vegetable("جزر", 150));
            tracker.AddFood(new Fruit("تفاح", 100));
            tracker.AddFood(new Meat("سمك", 250));

            tracker.PrintMealBreakdown();
            tracker.PrintDailyNutrition();

            // ─────────────────────────────────────────
            // التمرين 3: نظام الرياضات
            // ─────────────────────────────────────────
            Console.WriteLine("\n\n" + new string('═', 60));
            Console.WriteLine("  التمرين 3: نظام اللياقة البدنية");
            Console.WriteLine(new string('═', 60) + "\n");

            var fitness = new FitnessTracker();
            fitness.AddActivity(new Running(10), 1);
            fitness.AddActivity(new Swimming(), 1.5);
            fitness.AddActivity(new Cycling(20), 2);

            fitness.PrintFitnessSummary();
            fitness.PrintMostEffective();

            Console.WriteLine("\n" + new string('═', 60));
            Console.WriteLine("  ✅ انتهت جميع التمارين");
            Console.WriteLine(new string('═', 60) + "\n");
            **/
            #endregion

            #region Real World Scenarios Banking System
            /**
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║        نظام بنكي متكامل - حالة واقعية Polymorphism    ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝\n");

            // ─────────────────────────────────────────
            // إنشاء البنك والحسابات
            // ─────────────────────────────────────────
            Console.WriteLine("🏦 إنشاء نظام البنك:");
            Console.WriteLine("════════════════════════════════\n");

            var bank = new BankSystem();

            // فتح حسابات مختلفة
            var checking = new CheckingAccount("1001", "أحمد محمود", 5000);
            var savings = new SavingsAccount("1002", "فاطمة علي", 20000);
            var investment = new InvestmentAccount("1003", "محمد حسن", 50000);

            bank.AddAccount(checking);
            bank.AddAccount(savings);
            bank.AddAccount(investment);

            // ─────────────────────────────────────────
            // عمليات بنكية
            // ─────────────────────────────────────────
            Console.WriteLine("\n\n💳 عمليات بنكية:");
            Console.WriteLine("════════════════════════════════\n");

            Console.WriteLine("حساب جاري:");
            checking.Deposit(2000);
            checking.Withdraw(1000);
            checking.PrintBalance();

            Console.WriteLine("\nحساب توفير:");
            savings.Deposit(5000);
            savings.Withdraw(500);
            savings.PrintBalance();

            Console.WriteLine("\nحساب استثماري:");
            investment.Deposit(30000);
            investment.PrintBalance();

            // ─────────────────────────────────────────
            // عرض جميع الحسابات
            // ─────────────────────────────────────────
            bank.PrintAllAccounts();

            // ─────────────────────────────────────────
            // تطبيق العمليات الشهرية
            // ─────────────────────────────────────────
            bank.ApplyMonthlyOperations();

            // ─────────────────────────────────────────
            // التقارير
            // ─────────────────────────────────────────
            bank.PrintBankReport();
            bank.PrintInterestSummary();

            // ─────────────────────────────────────────
            // تفاصيل حساب معين
            // ─────────────────────────────────────────
            bank.PrintAccountDetails("1002");

            // ─────────────────────────────────────────
            // الملخص
            // ─────────────────────────────────────────
            Console.WriteLine("\n\n═══════════════════════════════════════════════════════════");
            Console.WriteLine("  ✨ مميزات الـ Polymorphism في النظام البنكي:");
            Console.WriteLine("═══════════════════════════════════════════════════════════\n");

            Console.WriteLine("""
            1️⃣  أنواع حسابات مختلفة:
                • CheckingAccount: جاري (رسوم شهرية)
                • SavingsAccount: توفير (فائدة منخفضة)
                • InvestmentAccount: استثماري (فائدة عالية)

            2️⃣  سلوك مختلف لكل نوع:
                • Deposit: نفس السلوك الأساسي
                • Withdraw: يختلف حسب النوع
                • Interest: محسوبة بطرق مختلفة
                • Fees: تختلف لكل نوع

            3️⃣  قائمة واحدة لجميع الحسابات:
                • List<BankAccount> تحتوي على الكل
                • ApplyMonthlyOperations يعمل على الجميع
                • بدون if/else معقدة

            4️⃣  التقارير الشاملة:
                • PrintBankReport: معلومات عن كل الحسابات
                • PrintInterestSummary: الفوائس الإجمالية
                • PrintAccountDetails: تفاصيل حساب معين

            5️⃣  توسيع سهل:
                • حساب جديد = فئة جديدة فقط
                • الكود الموجود لا يتغير
                • توافق كامل مع النظام

            هذا هو الـ Polymorphism في نظام حقيقي احترافي!
            """);

            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("  ✅ انتهت الحالة الواقعية");
            Console.WriteLine("═══════════════════════════════════════════════════════════\n");
        **/
            #endregion
            #endregion
        }
    }
}
