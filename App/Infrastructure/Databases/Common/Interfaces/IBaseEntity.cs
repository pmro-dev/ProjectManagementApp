namespace App.Infrastructure.Databases.Common.Interfaces;

public interface IBaseEntity<Y> where Y : notnull
{
	public Y Id { get; set; }
}
