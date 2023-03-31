using Project_IdentityDomainEntities;
using Project_Main.Models.DataBases.Repositories.General;

namespace Project_Main.Models.DataBases.Repositories.Identity
{
    public interface IUserRepository : IGenericRepository<UserModel>
    {
        Task<UserModel?> GetWithDetailsAsync(string userId);
        Task<UserModel?> GetByNameAndPasswordAsync(string userLogin, string userPassword);
        Task<bool> IsNameTakenAsync(string userName);

        Task<IEnumerable<RoleModel>> GetRolesAsync(string userId);
    }
}
