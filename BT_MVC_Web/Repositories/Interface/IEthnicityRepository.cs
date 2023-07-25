using BT_MVC_Web.Models;

namespace BT_MVC_Web.Repositories.Interface
{
    public interface IEthnicityRepository : IRepository<Ethnicity>
    {
        Task UpdateEthnicityAsync(Ethnicity ethnicity);
    }

}
