using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Entities
{
    class Reader
    {
        private Guid Id { get; set; }
        public string FirstName { get; set; }
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

        public override bool Equals(object obj)
        {
            return Equals(obj as Reader);
        }

        public bool Equals(Reader reader)
        {
            return reader != null &&
                   FirstName == reader.FirstName &&
                   LastName == reader.LastName;
        }
    }
}
