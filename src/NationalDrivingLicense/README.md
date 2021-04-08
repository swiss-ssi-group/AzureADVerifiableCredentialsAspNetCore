# National Driving License 

creates drving license credentials

## Database migrations create

### Console

dotnet ef migrations add init_ndl -c ApplicationDbContext

### Powershell

Add-Migration "init_ndl" -c ApplicationDbContext  

## Database migrations update

### Console

dotnet ef database update

### Powershell

Update-Database

