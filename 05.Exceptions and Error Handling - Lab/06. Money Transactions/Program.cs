using System;
using System.Collections.Generic;

namespace _06._Money_Transactions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, double> bankAccounts = CreateAccounts();
            string input;
            while ((input=Console.ReadLine()) != "End")
            {
                string[] commands = input.Split();
                try
                {
                    ValidateData(bankAccounts, commands);
                    ExecuteCommand(bankAccounts, commands);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.WriteLine("Enter another command");
                }
            }
        }

        public static void ExecuteCommand(Dictionary<int, double> bankAccounts, string[] commands)
        {
            string action = commands[0];
            int accountNumber = int.Parse(commands[1]);
            double sum = double.Parse(commands[2]);
            if (action == "Deposit")
            {
                bankAccounts[accountNumber] += sum;
            }
            else
            {
                bankAccounts[accountNumber] -= sum;   
            }
            Console.WriteLine($"Account {accountNumber} has new balance: {bankAccounts[accountNumber]:f2}");
        }
        private static Dictionary<int, double> CreateAccounts()
        {
            string[] accounts = Console.ReadLine().Split(',');
            Dictionary<int, double> accountsAndBalances = new Dictionary<int, double>();
            foreach (var item in accounts)
            {
                string[] accountData = item.Split('-');
                int accountNum = int.Parse(accountData[0]);
                double balance = double.Parse(accountData[1]);
                accountsAndBalances[accountNum] = balance;
            }
            return accountsAndBalances;
        }

        public static void ValidateData(Dictionary<int, double> bankAccounts, string[] commands)
        {
            string action = commands[0];
            int accountNumber = int.Parse(commands[1]);
            double sum = double.Parse(commands[2]);

            if (action != "Deposit" && action != "Withdraw")
            {
                throw new ArgumentException("Invalid command!");
            }
            if (!bankAccounts.ContainsKey(accountNumber))
            {
                throw new InvalidOperationException("Invalid account!");
            }
            if (action == "Withdraw" && bankAccounts[accountNumber] - sum < 0)
            {
                throw new InvalidOperationException("Insufficient balance!");
            }
        }
    }
}
