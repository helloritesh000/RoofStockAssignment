using System;

namespace RoofStockAssignent.Domain
{
    public class Property
    {
        public int Id { get; set; }
        public int JsonId { get; set; }
        public Address Address { get; set; }
        public int YearBuilt { get; set; }
        public double ListPrice { get; set; }
        public double MonthlyRent { get; set; }
        public double GrossYield { get; set; }
    }
}
