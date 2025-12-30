/*
 * LibraryManagementSystem.cs
 * ════════════════════════════════════════════════════════════
 * حالة واقعية: نظام مكتبة رقمية متكامل
 *
 * السيناريو:
 * ────────
 * مكتبة رقمية تحتاج:
 * - حماية بيانات الكتب
 * - إدارة حسابات الأعضاء
 * - نظام استعارة وإرجاع
 * - التحقق من الفترات الزمنية
 *
 * هذا يوضح Encapsulation في نظام حقيقي معقد
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace Encapsulation.RealWorldScenarios
{
    // ════════════════════════════════════════════════════════════
    // نماذج البيانات
    // ════════════════════════════════════════════════════════════

    /// <summary>
    /// الكتاب - البيانات محمية
    /// </summary>
    public class Book
    {
        // Private fields - البيانات الحساسة
        private string isbn;
        private string title;
        private string author;
        private int yearPublished;
        private int totalCopies;
        private int availableCopies;
        private decimal price;

        public Book(string isbn, string title, string author,
                   int year, int copies, decimal price)
        {
            ISBN = isbn;
            Title = title;
            Author = author;
            YearPublished = year;
            TotalCopies = copies;
            availableCopies = copies;
            Price = price;
        }

        // Properties مع Validation
        public string ISBN
        {
            get { return isbn; }
            private set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    isbn = value;
                else
                    throw new ArgumentException("ISBN لا يمكن أن يكون فارغ");
            }
        }

        public string Title
        {
            get { return title; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && value.Length >= 3)
                    title = value;
                else
                    throw new ArgumentException("العنوان يجب أن يكون 3 أحرف على الأقل");
            }
        }

        public string Author
        {
            get { return author; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    author = value;
                else
                    throw new ArgumentException("اسم المؤلف لا يمكن أن يكون فارغ");
            }
        }

        public int YearPublished
        {
            get { return yearPublished; }
            set
            {
                if (value > 0 && value <= DateTime.Now.Year)
                    yearPublished = value;
                else
                    throw new ArgumentException("سنة النشر غير صحيحة");
            }
        }

        public int TotalCopies
        {
            get { return totalCopies; }
            private set
            {
                if (value > 0)
                    totalCopies = value;
                else
                    throw new ArgumentException("عدد النسخ يجب أن يكون موجب");
            }
        }

        public int AvailableCopies
        {
            get { return availableCopies; }
            private set
            {
                if (value >= 0 && value <= totalCopies)
                    availableCopies = value;
                else
                    throw new ArgumentException("عدد النسخ المتاحة غير صحيح");
            }
        }

        public decimal Price
        {
            get { return price; }
            set
            {
                if (value > 0)
                    price = value;
                else
                    throw new ArgumentException("السعر يجب أن يكون موجب");
            }
        }

        // Methods
        public bool BorrowCopy()
        {
            if (availableCopies > 0)
            {
                availableCopies--;
                return true;
            }
            return false;
        }

        public void ReturnCopy()
        {
            if (availableCopies < totalCopies)
                availableCopies++;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"📚 {Title}");
            Console.WriteLine($"   المؤلف: {Author}");
            Console.WriteLine($"   سنة النشر: {YearPublished}");
            Console.WriteLine($"   النسخ المتاحة: {availableCopies}/{totalCopies}");
            Console.WriteLine($"   السعر: {price:C}");
        }
    }

    /// <summary>
    /// عضو المكتبة - البيانات محمية
    /// </summary>
    public class Member
    {
        // Private fields
        private int memberId;
        private string name;
        private string email;
        private DateTime registrationDate;
        private List<BorrowRecord> borrowHistory;
        private bool isActive;
        private decimal accountBalance;

        public Member(int id, string name, string email)
        {
            MemberId = id;
            Name = name;
            Email = email;
            registrationDate = DateTime.Now;
            borrowHistory = new List<BorrowRecord>();
            isActive = true;
            accountBalance = 0;
        }

        // Properties
        public int MemberId
        {
            get { return memberId; }
            private set
            {
                if (value > 0)
                    memberId = value;
                else
                    throw new ArgumentException("رقم العضوية يجب أن يكون موجب");
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && value.Length >= 3)
                    name = value;
                else
                    throw new ArgumentException("الاسم يجب أن يكون 3 أحرف على الأقل");
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && value.Contains("@"))
                    email = value;
                else
                    throw new ArgumentException("البريد الإلكتروني غير صحيح");
            }
        }

        public DateTime RegistrationDate => registrationDate;  // Read-only

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        public decimal AccountBalance
        {
            get { return accountBalance; }
            private set
            {
                accountBalance = value;
            }
        }

        // Methods
        public void AddToBalance(decimal amount)
        {
            AccountBalance += amount;
        }

        public bool ChargeFromBalance(decimal amount)
        {
            if (accountBalance >= amount)
            {
                AccountBalance -= amount;
                return true;
            }
            return false;
        }

        public void AddBorrowRecord(BorrowRecord record)
        {
            borrowHistory.Add(record);
        }

        public List<BorrowRecord> GetBorrowHistory()
        {
            return new List<BorrowRecord>(borrowHistory);
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"👤 {Name} (العضو: {memberId})");
            Console.WriteLine($"   البريد: {Email}");
            Console.WriteLine($"   التسجيل: {registrationDate:yyyy-MM-dd}");
            Console.WriteLine($"   الحالة: {(isActive ? "نشط" : "غير نشط")}");
            Console.WriteLine($"   الرصيد: {accountBalance:C}");
        }
    }

    /// <summary>
    /// سجل الاستعارة
    /// </summary>
    public class BorrowRecord
    {
        private string borrowId;
        private Book book;
        private DateTime borrowDate;
        private DateTime dueDate;
        private DateTime? returnDate;
        private decimal fineAmount;

        public BorrowRecord(Book book)
        {
            borrowId = Guid.NewGuid().ToString().Substring(0, 8);
            this.book = book;
            borrowDate = DateTime.Now;
            dueDate = borrowDate.AddDays(14);  // 14 يوم
            returnDate = null;
            fineAmount = 0;
        }

        public string BorrowId => borrowId;
        public Book Book => book;
        public DateTime BorrowDate => borrowDate;
        public DateTime DueDate => dueDate;
        public DateTime? ReturnDate => returnDate;
        public decimal FineAmount => fineAmount;

        public bool IsOverdue => returnDate == null && DateTime.Now > dueDate;

        public void ReturnBook()
        {
            returnDate = DateTime.Now;

            // حساب الغرامة
            if (IsOverdue)
            {
                int overdueDays = (int)(DateTime.Now - dueDate).TotalDays;
                fineAmount = overdueDays * 5;  // 5 ريال لكل يوم
            }
        }

        public void DisplayInfo()
        {
            string status = returnDate == null ? "🔴 مستعار" : "🟢 مرجع";
            Console.WriteLine($"{status} {book.Title}");
            Console.WriteLine($"   تاريخ الاستعارة: {borrowDate:yyyy-MM-dd}");
            Console.WriteLine($"   تاريخ الاستحقاق: {dueDate:yyyy-MM-dd}");

            if (returnDate.HasValue)
                Console.WriteLine($"   تاريخ الإرجاع: {returnDate:yyyy-MM-dd}");

            if (fineAmount > 0)
                Console.WriteLine($"   الغرامة: {fineAmount:C}");
        }
    }

    // ════════════════════════════════════════════════════════════
    // نظام المكتبة
    // ════════════════════════════════════════════════════════════

    public class LibraryManagementSystem
    {
        private List<Book> books = new();
        private List<Member> members = new();
        private List<BorrowRecord> activeLoans = new();

        // ─────────────────────────────────────────
        // إدارة الكتب
        // ─────────────────────────────────────────

        public void AddBook(Book book)
        {
            books.Add(book);
            Console.WriteLine($"✅ تمت إضافة الكتاب: {book.Title}");
        }

        public Book FindBook(string isbn)
        {
            return books.FirstOrDefault(b => b.ISBN == isbn);
        }

        public List<Book> SearchByAuthor(string author)
        {
            return books.Where(b => b.Author.Contains(author)).ToList();
        }

        public List<Book> SearchByTitle(string title)
        {
            return books.Where(b => b.Title.Contains(title)).ToList();
        }

        // ─────────────────────────────────────────
        // إدارة الأعضاء
        // ─────────────────────────────────────────

        public void AddMember(Member member)
        {
            members.Add(member);
            Console.WriteLine($"✅ تم تسجيل عضو جديد: {member.Name}");
        }

        public Member FindMember(int memberId)
        {
            return members.FirstOrDefault(m => m.MemberId == memberId);
        }

        // ─────────────────────────────────────────
        // عمليات الاستعارة والإرجاع
        // ─────────────────────────────────────────

        public bool BorrowBook(int memberId, string isbn)
        {
            var member = FindMember(memberId);
            var book = FindBook(isbn);

            if (member == null)
            {
                Console.WriteLine("❌ العضو غير موجود");
                return false;
            }

            if (book == null)
            {
                Console.WriteLine("❌ الكتاب غير موجود");
                return false;
            }

            if (!member.IsActive)
            {
                Console.WriteLine("❌ العضو غير نشط");
                return false;
            }

            if (!book.BorrowCopy())
            {
                Console.WriteLine("❌ لا توجد نسخ متاحة");
                return false;
            }

            var record = new BorrowRecord(book);
            member.AddBorrowRecord(record);
            activeLoans.Add(record);

            Console.WriteLine($"✅ تم استعارة: {book.Title}");
            Console.WriteLine($"   تاريخ الاستحقاق: {record.DueDate:yyyy-MM-dd}");

            return true;
        }

        public bool ReturnBook(int memberId, string isbn)
        {
            var record = activeLoans.FirstOrDefault(
                r => r.Book.ISBN == isbn && r.ReturnDate == null);

            if (record == null)
            {
                Console.WriteLine("❌ سجل الاستعارة غير موجود");
                return false;
            }

            record.ReturnBook();
            record.Book.ReturnCopy();

            var member = FindMember(memberId);
            if (member != null && record.FineAmount > 0)
            {
                member.AddToBalance(record.FineAmount);
                Console.WriteLine($"⚠️  غرامة تأخير: {record.FineAmount:C}");
            }

            Console.WriteLine($"✅ تم إرجاع: {record.Book.Title}");
            return true;
        }

        // ─────────────────────────────────────────
        // التقارير
        // ─────────────────────────────────────────

        public void PrintLibraryStatistics()
        {
            Console.WriteLine("\n📊 إحصائيات المكتبة:");
            Console.WriteLine("════════════════════════════════");
            Console.WriteLine($"  إجمالي الكتب: {books.Count}");
            Console.WriteLine($"  إجمالي الأعضاء: {members.Count}");
            Console.WriteLine($"  الاستعارات النشطة: {activeLoans.Count(r => r.ReturnDate == null)}");

            var overdueLoans = activeLoans.Where(r => r.IsOverdue).ToList();
            if (overdueLoans.Count > 0)
                Console.WriteLine($"  ⚠️  استعارات متأخرة: {overdueLoans.Count}");
        }

        public void PrintOverdueBooks()
        {
            var overdue = activeLoans.Where(r => r.IsOverdue).ToList();

            if (overdue.Count == 0)
            {
                Console.WriteLine("\n✅ لا توجد كتب متأخرة");
                return;
            }

            Console.WriteLine("\n⚠️  الكتب المتأخرة:");
            foreach (var record in overdue)
            {
                record.DisplayInfo();
            }
        }

        public void PrintMemberBorrowHistory(int memberId)
        {
            var member = FindMember(memberId);
            if (member == null)
            {
                Console.WriteLine("❌ العضو غير موجود");
                return;
            }

            var history = member.GetBorrowHistory();
            Console.WriteLine($"\n📋 سجل استعارات {member.Name}:");
            foreach (var record in history)
            {
                record.DisplayInfo();
                Console.WriteLine();
            }
        }

        public void PrintAllBooks()
        {
            Console.WriteLine("\n📚 جميع الكتب:");
            foreach (var book in books)
            {
                book.DisplayInfo();
                Console.WriteLine();
            }
        }

        public void PrintAllMembers()
        {
            Console.WriteLine("\n👥 جميع الأعضاء:");
            foreach (var member in members)
            {
                member.DisplayInfo();
                Console.WriteLine();
            }
        }
    }
}
