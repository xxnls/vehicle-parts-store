using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Vehicle_Parts_Store.Items;

namespace Vehicle_Parts_Store
{
    public class Order
    {
        #region FieldsAndProperties
        private static int ID_Assignment = 1;
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public int CustomerID { get; set; }
        //item, quantity
        public Dictionary<Part, int> OrderList;
        //delegate definition
        public delegate void CalculateOrderValueDelegate(Dictionary<Part, int> orderList);
        public event CalculateOrderValueDelegate OnOrderCreate;
        public int OrderValue { get; set; }

        public DateTime CompletionDate { get; set; }

        public Order(int employeeID, int customerID, Dictionary<Part, int> orderList)
        {
            ID = ID_Assignment;
            EmployeeID = employeeID;
            CustomerID = customerID;
            OrderList = orderList;

            //OrderValue = 0;

            //foreach (KeyValuePair<Part, int> item in OrderList)
            //{
            //    int price = item.Key.Price * item.Value;
            //    OrderValue += price;
            //}

            //add method do event
            OnOrderCreate += CalculateOrderValue;

            CompletionDate = DateTime.Now;
            
            ID_Assignment++;
        }
        #endregion
        #region Methods
        public override string ToString()
        {
            string output = "";

             output += $"""
                       ID: {ID}
                       Employee ID: {EmployeeID}
                       Customer ID: {CustomerID}
                       Order value: {OrderValue} zł
                       Completion date: {CompletionDate.ToString("dd/MM/yyyy")}

                       """;
            foreach (KeyValuePair<Part, int> item in OrderList)
                output += $"Part: {item.Key.Name} | Quantity: {item.Value} | Total price: {item.Key.Price * item.Value}\n";

            return output;
        }

        public void CalculateOrderValue(Dictionary<Part, int> orderList)
        {
            int orderValue = 0;
            foreach (KeyValuePair<Part, int> item in orderList)
            {
                int price = item.Key.Price * item.Value;
                orderValue += price;
            }
            OrderValue = orderValue;
        }

        public void OnOrder(Dictionary<Part, int> orderList)
        {
            OnOrderCreate.Invoke(orderList);
        }
        #endregion


    }
}
