using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle_Parts_Store.HR
{
    internal class Employee : Person
    {
        #region PropertiesAndFields
        public int Age { get; set; }
        public int Salary { get; set; }
        public string Position { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime ResignDate { get; set; }
        public List<int> CompletedOrdersIDs { get; set; } = new List<int>();

        #endregion
        #region Constructors
        public Employee(string firstName, string lastName, string phoneNumber, string position, int age, int salary) : base(firstName, lastName, phoneNumber)
        {
            Age = age;
            Salary = salary;
            JoinDate = DateTime.Now;
            ResignDate = DateTime.Parse("01/01/0001");
            Position = position;
        }
        #endregion
        #region Methods
        public override string ToString()
        {
            return base.ToString() + "\n" + $"""
                                            Age: {Age}
                                            Salary: {Salary}
                                            Position: {Position}
                                            Join date: {JoinDate.ToShortDateString()}
                                            Resign date: {(ResignDate != DateTime.Parse("01/01/0001") ? ResignDate.ToShortDateString() : "-")}
                                            """;
        }
        #endregion
    }
}
