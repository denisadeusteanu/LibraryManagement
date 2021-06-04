using System;
using System.Collections.Generic;
using System.Text;
using Library.CustomAttributes;

namespace Library.Entities
{
    class Reader
    {
        private Guid Id { get; set; }

        [NotNullOrWhiteSpaceValidatorAttribute]
        public string FirstName { get; set; }

        [NotNullOrWhiteSpaceValidatorAttribute]
        public string LastName { get; set; }

        public Reader(string firstName, string lastName)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
        }

        public Reader()
        {
            Id = Guid.NewGuid();
        }
    }
}
