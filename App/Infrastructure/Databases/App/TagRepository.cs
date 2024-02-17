using App.Features.Tags.Common.Models;
using App.Infrastructure.Databases.App.Interfaces;
using App.Infrastructure.Databases.Common;

namespace App.Infrastructure.Databases.App;

public class TagRepository : GenericRepository<TagModel>, ITagRepository
{
	public TagRepository(CustomAppDbContext dbContext, ILogger<TagRepository> logger) : base(dbContext, logger)
	{
	}
}
