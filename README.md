HOW TO 

Instructions:
(If you don't have VS or SSMS)
Download Visual Studio (Community): https://visualstudio.microsoft.com/
Download and connect to SQL Server Management Studio (SSMS): https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15

Download and save the RecipeManagerApp file/folder.

Having located the saved file/folder, go to:
"Emma Gunnari Lab3" --> and then open the file.
Having opened the program in Visual Studio. Open the Package Manager Console (PMC) and write: Update-Database
Run the program!

How to Ef Core: https://docs.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli

Install EF Tools globally

Dotnet tool install --global dotnet-ef

Update with dotnet tool update --global dotnet-ef

Dotnet add package Microsoft.EntityFrameworkCore.SqlServer

Dotnet add package Microsoft.EntityFrameworkCore.Design

To read appsettings.json: dotnet add package Microsoft.Extensions.Configuration.Json
