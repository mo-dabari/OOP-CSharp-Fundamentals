namespace Composition.Exercises
{
    public class Author
    {
        public string Name { get; set; }

        public Author(string name) => Name = name;
    }

    public class Book
    {
        public string Title { get; set; }
        public Author author;  // Weak Composition

        public Book(string title, Author aut)
        {
            Title = title;
            author = aut;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"📚 {Title} | المؤلف: {author.Name}");
        }
    }

    public class Library
    {
        private List<Book> books = new();

        public void AddBook(Book book) => books.Add(book);

        public void DisplayBooks()
        {
            Console.WriteLine("\n📖 كتب المكتبة:");
            foreach (var book in books)
                book.DisplayInfo();
        }
    }
}
