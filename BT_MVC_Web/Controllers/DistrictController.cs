using AutoMapper;
using BT_MVC_Web.Constants;
using BT_MVC_Web.DTOs;
using BT_MVC_Web.Helpers;
using BT_MVC_Web.Models;
using BT_MVC_Web.Repositories.Interface;
using BT_MVC_Web.Services;
using BT_MVC_Web.Services.Interface;
using BT_MVC_Web.ViewModels;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using System.Net;

namespace BT_MVC_Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DistrictController : BaseController
    {
        private readonly IDistrictService _districtService;
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;

        public DistrictController(IDistrictService districtService, ICityService cityService, IMapper mapper)
        {
            _districtService = districtService;
            _cityService = cityService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetDistricts(int page = AppConstrants.PAGE_DEFAULT, int pageSize = AppConstrants.PAGE_SIZE_DEFAULT)
        {
            try
            {
                var districts = await _districtService.GetAllDistrictsAsync(page, pageSize, "City,Wards");

                var districtsGet = _mapper.Map<List<DistrictGetDto>>(districts);

                var paginationData = PaginationHelper<DistrictGetDto>.GetPagedData(districtsGet, page, pageSize);

                return CustomResult(paginationData, HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDistrictById([FromRoute] int id)
        {
            try
            {
                var district = await _districtService.GetDistrictAsync(p => p.CityId == id);
                if (district == null)
                {
                    return NotFound();
                }
                return CustomResult(district, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDistrict([FromBody] DistrictCreatDto obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var city = await _cityService.GetCityAsync(c => c.CityId == obj.CityId);

                    if (city == null)
                    {
                        return BadRequest();
                    }
                    await _districtService.AddDistrictAsync(obj);

                    return CustomResult(HttpStatusCode.Created);
                }
                catch (Exception ex)
                {
                    return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
                }
            }
            return CustomResult(ModelState, HttpStatusCode.BadRequest);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateDistrict([FromRoute] int id, [FromBody] District obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (id != obj.DistrictId)
                    {
                        return BadRequest();
                    }

                    await _districtService.UpdateDistrictAsync(obj);

                    return CustomResult(obj, HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
                }
            }
            return CustomResult(ModelState, HttpStatusCode.BadRequest);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCity([FromRoute] int id)
        {
            try
            {
                var existsDistrict = await _districtService.GetDistrictAsync(c => c.DistrictId == id);

                if (existsDistrict == null)
                {
                    return CustomResult("The city does not exist!", HttpStatusCode.NotFound);
                }
                await _districtService.RemoveDistrict(existsDistrict);

                return CustomResult("Delete successfully.", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("ward/{id:int}")]
        public async Task<IActionResult> GetAllWardByDistrictId([FromRoute] int id)
        {
            try
            {
                var districts = await _districtService.GetDistrictAsync(c => c.DistrictId == id, "Wards");

                var wardsGet = _mapper.Map<List<WardGetDto>>(districts.Wards);

                return CustomResult(wardsGet, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }

}
