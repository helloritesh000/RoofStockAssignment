using System;
using System.Collections.Generic;
using System.Text;

namespace RoofStockAssignent.Domain
{
    public class Address
    {
        public int Id { get; set; }
        public string Address1 { get; set; }
		public string Address2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string County { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string ZipPlus4 { get; set; }
    }
}
