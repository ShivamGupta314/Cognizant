using System;
using BankingApp.Data;

namespace BankingApp.Services
{
    public class AccountService
    {
        public void CreateAccount(int userId, string accountNumber)
        {
            string query = "INSERT INTO Accounts (UserId, AccountNumber, Balance) VALUES (@UserId, @AccountNumber, @Balance)";
            DatabaseHelper db = new DatabaseHelper();
            db.ExecuteQuery(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@AccountNumber", accountNumber);
                cmd.Parameters.AddWithValue("@Balance", 0);
            });
            Console.WriteLine("Account created successfully!");
        }

        public void ViewBalance(int accountNumber)
        {
            string query = "SELECT Balance FROM Accounts WHERE AccountNumber = @AccountNumber";
            DatabaseHelper db = new DatabaseHelper();
            var result = db.ExecuteSelect(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@AccountNumber", accountNumber);
            });

            if (result.Rows.Count > 0)
            {
                decimal balance = (decimal)result.Rows[0]["Balance"];
                Console.WriteLine($"Current Balance: ₹{balance}");
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }
    }
}
