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

        public decimal? CalculateFee(BorrowingDetails borrowingDetails)
        {
            var borrowingfee = (decimal?)(borrowingDetails.Book.Price * 0.1m * (decimal)(borrowingDetails.DateBorrowed - DateTime.Today).TotalDays);
            return borrowingfee;
        }

        public bool CheckIfBorrowingFeeExists(string ISBN, string firstName, string LastName, List<BorrowingDetails> borrowingDetailsList)
        {
            var borrowingDetails = borrowingDetailsList.Where(borrowingDetails => borrowingDetails.Book.ISBN == ISBN &&
                                                                                  borrowingDetails.Reader.FirstName == firstName &&
                                                                                  borrowingDetails.Reader.LastName == LastName).FirstOrDefault();

            var nuumberOfdaysTillRestore = (borrowingDetails.DateBorrowed - DateTime.Today).TotalDays;
            if (nuumberOfdaysTillRestore > 14) return true;

            return false;
        }
    }
}
