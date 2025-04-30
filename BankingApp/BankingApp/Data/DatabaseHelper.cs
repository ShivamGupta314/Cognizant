using System;
using System.Data;
using Microsoft.Data.SqlClient;


namespace BankingApp.Data
{
    public class DatabaseHelper
    {
        // Connection string to connect to the SQL Server database
        // Replace 'Server=localhost;Database=BankingAppDb' with your actual database connection details if needed.
        private string connectionString = @"Server=localhost\SQLExpress;Database=BankingAppDb;Trusted_Connection=True;TrustServerCertificate=True;";

        /// <summary>
        /// Executes a non-query (e.g., INSERT, UPDATE, DELETE) on the database.
        /// </summary>
        /// <param name="query">The SQL query to execute.</param>
        /// <param name="parameterize">Action to add parameters to the query.</param>

        public void ExecuteQuery(string query, Action<SqlCommand> parameterize = null)
        {
            // Function implementation
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open(); // Opens the connection to the database

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters if specified
                        parameterize?.Invoke(command);
                        command.ExecuteNonQuery(); // Executes the query
                    }
                }
            }
            catch (SqlException ex)
            {
                // Log or print SQL-related errors
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Log or print general errors
                Console.WriteLine($"Unexpected Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Executes a SELECT query and retrieves data from the database.
        /// </summary>
        /// <param name="query">The SQL query to execute.</param>
        /// <param name="parameterize">Action to add parameters to the query.</param>
        /// <returns>A DataTable containing the result set.</returns>
        public DataTable ExecuteSelect(string query, Action<SqlCommand> parameterize = null)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open(); // Opens the connection to the database

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters if specified
                        parameterize?.Invoke(command);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table); // Fills the DataTable with the query result set
                            return table;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Log or print SQL-related errors
                Console.WriteLine($"SQL Error: {ex.Message}");
                return null; // Return null if there is an SQL error
            }
            catch (Exception ex)
            {
                // Log or print general errors
                Console.WriteLine($"Unexpected Error: {ex.Message}");
                return null; // Return null if there is a general error
            }
        }

        /// <summary>
        /// Tests the database connection.
        /// </summary>
        public void TestConnection()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open(); // Opens the connection to the database
                    Console.WriteLine("Connection to the database was successful!");
                }
            }
            catch (SqlException ex)
            {
                // Log or print SQL-related errors
                Console.WriteLine($"Connection failed: SQL Error - {ex.Message}");
            }
            catch (Exception ex)
            {
                // Log or print general errors
                Console.WriteLine($"Connection failed: Unexpected Error - {ex.Message}");
            }
        }
    }
}
