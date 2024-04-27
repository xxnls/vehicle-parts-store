using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle_Parts_Store.Items
{
    internal class EnginePart : Part
    {
        public string EngineSize { get; set; }
        public string Description { get; set; }

        public EnginePart(string name, string brand, string vehicleType, int quantity, int price, string engineSize, string description) : base(name, brand, vehicleType, quantity, price)
        {
            EngineSize = engineSize;
            Description = description;
        }

        public override string ToString()
        {
            return base.ToString() + "\n" + $"""
                                            Engine size: {EngineSize}
                                            Description: {Description}
                                            """;
        }
    }
}
