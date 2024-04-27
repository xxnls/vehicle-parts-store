using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle_Parts_Store.Items
{
    public class Part
    {
        #region PropertiesAndFields
        public enum VehicleTypeAllowed
        {
            None,
            Car,
            Truck,
            Motorcycle
        }

        private static int Id_assignment = 1;

        public int ID { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public int Quantity { get; set; }
        private string _vehicleType;
        public string VehicleType
        {
            get { return _vehicleType; }
            set
            {
                if (value != VehicleTypeAllowed.None.ToString() ||
                   value != VehicleTypeAllowed.Car.ToString() ||
                   value != VehicleTypeAllowed.Truck.ToString() ||
                   value != VehicleTypeAllowed.Motorcycle.ToString())
                    _vehicleType = VehicleTypeAllowed.None.ToString();
                else
                    _vehicleType = value;
            }
        }
        public int Price { get; set; }
        public DateTime WhenCreated { get; set; }

        #endregion
        #region Constructors
        public Part(string name, string brand, string vehicleType, int quantity, int price)
        {
            ID = Id_assignment;
            Name = name;
            Brand = brand;
            Quantity = quantity;
            VehicleType = vehicleType;
            Price = price;
            WhenCreated = DateTime.Now;

            Id_assignment++;
        }
        #endregion
        #region Methods
        public override string ToString()
        {
            return $"""
                    ID: {ID} | {Brand} | {Name}
                    Quantity: {Quantity}
                    Vehicle type: {VehicleType}
                    Price: {Price}
                    """;
        }
        #endregion
    }
}
