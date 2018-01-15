using ClassLibrary.DTOs;
using ClassLibrary.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class CustomersDAO_ER : IDAO<CustomersDto>
    {
        private SqlConnection sqlConnection = null;
        private CustomersDto customerDTOToReturn;

        public string Create(CustomersDto t)
        {
            string baseSqlQuery = @"INSERT INTO [NORTHWNDSDP-162].[dbo].[Customers],
            ([CustomerID], [CompanyName], [ContactName], [ContactTitle], [Address],
            [City], [Region], [PostalCode], [Country], [Phone], [Fax])" +
            "VALUES (@customerID, @companyName, @contactName, @contactTitle, @address, @city, @region, @postalCode, @country, @phone, @fax)" +
            "SELECT Id AS @ReturnedId FROM [NORTHWNDSDP-162].[dbo].[Customers] WHERE CustomerID = @customerID";

            SqlParameter idParameter = new SqlParameter()
            {
                ParameterName = "@customerID",
                Value = t.CustomerId,
                SqlDbType = SqlDbType.NChar,
                Direction = ParameterDirection.Input
            };
            SqlParameter companyNameParameter = new SqlParameter()
            {
                ParameterName = "@companyName",
                Value = t.CompanyName,
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input
            };
            SqlParameter contactNameParameter = new SqlParameter()
            {
                ParameterName = "@contactName",
                Value = t.ContactName,
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input
            };
            SqlParameter contactTitleParameter = new SqlParameter()
            {
                ParameterName = "@contactTitle",
                Value = t.ContactTitle,
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input
            };
            SqlParameter addressParameter = new SqlParameter()
            {
                ParameterName = "@address",
                Value = t.Address,
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input
            };
            SqlParameter cityParameter = new SqlParameter()
            {
                ParameterName = "@city",
                Value = t.City,
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input
            };
            SqlParameter regionParameter = new SqlParameter()
            {
                ParameterName = "@region",
                Value = t.Region,
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input
            };
            SqlParameter postalCodeParameter = new SqlParameter()
            {
                ParameterName = "@postalCode",
                Value = t.PostalCode,
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input
            };
            SqlParameter countryParameter = new SqlParameter()
            {
                ParameterName = "@country",
                Value = t.Country,
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input
            };
            SqlParameter phoneParameter = new SqlParameter()
            {
                ParameterName = "@phone",
                Value = t.Phone,
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input
            };
            SqlParameter faxParameter = new SqlParameter()
            {
                ParameterName = "@Fax",
                Value = t.Fax,
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input
            };

            SqlParameter returnedIdParameter = new SqlParameter()
            {
                ParameterName = "@ReturnedId",
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Output,
                Size = 100
            };

            using (sqlConnection = DBConnectionFactory.GetConnection())
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(baseSqlQuery, sqlConnection))
                {
                    sqlCommand.CommandText = baseSqlQuery;
                    sqlCommand.CommandType = CommandType.Text;

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

                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    reader.Read();
                    string value = reader["@returnedId"].ToString();
                }
                sqlConnection.Close();
                return t.CustomerId.ToString();
            }
        }

        public CustomersDto Read(int id)
        {
            using (sqlConnection = DBConnectionFactory.GetConnection())
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    string baseSelectQuery = @"SELECT * FROM [NORTHWNDSDP-162].[dbo].[Customers]" +
                                            "WHERE [CustomerId = {0}]";
                    string realSelectQuery = String.Format(baseSelectQuery, id.ToString());

                    sqlCommand.CommandText = realSelectQuery;
                    sqlCommand.CommandType = CommandType.Text;

                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        customerDTOToReturn = new CustomersDto()
                        {
                            CustomerId = reader["CustomerId"].ToString(),
                            CompanyName = reader["CompanyName"].ToString(),
                            ContactName = reader["ContactName"].ToString(),
                            ContactTitle = reader["ContactTitle"].ToString(),
                            Address = reader["Address"].ToString(),
                            City = reader["City"].ToString(),
                            Region = reader["Region"].ToString(),
                            PostalCode = reader["PostalCode"].ToString(),
                            Country = reader["Country"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            Fax = reader["Fax"].ToString()
                        };
                    }
                }
                sqlConnection.Close();
            }
            return customerDTOToReturn;
        }

        public ICollection<CustomersDto> Read()
        {
            List<CustomersDto> customerDTOsToReturn = new List<CustomersDto>();
            using (sqlConnection = DBConnectionFactory.GetConnection())
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    string realSelectQuery = @"SELECT * FROM [NORTHWNDSDP-162].[dbo].[Customers]";

                    sqlCommand.CommandText = realSelectQuery;
                    sqlCommand.CommandType = CommandType.Text;

                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            customerDTOsToReturn.Add(new CustomersDto()
                            {
                                CustomerId = reader["CustomerId"].ToString(),
                                CompanyName = reader["CompanyName"].ToString(),
                                ContactName = reader["ContactName"].ToString(),
                                ContactTitle = reader["ContactTitle"].ToString(),
                                Address = reader["Address"].ToString(),
                                City = reader["City"].ToString(),
                                Region = reader["Region"].ToString(),
                                PostalCode = reader["PostalCode"].ToString(),
                                Country = reader["Country"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                Fax = reader["Fax"].ToString()
                            });
                        }
                    }
                }
                sqlConnection.Close();
            }
            return customerDTOsToReturn;
        }

        public void Delete(int id)
        {
            using (sqlConnection = DBConnectionFactory.GetConnection())
            {
                string baseQuery = "DELETE FROM [NORTHWNDSDP-162].[dbo].[Customers] WHERE CustomerId = '{0}'";
                string realQuery = String.Format(baseQuery, id);

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(realQuery, sqlConnection))
                {
                    sqlCommand.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }
        }

        public string Update(CustomersDto t)
        {
            using (sqlConnection = DBConnectionFactory.GetConnection())
            {
                string baseQuery = "UPDATE [NORTHWNDSDP-162].[dbo].[Customers] SET CustomerId = '{0}',CompanyName = '{1}',ContactName = '{2}',ContactTitle = {3},Address = {4},City = '{5}',Region = '{6}',PostalCode = {7},Country = {8},Phone = {9},Fax = {10}";
                string realQuery = String.Format(baseQuery, t.CustomerId, t.CompanyName,
                    t.ContactName, t.ContactTitle, t.Address, t.City, 
                    t.Region, t.PostalCode, t.Country, t.Phone, t.Fax);

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
