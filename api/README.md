# React .NET Starter

## Tech Stack

- .NET Core 5.0
- Entity Framework

---

## Getting Started

Run the following commands from the api directory

```shell
dotnet add package Microsoft.AspNetCore.Authentication.Google
dotnet user-secrets init
dotnet user-secrets set "Authentication:Google:ClientID" "YOUR_CLIENT_ID"
dotnet user-secrets set "Authentication:Google:ClientSecret" "YOUR_CLIENT_SECRET"
```

You will also need a local SQL Server DB instance. After setting up a database, run the following commands:

```shell
dotnet user-secrets set "DbConnection" "YOUR_CONN_STRING"
dotnet ef database update
```
