using AutoMapper;
using BT_MVC_Web.DTOs;
using BT_MVC_Web.Models;
using BT_MVC_Web.Repositories.Interface;
using BT_MVC_Web.Services.Interface;
using System.Linq.Expressions;

namespace BT_MVC_Web.Services
{
    public class DistrictService : IDistrictService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DistrictService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task AddDistrictAsync(DistrictCreatDto obj)
        {
            District district = _mapper.Map<District>(obj);
            await _unitOfWork.District.AddAsync(district);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<District>> GetAllDistrictsAsync(int page, int pageSize, string? includeProperties = null)
        {
            return await _unitOfWork.District.GetAllAsync(page, pageSize, includeProperties);
        }

        public async Task<District> GetDistrictAsync(Expression<Func<District, bool>> filter, string? includeProperties = null)
        {
            return await _unitOfWork.District.GetAsync(filter, includeProperties);
        }

        public async Task RemoveDistrict(District obj)
        {
            _unitOfWork.District.Remove(obj);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateDistrictAsync(District obj)
        {
            await _unitOfWork.District.UpdateDistrictAsync(obj);
            await _unitOfWork.SaveAsync();
        }
    }
}
