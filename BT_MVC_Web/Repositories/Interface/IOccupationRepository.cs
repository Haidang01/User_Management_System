using BT_MVC_Web.Models;

namespace BT_MVC_Web.Repositories.Interface
{
    public interface IOccupationRepository : IRepository<Occupation>
    {
        Task UpdateOccupationAsync(Occupation occupation);
    }

}
