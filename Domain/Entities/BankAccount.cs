using System.Security.Principal;
using System.Transactions;
using VaultBank.Domain.Enums;

namespace VaultBank.Domain.Entities
{
    public class BankAccount
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public string AccountNumber { get; set; } = string.Empty;
        public AccountType AccountType { get; set; }

        public decimal Balance { get; private set; }
        public bool IsFrozen { get; private set; }

        public List<Transaction> Transactions { get; set; } = new();

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be positive", nameof(amount));

            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Withdraw amount must be positive", nameof(amount));

            if (amount > Balance)
                throw new InvalidOperationException("Insufficient funds.");

            Balance -= amount;
        }

        public void Freeze() => IsFrozen = true;
        public void Unfreeze() => IsFrozen = false;

    }
}
