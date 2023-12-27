# PTN Backend Assignment

This is an assignment project for {Piton Technology}.

## Prerequisites

To run the project, make sure you have the following installed:

- [**.NET SDK**](https://dotnet.microsoft.com/download)
- [**SQL Server**](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## Getting Started

To run the project, follow these steps:

1. Clone the repository and navigate to the directory:

   ```bash
   git clone https://github.com/seppacar/PTN-BackendAssignment.git
   cd PTN-BackendAssignment
   ```
2. Edit database connection string: Open appsettings.json and update the connection string under "ConnectionStrings".
   ```
   "ConnectionStrings": {
    "MSSQL": "YourDatabaseConnectionStringHere"
   },
   ```
4. **Generate and Apply Migrations:**

   Run the following commands to generate and apply migrations:
   Install dotnet ef global or locally if you don't have
   ```
   dotnet tool install --global dotnet-ef
   ```
   And apply migrations to the database

   ```bash
   dotnet ef migrations add InitialMigration
   dotnet ef database update
   ```
   
3. Run the application
   ```
   dotnet run
   ```
