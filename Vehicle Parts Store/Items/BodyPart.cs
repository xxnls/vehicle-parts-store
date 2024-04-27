using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle_Parts_Store.Items
{
    internal class BodyPart : Part
    {
        public string Color { get; set; }
        public bool WithBolts { get; set; }

        public BodyPart(string name, string brand, string vehicleType, int quantity, int price, string color, bool withBolts) : base(name, brand, vehicleType, quantity, price)
        {
            this.Color = color;
            this.WithBolts= withBolts;
        }

        public override string ToString()
        {
            return base.ToString() + "\n" + $"""
                                            Color: {Color}
                                            With bolts: {(WithBolts == true ? "Yes" : "No")}
                                            """;
        }


    }
}
