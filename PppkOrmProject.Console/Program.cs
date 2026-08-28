using PppkOrmProject.Data;

using var context = new AppDbContext();

var canConnect = context.Database.CanConnect();

Console.WriteLine($"DbConnection {canConnect}");