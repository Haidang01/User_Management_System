using BT_MVC_Web.Models;
using BT_MVC_Web.Repositories.Interface;
using BT_MVC_Web.Services.Interface;
using System.Linq.Expressions;

namespace BT_MVC_Web.Services
{
    public class EthnicityService : IEthnicityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EthnicityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddEthnicityAsync(Ethnicity ethnicity)
        {

            await _unitOfWork.Ethnicity.AddAsync(ethnicity);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<Ethnicity>> GetAllEthnicityAsync(string? includeProperties = null)
        {
            return await _unitOfWork.Ethnicity.GetAllAsync(includeProperties);
        }

        public async Task<Ethnicity> GetEthnicityAsync(Expression<Func<Ethnicity, bool>> filter, string? includeProperties = null)
        {
            return await _unitOfWork.Ethnicity.GetAsync(filter, includeProperties);
        }

        public async Task RemoveEthnicity(Ethnicity ethnicity)
        {
            _unitOfWork.Ethnicity.Remove(ethnicity);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateEthnicityAsync(Ethnicity ethnicity)
        {
            await _unitOfWork.Ethnicity.UpdateEthnicityAsync(ethnicity);
            await _unitOfWork.SaveAsync();
        }
    }
}
