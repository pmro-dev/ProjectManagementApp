namespace Project_Main.Models.DataBases.Repositories
{
	public class IdentityUnitOfWork : UnitOfWork<CustomIdentityDbContext>, IIdentityUnitOfWork
	{
		public IIdentityRepository IdentityRepository { get; }

		public IdentityUnitOfWork(CustomIdentityDbContext context, IIdentityRepository identityRepository)
			: base(context)
		{
			IdentityRepository = identityRepository;
		}
	}
}
