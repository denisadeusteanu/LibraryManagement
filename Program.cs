using Library.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Library
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to library management!");
            List<Book> bookList = new List<Book>();
            List<BorrowingDetails> borrowingList = new List<BorrowingDetails>();
            List<Reader> readersList = new List<Reader>();

            bool closeConsole = false;

            Console.WriteLine("\nMenu\n" +
                "1)Add new book\n" +
                "2)See all books\n" +
                "3)See number of copies for a book\n" +
                "4)Borrow book\n" +
                "5)Return book\n" +
                "6)Close Console\n\n");

            while (!closeConsole)
            {
                Console.Write("\nChoose your option from menu by entering the number of the option you want:");

                int option;
                if (int.TryParse(Console.ReadLine(), out option))
                {
                    switch (option)
                    {
                        case 1:
                            AddNewBook(bookList);
                            break;
                        case 2:
                            Book.getAllBooks(bookList);
                            break;
                        case 3:
                            EnterBookAndGetNrOfCopies(bookList);
                            break;
                        case 4:
                            Borrow(bookList, borrowingList, readersList);
                            break;
                        case 5:
                            ReturnBook(borrowingList);
                            break;
                        case 6:
                            Console.WriteLine("Thank you");
                            closeConsole = true;
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid option\nPlease retry!");
                }
            }
        }

        private static void ReturnBook(List<BorrowingDetails> borrowingList)
        {
            Console.Write("Reader First Name:");
            var firstName = Console.ReadLine();
            Console.Write("Reader Last Name:");
            var lastName = Console.ReadLine();
            Console.Write("Book Name:");
            var name = Console.ReadLine();
            Console.Write("Book Author:");
            var author = Console.ReadLine();
            var borrowingDetails = BorrowingDetails.GetBorrowingDetails(firstName, lastName, name, author, borrowingList);
            if (borrowingDetails == null)
            {
                Console.WriteLine("This book does not exist!");
                return;
            }
            if (borrowingDetails.CheckIfBorrowingFeeExists())
            {
                var fee = System.Math.Round((decimal)borrowingDetails.CalculateFee(),2);
                Console.WriteLine($"You have to pay {fee}", fee);
            }



            borrowingDetails.Book.RestoreBook();
            borrowingList.Remove(borrowingDetails);
            Console.WriteLine(borrowingDetails.Book.ToString() + " was restored succesfully!");
        }

        private static void Borrow(List<Book> bookList, List<BorrowingDetails> borrowingList, List<Reader> readersList)
        {
            Reader reader = new Reader();
            Console.Write("Reader First Name:");
            reader.FirstName = Console.ReadLine();
            Console.Write("Reader Last Name:");
            reader.LastName = Console.ReadLine();
            Console.Write("Book Name:");
            var name = Console.ReadLine();
            Console.Write("Book Author:");
            var author = Console.ReadLine();

            var bookToBorrow = Book.GetAvailableCopyByNameAndAuthor(name, author, bookList).FirstOrDefault();
            if (bookToBorrow == null)
            {
                Console.WriteLine("This book does not exist!");
                return;
            }
            bookToBorrow.BorrowBook();
            BorrowingDetails borrowingDetails = new BorrowingDetails(bookToBorrow, reader);
            readersList.Add(reader);
            borrowingList.Add(borrowingDetails);

            Console.WriteLine(bookToBorrow.ToString() + " was borrowed succesfully!");
        }

        public static void EnterBookAndGetNrOfCopies(List<Book> bookList)
        {
            Console.Write("Book Name:");
            var name = Console.ReadLine();
            Console.Write("Book Author:");
            var author = Console.ReadLine();
            int nrOfCopies = Book.getNrAvailableCopiesForBook(name, author, bookList);

            if (nrOfCopies == 0)
            {
                Console.WriteLine("This book does not exist in the library!");
                return;
            }

            Console.Write("Book {0} by {1} is in {2} copies", name, author, nrOfCopies);
        }

        public static void AddNewBook(List<Book> bookList)
        {
            Book book = new Book();
            var results = new Collection<ValidationResult>();
            Console.Write("Book Name:");
            book.Name = Console.ReadLine();

            Console.Write("Book Author:");
            var author = Console.ReadLine();
            if (Regex.IsMatch(author, @"^[a-zA-Z]+$"))
                book.Author = author;
            else
            {
                Console.WriteLine("Input for author is not correct!");
                return;
            }

            Console.Write("Book Price:");
            decimal price;
            if (Decimal.TryParse(Console.ReadLine(), out price) && price > 0)
                book.Price = price;
            else
            {
                Console.WriteLine("Input for price is not correct!");
                return;
            }

            Console.Write("Book ISBN:");
            book.ISBN = Console.ReadLine();

            if (!Validator.TryValidateObject(book, new ValidationContext(book), results, true))
            {
                Console.WriteLine($"{results[0].ErrorMessage}");
            };

            if (results.Count() == 0)
            {
                results.Clear();
                bookList.Add(book);
                Console.WriteLine("You added a new book successfully!");
            }
        }
    }
}
