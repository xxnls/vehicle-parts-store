using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle_Parts_Store.Items
{
    internal class ElectricalPart : Part
    {
        public bool IsHighVoltage { get; set; }
        public string Size { get; set; }

        public ElectricalPart(string name, string brand, string vehicleType, int quantity, int price, bool isHighVoltage, string size) : base(name, brand, vehicleType, quantity, price)
        {
            this.IsHighVoltage= isHighVoltage;
            this.Size= size;
        }

        public override string ToString()
        {
            return base.ToString() + "\n" + $"""
                                            Is high voltage: {(IsHighVoltage == true ? "Yes" : "No")}
                                            Size: {Size}
                                            """;
        }
    }
}
