using BT_MVC_Web.Models;
using BT_MVC_Web.Repositories.Interface;
using BT_MVC_Web.Services.Interface;
using System.Linq.Expressions;

namespace BT_MVC_Web.Services
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddCityAsync(City city)
        {
            await _unitOfWork.City.AddAsync(city);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<City>> GetAllCitiesAsync(int page, int pageSize, string? includeProperties = null)
        {
            return await _unitOfWork.City.GetAllAsync(page, pageSize, includeProperties);
        }

        public async Task<City> GetCityAsync(Expression<Func<City, bool>> filter, string? includeProperties = null)
        {
            return await _unitOfWork.City.GetAsync(filter, includeProperties);
        }

        public async Task RemoveCity(City city)
        {
            _unitOfWork.City.Remove(city);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateCityAsync(City city)
        {
            await _unitOfWork.City.UpdateCityAsync(city);
            await _unitOfWork.SaveAsync();
        }
    }
}
