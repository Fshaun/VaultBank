##VaultBank – Secure Banking Backend API

<h1>A Clean Architecture .NET 8 Banking System</h1>

VaultBank is a backend‑only banking API built in .NET 8, following strict Clean Architecture, SOLID principles, and constructor injection throughout the entire solution.
It simulates real banking operations such as customer management, account creation, deposits, withdrawals, transfers, and transaction auditing.

<h2>Architecture Overview</h2>

VaultBank follows Clean Architecture with the following layers:

/Api                → ASP.NET Core Controllers, HTTP Endpoints
/Application        → Business logic, DTOs, Interfaces, Services
/Domain             → Entities, Enums, Repository Interfaces (Core)
/Infrastructure     → EF Core, DbContext, Migrations, SQL Repositories

<h2>Key Architectural Techniques</h2>

- Domain‑driven entities (Customer, BankAccount, Transaction)
- Repository pattern with interface abstractions
- Dependency Inversion (Application depends only on Domain)
- Constructor injection everywhere
- EF Core for persistence
- Swagger UI enabled for quick testing

 <h1>Features</h1>

 <h2>Customer Management</h2>
 
- Create customer
- - View customer
- List customers

<h2>Bank Account Management</h2>
- Open an account (Savings, Cheque, Investment)
- View accounts per customer
- Freeze / Unfreeze functionality (optional extension)

<h2>Transactions</h2>
- Deposit
- Withdraw
- Transfer between accounts
- Fully audited with transaction history

<h2>Query</h2>
- Get all transactions for a specific account

<h1>Technologies Used</h1>

<h1>Folder Structure</h1>
VaultBank
 ├── Api
 │    ├── Controllers
 │    ├── Program.cs
 │    └── appsettings.json
 │
 ├── Application
 │    ├── Interfaces
 │    ├── Services
 │    └── Models/DTOs
 │
 ├── Domain
 │    ├── Entities
 │    ├── Enums
 │    └── Repositories
 │
 └── Infrastructure
      ├── Persistence
      │    └── VaultDbContext.cs
      ├── Repositories
      └── Migrations



