using System;
using BankingApp.Data;

namespace BankingApp.Services
{
    public class TransactionService
    {
        public void Deposit(int accountId, decimal amount)
        {
            string updateBalanceQuery = "UPDATE Accounts SET Balance = Balance + @Amount WHERE AccountId = @AccountId";
            string addTransactionQuery = "INSERT INTO Transactions (AccountId, TransactionType, Amount, Date) VALUES (@AccountId, @TransactionType, @Amount, @Date)";
            DatabaseHelper db = new DatabaseHelper();

            db.ExecuteQuery(updateBalanceQuery, cmd =>
            {
                cmd.Parameters.AddWithValue("@Amount", amount);
                cmd.Parameters.AddWithValue("@AccountId", accountId);
            });

            db.ExecuteQuery(addTransactionQuery, cmd =>
            {
                cmd.Parameters.AddWithValue("@AccountId", accountId);
                cmd.Parameters.AddWithValue("@TransactionType", "Deposit");
                cmd.Parameters.AddWithValue("@Amount", amount);
                cmd.Parameters.AddWithValue("@Date", DateTime.Now);
            });

            Console.WriteLine("Deposit successful!");
        }

        public void Withdraw(int accountId, decimal amount)
        {
            string checkBalanceQuery = "SELECT Balance FROM Accounts WHERE AccountId = @AccountId";
            string updateBalanceQuery = "UPDATE Accounts SET Balance = Balance - @Amount WHERE AccountId = @AccountId";
            string addTransactionQuery = "INSERT INTO Transactions (AccountId, TransactionType, Amount, Date) VALUES (@AccountId, @TransactionType, @Amount, @Date)";
            DatabaseHelper db = new DatabaseHelper();

            var result = db.ExecuteSelect(checkBalanceQuery, cmd =>
            {
                cmd.Parameters.AddWithValue("@AccountId", accountId);
            });

            if (result.Rows.Count > 0)
            {
                decimal currentBalance = (decimal)result.Rows[0]["Balance"];
                if (currentBalance >= amount)
                {
                    db.ExecuteQuery(updateBalanceQuery, cmd =>
                    {
                        cmd.Parameters.AddWithValue("@Amount", amount);
                        cmd.Parameters.AddWithValue("@AccountId", accountId);
                    });

                    db.ExecuteQuery(addTransactionQuery, cmd =>
                    {
                        cmd.Parameters.AddWithValue("@AccountId", accountId);
                        cmd.Parameters.AddWithValue("@TransactionType", "Withdraw");
                        cmd.Parameters.AddWithValue("@Amount", amount);
                        cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                    });

                    Console.WriteLine("Withdrawal successful!");
                }
                else
                {
                    Console.WriteLine("Insufficient balance.");
                }
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }
    }
}
