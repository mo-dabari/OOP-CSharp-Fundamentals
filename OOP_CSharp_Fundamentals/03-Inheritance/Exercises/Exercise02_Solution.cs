using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_CSharp_Fundamentals
{
    public abstract class Document
    {

        public string Title {get;}
        public string Author {get;}
        public DateOnly DateCreated {get;}

        public Document(string title , string author , DateOnly dateCreated)
        {
            if(string.IsNullOrWhiteSpace(title, author))
                throw new InvalidOperationException("Must Be Required title, author");

            if(dateCreated is null)
                throw new InvalidOperationException("Must Be Required Date Created");

            if(dateCreated > DateOnly.FromDateTime(DateTime.Now))
                throw new InvalidOperationException("Invalid Date Must Be smaller Than Today or Equale Today");

            Title = title;
            Author = author;
            DateCreated = dateCreated;
        }

        public virtual void Open()
        {
            Console.WriteLine($"{Title} فتح المستند");
        }

        public virtual void Save()
        {
            Console.WriteLine($"{Title} حفظ المستند");
        }

        public virtual void Print()
        {
            Console.WriteLine($"{Title}  طباعه المستند");
        }

        public virtual string GetInfo()
        {
            return $"{Title} (بقلم {Author})";
        }
    }


    public class TextFile : Document
    {

        public TextFile(string title , string author , DateOnly dateCreated)
        : base(title, author, dateCreated){}
        public override void Open()
        {
            Console.WriteLine($"📝 فتح محرر نصوص: {title}");
        }

        public override void Save()
        {
            Console.WriteLine($"💾 حفظ الملف النصي: {title}");
        }
    }


    public class PDFDocument : Document
    {

        public PDFDocument(string title , string author , DateOnly dateCreated)
        : base(title, author, dateCreated){}

        public void Compress()
        {
            Console.WriteLine($"🗜️  ضغط PDF: {title}");
        }

        public override void Open()
        {
            Console.WriteLine($"📄 فتح قارئ PDF: {title}");
        }
        public override void Print()
        {
            Console.WriteLine($"🖨️  طباعة PDF: {title}");
        }
    }


    public class ExcelDocument : Document
    {

        public ExcelDocument(string title , string author , DateOnly dateCreated)
        : base(title, author, dateCreated){}

        public override void Open()
        {
            Console.WriteLine($"📊 فتح جدول بيانات: {title}");
        }
        
        public override void Save()
        {
            Console.WriteLine($"💾 حفظ جدول البيانات: {title}");
        }
        
        public void CalculateFormulas()
        {
            Console.WriteLine($"🧮 حساب الصيغ في: {title}");
        }
    }


    public class PowerPointPresentation : Document
    {

        public PowerPointPresentation(string title , string author , DateOnly dateCreated)
        : base(title, author, dateCreated){}
        public override void Open()
        {
            Console.WriteLine($"🎬 فتح عرض الشرائح: {title}");
        }
        
        public void StartPresentation()
        {
            Console.WriteLine($"▶️  بدء العرض: {title}");
        }
    }


    public class DocumentManager
    {
        private List<Document> _documents;
        public IReadOnlyList values;

        public DocumentManager(List<Document> Documents)
        {
            _documents = Documents;
            values = _documents;
        }
        public void AddDocument(Document doc)
        {
            if(doc is null)
                throw new NullReferenceException();
            _documents.Add(doc);
            Console.WriteLine($"✅ تم إضافة المستند{doc.GetInfo()}");
        }
        public void  OpenAll()
        {
            Console.WriteLine("\n📂 فتح جميع المستندات:");
            foreach (var doc in documents)
                doc.Open();
        }
        public void SaveAll()
        {
            Console.WriteLine("\n💾 حفظ جميع المستندات:");
            foreach (var doc in documents)
                doc.Save();
        }

        public void PrintAll()
        {
            Console.WriteLine("\n🖨️  طباعة جميع المستندات:");
            foreach (var doc in documents)
                doc.Print();
        }
        public void PrintDocumentsInfo()
        {
            Console.WriteLine("\n📋 معلومات المستندات:");
            foreach (var doc in documents)
                Console.WriteLine($"  • {doc.GetInfo()}");
        }
    }
}