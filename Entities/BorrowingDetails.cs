using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Entities
{
    class BorrowingDetails
    {
        private Guid Id { get; set; }
        public Book Book { get; set; }
        public Reader Reader { get; set; }
        private DateTime DateBorrowed { get; set; }

        public BorrowingDetails(Book book, Reader reader)
        {
            Id = Guid.NewGuid();
            Book = book;
            Reader = reader;
            DateBorrowed = DateTime.Today;
        }

        public decimal? CalculateFee()
        {
            var numberOfLateDays = (decimal)System.Math.Round((this.DateBorrowed - DateTime.Today).TotalDays - 14d);
            var borrowingfee = (decimal?)(this.Book.Price * 0.01m * numberOfLateDays);
            return borrowingfee;
        }

        public bool CheckIfBorrowingFeeExists()
        {
            var numberOfdaysTillRestore = (this.DateBorrowed - DateTime.Today).TotalDays;
            if (numberOfdaysTillRestore > 14) return true;

            return false;
        }

        public static BorrowingDetails GetBorrowingDetails(string firstName, string lastName,string name, string author, List<BorrowingDetails> borrowingDetailsList)
        {
            var borrowingDetails = borrowingDetailsList.Where(borrowingDetails => borrowingDetails.Reader.FirstName == firstName && borrowingDetails.Reader.LastName == lastName
                                                                                 && borrowingDetails.Book.Name == name && borrowingDetails.Book.Author == author
                                                                                 && borrowingDetails.Book.IsBorrowed).FirstOrDefault();
            return borrowingDetails;
        }
    }
}
