using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Library
{
    class Book
    {
        private Guid Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public decimal? Price { get; set; }
        private int NrCopies { get; set; }
        public Boolean IsBorrowed { get; set; }

        public Book(string name, string iSBN, decimal price, int nrCopies = 1, bool isBorrowed = false)
        {
            Id = Guid.NewGuid();
            Name = name;
            ISBN = iSBN;
            Price = price;
            NrCopies = nrCopies;
            IsBorrowed = isBorrowed;
        }

        public Book(int nrCopies = 1, bool isBorrowed = false)
        {
            Id = Guid.NewGuid();
            NrCopies = nrCopies;
            IsBorrowed = isBorrowed;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Book);
        }

        public bool Equals(Book book)
        {
            return book != null &&
                   Name == book.Name &&
                   ISBN == book.ISBN &&
                   Price == book.Price;
        }

        public bool IsCopyOfExistingBook(Book book, List<Book> bookList)
        {
            foreach (var b in bookList)
                if (book.Equals(b)) return true;

            return false;
        }

        //public void AddCopyOfExistinhBook(Book book)
        //{
        //    book.NrCopies++;
        //}

        public static void getAllBooks(List<Book> bookList)
        {
            if (!bookList.Any())
            {
                Console.WriteLine("In the library we don't have any books added yet!\n");
                return;
            }
            else
            {
                Console.WriteLine("In the library we have the following books:\n");
                foreach (var b in bookList)
                    Console.WriteLine(b.ToString());
            }
        }

        public static void getAvailableBooks(List<Book> bookList)
        {
            var availableBooks = bookList.Where(book => !book.IsBorrowed);

            foreach (var b in availableBooks)
                Console.WriteLine(b.ToString());
        }

        public static List<Book> GetAvailableCopyByNameAndAuthor(string name, string author, List<Book> bookList)
        {
            return bookList.Where(book => book.Name == name && book.Author == author && !book.IsBorrowed).ToList();
        }

        public static int getNrAvailableCopiesForBook(string name, string author, List<Book> bookList)
        {
            var nrOfCopiesAvailable = Book.GetAvailableCopyByNameAndAuthor(name, author, bookList).Count();
            return nrOfCopiesAvailable;
        }

        public void BorrowBook()
        {
            this.IsBorrowed = true;
        }

        public void RestoreBook()
        {
            this.IsBorrowed = false;
        }

        public override string ToString()
        {
            return String.Concat("\nBook ", this.Name, " by ", this.Author, " , ISBN ", this.ISBN, " ,price " + this.Price + " RON");
        }
    }
}
