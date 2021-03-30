using RoofStockAssignent.Domain;
using RoofStockAssignment.DAL;
using System;
using System.Collections.Generic;

namespace RoofStockAssignment.BL
{
    public class BusinessLogic
    {
        DataAccess dataAccess;
        public BusinessLogic()
        {
            dataAccess = new DataAccess();
        }
        public List<Property> GetSavedProperties()
        {
            return dataAccess.GetSavedProperties();
        }

        public bool SaveProperty(Property property)
        {
            return dataAccess.SaveProperty(property);
        }
    }
}
