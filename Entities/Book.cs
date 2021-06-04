using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Library.CustomAttributes;

namespace Library.Entities
{
    public class Book
    {
        private Guid Id { get; set; }

        [NotNullOrWhiteSpaceValidatorAttribute]
        public string Name { get; set; }

        [NotNullOrWhiteSpaceValidatorAttribute]
        public string Author { get; set; }

        [NotNullOrWhiteSpaceValidatorAttribute]
        public string ISBN { get; set; }

        [NotNullOrWhiteSpaceValidatorAttribute]
        public decimal? Price { get; set; }

        public Boolean IsBorrowed { get; set; }

        public Book( bool isBorrowed = false)
        {
            Id = Guid.NewGuid();
            IsBorrowed = isBorrowed;
        }

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
