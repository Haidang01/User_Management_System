using BT_MVC_Web.Models;

namespace BT_MVC_Web.Repositories.Interface
{
    public interface IWardRepository : IRepository<Ward>
    {
        Task UpdateWardAsync(Ward ward);
    }

}
