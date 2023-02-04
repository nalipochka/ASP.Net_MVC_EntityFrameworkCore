namespace ASP.Net_MVC_EntityFrameworkCore.Models
{
    public class BooksLibrary : ILibrary<Book>
    {
        private readonly LibraryContext context;

        public BooksLibrary(LibraryContext context)
        {
            this.context = context;
        }
        public Book Add(Book item)
        {
            context.Books.Add(item);
            context.SaveChanges();
            return item;
        }

        public bool Delete(int id)
        {
            Book? book = context.Books.Find(id);
            if (book != null)
            {
                context.Books.Remove(book);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public Book Edit(Book item)
        {
            Book? book = context.Books.Find(item.Id);
            book!.Title = item.Title;
            book!.Author = item.Author;
            book!.Genre = item.Genre;
            book!.Publisher = item.Publisher;
            book!.FilePath = item.FilePath;
            book!.YearOfPublish = item.YearOfPublish;
            book!.More = item.More;
            context.SaveChanges();
            return book;
        }

        public Book? Get(int id)
        {
            return context.Books.Find(id);
        }

        public IEnumerable<Book> GetAll()
        {
            return context.Books.ToList();
        }
    }
}
