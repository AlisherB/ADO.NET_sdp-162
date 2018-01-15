using ClassLibrary.DTOs;
using ClassLibrary.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.DAOs
{
    public class CustomersDAO_DataSet : IDAO<CustomersDto>
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ToString();

        string sql = @"SELECT * FROM Customers";

        public string Create(CustomersDto dto)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "Customers");

                    DataRow dataRowToAdd = dataSet.Tables["Customers"].NewRow();

                    foreach (var item in dto.GetType().GetProperties())
                    {
                        dataRowToAdd[item.Name] = item.GetValue(dto, null);
                    }

                    dataSet.Tables["Customers"].Rows.Add(dataRowToAdd);

                    SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);

                    adapter.Update(dataSet.Tables["Customers"]);
                }
                sqlConnection.Close();
                return dto.CustomerId.ToString();
            }
        }

        public CustomersDto Read(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "CustomersDto");
                    dataSet.Tables["CustomersDto"].PrimaryKey = new DataColumn[] { dataSet.Tables["CustomersDto"].Columns["CustomerId"] };

                    DataRow dataRowToReturn = dataSet.Tables["CustomersDto"].Rows.Find(id);

                    foreach (var item in dataRowToReturn.ItemArray.ToList())
                    {
                        Console.WriteLine(item);
                    }
                }
                sqlConnection.Close();
            }
            return null;
        }

        public string Update(CustomersDto dto)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);

                    DataColumn[] key = new DataColumn[1];
                    key[0] = dataSet.Tables[0].Columns[0];
                    dataSet.Tables[0].PrimaryKey = key;

                    DataRow dataRow = dataSet.Tables[0].Rows.Find(dto.CustomerId);

                    dataRow.BeginEdit();

                    dataRow["CustomerId"] = dto.CustomerId;

                    dataRow.EndEdit();
                    SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);

                    adapter.Update(dataSet);
                }
                sqlConnection.Close();
                return dto.CustomerId.ToString();
            }
        }
        public void Delete(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);

                    DataColumn[] key = new DataColumn[1];
                    key[0] = dataSet.Tables[0].Columns[0];
                    dataSet.Tables[0].PrimaryKey = key;

                    DataRow toDelete = dataSet.Tables[0].Rows.Find(id);

                    toDelete.Delete();
                    adapter.Update(dataSet);
                }
                sqlConnection.Close();
            }
        }

        public ICollection<CustomersDto> Read()
        {
            throw new NotImplementedException();
        }
    }
}
