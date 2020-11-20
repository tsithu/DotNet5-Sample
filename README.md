# DotNet5-Sample
Testing Project: Simple Transaction with .NET 5

### Setup SqlExpress for Linux ( Not Use )
```
docker pull mcr.microsoft.com/mssql/server
```
```
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=618wSJHU1kL3ddSMNW' -e 'MSSQL_PID=Express' -p 1433:1433 -d mcr.microsoft.com/mssql/server
```

### Applying migrations manually
```
dotnet ef database update
```