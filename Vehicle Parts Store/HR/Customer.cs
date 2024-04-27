using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle_Parts_Store.HR
{
    internal class Customer : Person
    {
        #region FieldsAndProperties
        public bool IsCompany { get; set; }
        public List<int> OrdersMadeIDs { get; set; } = new List<int>();
        public int MoneySpent { get; set; }
        public DateTime WhenCreated { get; set; }
        #endregion
        #region Constructors
        public Customer(string firstName, string lastName, string phoneNumber, bool isCompany) : base(firstName, lastName, phoneNumber)
        {
            this.IsCompany = isCompany;
            WhenCreated = DateTime.Now;
        }
        #endregion
        #region Methods
        public override string ToString()
        {
            return base.ToString() + "\n" + $"""
                                            Is company: {(IsCompany == true ? "Yes" : "No")}
                                            Money spent: {MoneySpent} zł
                                            """;
        }
        #endregion
    }
}
