namespace Project_Main.Models.DataBases.Repositories
{
	public interface IIdentityUnitOfWork : IUnitOfWork
	{
		IIdentityRepository IdentityRepository { get; }
	}
}
