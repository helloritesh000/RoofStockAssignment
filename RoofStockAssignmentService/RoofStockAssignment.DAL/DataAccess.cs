using Microsoft.Extensions.Configuration;
using RoofStockAssignent.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace RoofStockAssignment.DAL
{
    public class DataAccess
    {
        public List<Property> GetSavedProperties()
        {
            List<Property> properties = null;
            using (SqlConnection con = new SqlConnection(AppConfiguration.GetConfiguration().GetConnectionString("RoofStockConnection")))
            {
                con.Open();
                SqlCommand sqlCommand = new SqlCommand("USP_GetProperties", con);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    properties = new List<Property>();
                    while (sqlDataReader.Read())
                    {
                        Property property = new Property();
                        property.Id = Convert.ToInt32(sqlDataReader["Id"]);
                        property.JsonId = Convert.ToInt32(sqlDataReader["JsonId"]);
                        properties.Add(property);
                    }
                }
            }
            return properties;
        }

        public bool SaveProperty(Property property)
        {
            bool isSaved = false;
            using (SqlConnection con = new SqlConnection(AppConfiguration.GetConfiguration().GetConnectionString("RoofStockConnection")))
            {
                con.Open();
                SqlCommand sqlCommand = new SqlCommand("USP_SaveProperty", con);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = con;
                SqlParameter[] pars = new SqlParameter[] {
                    new SqlParameter("@JsonId", property.JsonId),
                    new SqlParameter("@YearBuilt", property.YearBuilt),
                    new SqlParameter("@ListPrice", property.ListPrice),
                    new SqlParameter("@MonthlyRent", property.MonthlyRent),
                    new SqlParameter("@GrossYield", property.GrossYield),

                    new SqlParameter("@Address1", property.Address.Address1 != null ? property.Address.Address1 : string.Empty),
                    new SqlParameter("@Address2", (property.Address.Address2 != null ? property.Address.Address2 : string.Empty)),
                    new SqlParameter("@City", property.Address.City != null ? property.Address.City : string.Empty),
                    new SqlParameter("@Country", property.Address.Country != null ? property.Address.Country : string.Empty),
                    new SqlParameter("@County", property.Address.County != null ? property.Address.County : string.Empty),
                    new SqlParameter("@District", property.Address.District != null ? property.Address.District : string.Empty),
                    new SqlParameter("@State", property.Address.State != null ? property.Address.State : string.Empty),
                    new SqlParameter("@Zip", property.Address.Zip != null ? property.Address.Zip : string.Empty),
                    new SqlParameter("@ZipPlus4", property.Address.ZipPlus4 != null ? property.Address.ZipPlus4 : string.Empty),

                };
                sqlCommand.Parameters.AddRange(pars);
                int noOfRowsAffected = sqlCommand.ExecuteNonQuery();
                if (noOfRowsAffected > 1)
                    isSaved = true;
            }
            return isSaved;
        }
    }
}
