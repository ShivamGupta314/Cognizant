using System;
using BankingApp.Data;
using BankingApp.Services;

namespace BankingApp
{
    class Program
    {
        static void Main(string[] args)
        {

            var db = new DatabaseHelper();
            db.TestConnection();
            AccountService accountService = new AccountService();
            TransactionService transactionService = new TransactionService();

            Console.WriteLine("Welcome to the Banking Management System!");
            bool running = true;

            while (running)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. View Balance");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Console.Write("Enter User ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int userId))
                        {
                            Console.WriteLine("Invalid User ID.");
                            break;
                        }
                        Console.Write("Enter Account Number: ");
                        string accountNumber = Console.ReadLine();
                        accountService.CreateAccount(userId, accountNumber);
                        break;

                    case 2:
                        Console.Write("Enter Account ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int accountIdForDeposit))
                        {
                            Console.WriteLine("Invalid Account ID.");
                            break;
                        }
                        Console.Write("Enter Deposit Amount: ");
                        if (!decimal.TryParse(Console.ReadLine(), out decimal depositAmount) || depositAmount <= 0)
                        {
                            Console.WriteLine("Invalid amount. Please enter a positive number.");
                            break;
                        }
                        transactionService.Deposit(accountIdForDeposit, depositAmount);
                        break;

                    case 3:
                        Console.Write("Enter Account ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int accountIdForWithdraw))
                        {
                            Console.WriteLine("Invalid Account ID.");
                            break;
                        }
                        Console.Write("Enter Withdrawal Amount: ");
                        if (!decimal.TryParse(Console.ReadLine(), out decimal withdrawalAmount) || withdrawalAmount <= 0)
                        {
                            Console.WriteLine("Invalid amount. Please enter a positive number.");
                            break;
                        }
                        transactionService.Withdraw(accountIdForWithdraw, withdrawalAmount);
                        break;

                    case 4:
                        Console.Write("Enter Account Number: ");
                        if (!int.TryParse(Console.ReadLine(), out int accountNumberForView))
                        {
                            Console.WriteLine("Invalid Account ID.");
                            break;
                        }
                        accountService.ViewBalance(accountNumberForView);
                        break;

                    case 5:
                        Console.WriteLine("Exiting the application. Goodbye!");
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
