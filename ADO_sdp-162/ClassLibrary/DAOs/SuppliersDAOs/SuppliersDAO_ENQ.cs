using ClassLibrary.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.DAOs
{
    public class SuppliersDAO_ENQ : IDAO<SuppliersDto>
    {
        private SqlConnection sqlConnection = null;
        private SuppliersDto supplierDTOToReturn;

        public string Create(SuppliersDto t)
        {
            using (sqlConnection = DBConnectionFactory.GetConnection())
            {
                SqlParameter idParameter = new SqlParameter("@SupplierId", SqlDbType.Int);
                SqlParameter companyNameParameter = new SqlParameter("@CompanyName", SqlDbType.NVarChar);
                SqlParameter contactNameParameter = new SqlParameter("@ContactName", SqlDbType.NVarChar);
                SqlParameter contactTitleParameter = new SqlParameter("@ContactTitle", SqlDbType.NVarChar);
                SqlParameter addressParameter = new SqlParameter("@Address", SqlDbType.NVarChar);
                SqlParameter cityParameter = new SqlParameter("@City", SqlDbType.NVarChar);
                SqlParameter regionParameter = new SqlParameter("@Region", SqlDbType.NVarChar);
                SqlParameter postalCodeParameter = new SqlParameter("@PostalCode", SqlDbType.NVarChar);
                SqlParameter countryParameter = new SqlParameter("@Country", SqlDbType.NVarChar);
                SqlParameter phoneParameter = new SqlParameter("@Phone", SqlDbType.NVarChar);
                SqlParameter faxParameter = new SqlParameter("@Fax", SqlDbType.NVarChar);
                SqlParameter homePageParameter = new SqlParameter("@HomePage", SqlDbType.NText);

                idParameter.Value = t.SupplierId;
                companyNameParameter.Value = t.CompanyName;
                contactNameParameter.Value = t.ContactName;
                contactTitleParameter.Value = t.ContactTitle;
                addressParameter.Value = t.Address;
                cityParameter.Value = t.City;
                regionParameter.Value = t.Region;
                postalCodeParameter.Value = t.ContactTitle;
                countryParameter.Value = t.Address;
                phoneParameter.Value = t.City;
                faxParameter.Value = t.Region;
                homePageParameter.Value = t.HomePage;

                sqlConnection.Open();
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "CreateNewSupplier";
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.Add(idParameter);
                    sqlCommand.Parameters.Add(companyNameParameter);
                    sqlCommand.Parameters.Add(contactNameParameter);
                    sqlCommand.Parameters.Add(contactTitleParameter);
                    sqlCommand.Parameters.Add(addressParameter);
                    sqlCommand.Parameters.Add(cityParameter);
                    sqlCommand.Parameters.Add(regionParameter);
                    sqlCommand.Parameters.Add(postalCodeParameter);
                    sqlCommand.Parameters.Add(countryParameter);
                    sqlCommand.Parameters.Add(phoneParameter);
                    sqlCommand.Parameters.Add(faxParameter);
                    sqlCommand.Parameters.Add(homePageParameter);

                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();

                    return t.SupplierId.ToString();
                }
            }
        }

        public SuppliersDto Read(int id)
        {
            using (sqlConnection = DBConnectionFactory.GetConnection())
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    string baseSelectQuery = @"SELECT * FROM [NORTHWNDSDP-162].[dbo].[Customers]" +
                                            "WHERE [SupplierId = {0}]";
                    string realSelectQuery = String.Format(baseSelectQuery, id.ToString());

                    sqlCommand.CommandText = realSelectQuery;
                    sqlCommand.CommandType = CommandType.Text;

                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        supplierDTOToReturn = new SuppliersDto()
                        {
                            SupplierId = Int32.Parse(reader["SupplierId"].ToString()),
                            CompanyName = reader["CompanyName"].ToString(),
                            ContactName = reader["ContactName"].ToString(),
                            ContactTitle = reader["ContactTitle"].ToString(),
                            Address = reader["Address"].ToString(),
                            City = reader["City"].ToString(),
                            Region = reader["Region"].ToString(),
                            PostalCode = reader["PostalCode"].ToString(),
                            Country = reader["Country"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            Fax = reader["Fax"].ToString(),
                            HomePage = reader["HomePage"].ToString()
                        };
                    }
                }
                sqlConnection.Close();
            }
            return supplierDTOToReturn;
        }

        public ICollection<SuppliersDto> Read()
        {
            List<SuppliersDto> supplierDTOsToReturn = new List<SuppliersDto>();
            using (sqlConnection = DBConnectionFactory.GetConnection())
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    string realSelectQuery = @"SELECT * FROM [NORTHWNDSDP-162].[dbo].[Suppliers]";

                    sqlCommand.CommandText = realSelectQuery;
                    sqlCommand.CommandType = CommandType.Text;

                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            supplierDTOsToReturn.Add(new SuppliersDto()
                            {
                                SupplierId = Int32.Parse(reader["CustomerId"].ToString()),
                                CompanyName = reader["CompanyName"].ToString(),
                                ContactName = reader["ContactName"].ToString(),
                                ContactTitle = reader["ContactTitle"].ToString(),
                                Address = reader["Address"].ToString(),
                                City = reader["City"].ToString(),
                                Region = reader["Region"].ToString(),
                                PostalCode = reader["PostalCode"].ToString(),
                                Country = reader["Country"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                Fax = reader["Fax"].ToString(),
                                HomePage = reader["HomePage"].ToString()
                            });
                        }
                    }
                }
                sqlConnection.Close();
            }
            return supplierDTOsToReturn;
        }

        public void Delete(int id)
        {
            using (sqlConnection = DBConnectionFactory.GetConnection())
            {
                string baseQuery = "DELETE FROM [NORTHWNDSDP-162].[dbo].[Suppliers] WHERE SupplierId = '{0}'";
                string realQuery = String.Format(baseQuery, id);

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(realQuery, sqlConnection))
                {
                    sqlCommand.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }
        }

        public string Update(SuppliersDto t)
        {
            using (sqlConnection = DBConnectionFactory.GetConnection())
            {
                string baseQuery = "UPDATE [NORTHWNDSDP-162].[dbo].[Suppliers] SET SupplierId = '{0}',CompanyName = '{1}',ContactName = '{2}',ContactTitle = {3},Address = {4},City = '{5}',Region = '{6}',PostalCode = {7},Country = {8},Phone = {9},Fax = {10},HomePage = {11}";
                string realQuery = String.Format(baseQuery, t.SupplierId, t.CompanyName,
                    t.ContactName, t.ContactTitle, t.Address,
                    t.City, t.Region, t.PostalCode, t.Country, t.Phone, t.Fax, t.HomePage);

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(realQuery, sqlConnection))
                {
                    sqlConnection.Close();
                    return sqlCommand.ExecuteNonQuery().ToString();
                }
            }
        }
    }
}
