using App.Infrastructure.Databases.Common.Interfaces;

namespace App.Common.Interfaces;

public interface IBasicModelAbstract<TId> : IBaseEntity<TId>
{
    //public int Id { get; set; }
    public string Title { get; set; }
}
