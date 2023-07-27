using BT_MVC_Web.Models;
using System.Linq.Expressions;

namespace BT_MVC_Web.Services.Interface
{
    public interface IEthnicityService
    {
        Task<IEnumerable<Ethnicity>> GetAllEthnicityAsync(int page, int pageSize, string? includeProperties = null);
        Task<Ethnicity>? GetEthnicityAsync(Expression<Func<Ethnicity, bool>> filter, string? includeProperties = null);
        Task AddEthnicityAsync(Ethnicity ethnicity);
        Task UpdateEthnicityAsync(Ethnicity ethnicity);
        Task RemoveEthnicity(Ethnicity ethnicity);

    }
}
