namespace App.Infrastructure.Databases.Common.Interfaces;

public interface IBasicSeeder
{
    Task EnsurePopulated();
}