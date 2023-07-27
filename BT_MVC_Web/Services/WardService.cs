using BT_MVC_Web.Models;
using BT_MVC_Web.Repositories.Interface;
using BT_MVC_Web.Services.Interface;
using System.Linq.Expressions;

namespace BT_MVC_Web.Services
{
    public class WardService : IWardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public WardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddWardAsync(Ward obj)
        {
            await _unitOfWork.Ward.AddAsync(obj);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<Ward>> GetAllWardsAsync(int page, int pageSize, string? includeProperties = null)
        {
            return await _unitOfWork.Ward.GetAllAsync(page, pageSize, includeProperties);
        }

        public async Task<Ward> GetWardAsync(Expression<Func<Ward, bool>> filter, string? includeProperties = null)
        {
            return await _unitOfWork.Ward.GetAsync(filter, includeProperties);
        }

        public async Task RemoveWard(Ward obj)
        {
            _unitOfWork.Ward.Remove(obj);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateWardAsync(Ward obj)
        {
            await _unitOfWork.Ward.UpdateWardAsync(obj);
            await _unitOfWork.SaveAsync();
        }
    }
}
