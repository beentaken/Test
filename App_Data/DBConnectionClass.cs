using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Security.Cryptography;


namespace WebApplication.App_Data
{
    public class DBConnectionClass
    {
        static String connectionString = ConfigurationManager.ConnectionStrings["Shoppingconnectinstring"].ConnectionString;
        SqlConnection connection = null;
        internal DBConnectionClass()
        {
            connection = new SqlConnection(connectionString);
        }
        public void OpenConnection()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
        }
        public void CloseConnection()
        {
            if (connection.State != System.Data.ConnectionState.Closed)
            {
                connection.Close();
            }
        }
        public DataSet DBViews(String Views)//"ViewProducts"
        {
            DataSet dataset = new DataSet();
            try
            {
                using (SqlCommand command = new SqlCommand("SELECT * FROM " + Views, connection))
                {
                    OpenConnection();
                    command.CommandType = CommandType.Text;

                    // Create a SqlDataAdapter object to fill the DataSet with the results of the stored procedure
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    // Fill the DataSet with the results of the stored procedure
                    adapter.Fill(dataset, Views);
                }
                return dataset;
            }
            catch (SqlException ex)
            {
                // Handle the exception
                Console.WriteLine("An error occurred while executing the stored procedure: " + ex.Message);
                return dataset;
            }
        }

        public void ExecuteStoredProcedure(String Storeproc, out int outParam1, out string outParam2)
        {
            string storedProcedureName = "YourStoredProcedure";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(Storeproc, connection);
                command.CommandType = CommandType.StoredProcedure;

                // Add output parameters
                SqlParameter outParam1 = new SqlParameter("@OutParam1", SqlDbType.Int);
                outParam1.Direction = ParameterDirection.Output;
                command.Parameters.Add(outParam1);

                SqlParameter outParam2 = new SqlParameter("@OutParam2", SqlDbType.NVarChar, 100);
                outParam2.Direction = ParameterDirection.Output;
                command.Parameters.Add(outParam2);

                connection.Open();
                command.ExecuteNonQuery();

                // Retrieve output parameter values
                outParam1 = (int)command.Parameters["@OutParam1"].Value;
                outParam2 = command.Parameters["@OutParam2"].Value.ToString();
            }
        }

        public String DBStoreprocInOut(String Storeproc, Dictionary<String, Object> keyValueIn, out Dictionary<String, Object> keyValueOut)
        {
            String message = "";
            try
            {

                // Create a SqlCommand object to execute the stored procedure
                using (SqlCommand command = new SqlCommand(Storeproc, connection))
                {
                    OpenConnection();
                    command.CommandType = CommandType.StoredProcedure;

                    foreach (KeyValuePair<string, object> parameter in keyValueIn)
                    {
                        command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                    }

                    DataSet dataset = new DataSet();
                    // Create a SqlDataAdapter object to fill the DataSet with the results of the stored procedure
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    // Fill the DataSet with the results of the stored procedure
                    adapter.Fill(dataset, Storeproc);

                    // Add an output parameter to the stored procedure to capture the return message
                    SqlParameter messageParam = new SqlParameter("@Message", SqlDbType.NVarChar, 100);

                    foreach (KeyValuePair<string, object> parameter in keyValueIn)
                    {
                        command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                    } 




                    messageParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(messageParam);

                    // Execute the stored procedure
                    command.ExecuteNonQuery();

                    // Retrieve the return message from the stored procedure
                    message = command.Parameters["@message"].Value.ToString();

                    // Close the connection to the database
                }

                return message;
            }
            catch (SqlException ex)
            {
                return "An error occurred while executing the stored procedure: " + ex.Message;
            }
        }

        public String DBStoreprocInMsg(String Storeproc, Dictionary<String, Object> keyValuePairs)
        {
            String message = "";
            try
            {

                // Create a SqlCommand object to execute the stored procedure
                using (SqlCommand command = new SqlCommand(Storeproc, connection))
                {
                    OpenConnection();
                    command.CommandType = CommandType.StoredProcedure;

                    foreach (KeyValuePair<string, object> parameter in keyValuePairs)
                    {
                        command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                    }

                    DataSet dataset = new DataSet();
                    // Create a SqlDataAdapter object to fill the DataSet with the results of the stored procedure
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    // Fill the DataSet with the results of the stored procedure
                    adapter.Fill(dataset, Storeproc);

                    // Add an output parameter to the stored procedure to capture the return message
                    SqlParameter messageParam = new SqlParameter("@Message", SqlDbType.NVarChar, 100);
                    messageParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(messageParam);

                    // Execute the stored procedure
                    command.ExecuteNonQuery();

                    // Retrieve the return message from the stored procedure
                    message = command.Parameters["@message"].Value.ToString();

                    // Close the connection to the database
                }

                return message;
            }
            catch (SqlException ex)
            {
                return "An error occurred while executing the stored procedure: " + ex.Message;
            }
        }
    }
}