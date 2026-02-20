VaultBank – Secure Banking Backend API

A Clean Architecture .NET 8 Banking System

VaultBank is a backend‑only banking API built in .NET 8, following strict Clean Architecture, SOLID principles, and constructor injection throughout the entire solution.
It simulates real banking operations such as customer management, account creation, deposits, withdrawals, transfers, and transaction auditing.

Architecture Overview

VaultBank follows Clean Architecture with the following layers:

/Api                → ASP.NET Core Controllers, HTTP Endpoints
/Application        → Business logic, DTOs, Interfaces, Services
/Domain             → Entities, Enums, Repository Interfaces (Core)
/Infrastructure     → EF Core, DbContext, Migrations, SQL Repositories
