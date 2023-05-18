Steps to Set Up the SalesAzure Project:

1. Create Database:
   - Open SQL Server Management Studio or any SQL client tool.
   - Execute the following SQL command to create a new database:
     
     CREATE DATABASE SalesDB;
     
2. Rebuild the Project:
   - Rebuild the entire solution to ensure all dependencies are resolved correctly.

3. Install Required NuGet Packages:
   - Open the NuGet Package Manager or use the Package Manager Console.
   - For the SalesAzure project, install the following packages:
     - AutoMapper v12.0.0
     - AutoMapper.Extensions.Microsoft.DependencyInjection v12.0.0
     - FluentValidation v11.4.0
     - Microsoft.Azure.Functions.Extensions v1.1.0
     - Microsoft.Azure.WebJobs.Extensions.OpenApi v1.5.0
     - Microsoft.NET.Sdk.Functions v4.1.3
   - For the SalesData project, install the following packages:
     - Microsoft.EntityFrameworkCore v6.0.12
     - Microsoft.EntityFrameworkCore.Design v6.0.12
     - Microsoft.EntityFrameworkCore.SqlServer v6.0.12
     - Microsoft.EntityFrameworkCore.Tools v6.0.12

4. Database Migration:
   - Open the Package Manager Console in Visual Studio.
   - Set the SalesData project as Start up project.
   - Run the following commands one by one:
     
     Add-Migration MigrationName
     Update-Database
     
   - These commands will generate the necessary database schema based on the entity models.

5. Add local.settings.json
{
    "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "SqlServerConnection": "Server=localhost;Database=SalesDB;User Id=sa;Password=Inchcape2023;Trust Server Certificate=true;"
  }
}

   **REMEMBER if you can't still connect to Database try replacing localhost with your local DB server name.

By following these steps, you will be able to set up the SalesAzure project successfully.
