using BT_MVC_Web.Models;
using BT_MVC_Web.Repositories.Interface;
using BT_MVC_Web.Services.Interface;
using System.Linq.Expressions;

namespace BT_MVC_Web.Services
{
    public class OccupationService : IOccupationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OccupationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddOccupationAsync(Occupation occupation)
        {
            await _unitOfWork.Occupation.AddAsync(occupation);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<Occupation>> GetAllOccupationsAsync(string? includeProperties = null)
        {
            return await _unitOfWork.Occupation.GetAllAsync(includeProperties);
        }

        public async Task<Occupation> GetOccupationAsync(Expression<Func<Occupation, bool>> filter, string? includeProperties = null)
        {
            return await _unitOfWork.Occupation.GetAsync(filter, includeProperties);
        }

        public async Task RemoveOccupation(Occupation occupation)
        {
            _unitOfWork.Occupation.Remove(occupation);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateOccupationAsync(Occupation occupation)
        {
            await _unitOfWork.Occupation.UpdateOccupationAsync(occupation);
            await _unitOfWork.SaveAsync();
        }
    }
}
