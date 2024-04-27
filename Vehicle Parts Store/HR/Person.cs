using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle_Parts_Store.HR
{
    public class Person
    {
        #region PropertiesAndFields
        private static int Id_assignment = 1;

        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        #endregion

        #region Constructors
        public Person(string firstName, string lastName, string phoneNumber)
        {
            ID = Id_assignment;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;

            Id_assignment++;
        }
        #endregion

        #region Methods
        //public override string ToString()
        //{
        //    return $"""
        //            ID: {ID}
        //            {FirstName}
        //            {LastName}
        //            Phone: {PhoneNumber}
        //            """;
        //}

        public override string ToString()
        {
            return $"""
                    ID: {ID} | {FirstName} {LastName}
                    Phone number: {PhoneNumber}
                    """;
        }
        #endregion
    }
}
