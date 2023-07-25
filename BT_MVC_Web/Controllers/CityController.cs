using AutoMapper;
using BT_MVC_Web.DTOs;
using BT_MVC_Web.Helpers;
using BT_MVC_Web.Models;
using BT_MVC_Web.Repositories.Interface;
using BT_MVC_Web.Services;
using BT_MVC_Web.Services.Interface;
using BT_MVC_Web.ViewModels;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BT_MVC_Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : BaseController
    {
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;

        public CityController(ICityService cityService, IMapper mapper)
        {
            _cityService = cityService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCities(int page = 1, int pageSize = 3)
        {
            try
            {
                var cities = await _cityService.GetAllCitiesAsync();

                var citiesGet = _mapper.Map<List<CityGetDto>>(cities);

                var paginationData = PaginationHelper<CityGetDto>.GetPagedData(citiesGet, page, pageSize);

                return CustomResult(paginationData, HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("districts/{id:int}")]
        public async Task<IActionResult> GetAllDistrictByCityId([FromRoute] int id)
        {
            try
            {
                var cities = await _cityService.GetCityAsync(c => c.CityId == id, "Districts");

                var districtGetData = _mapper.Map<List<DistrictGetDto>>(cities.Districts);

                return CustomResult(districtGetData, HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCityById([FromRoute] int id)
        {
            try
            {
                var city = await _cityService.GetCityAsync(p => p.CityId == id, "Districts");
                if (city == null)
                {
                    return CustomResult("City is not found!", HttpStatusCode.NotFound);
                }
                return CustomResult(city, HttpStatusCode.OK); ;
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCity([FromBody] City city)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existsCity = await _cityService.GetAllCitiesAsync();
                    if (existsCity.Any(c => c.CityName == city.CityName))
                    {
                        return CustomResult("The city has existed!", HttpStatusCode.BadRequest);
                    }

                    await _cityService.AddCityAsync(city);

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
        public async Task<IActionResult> UpdateCity([FromRoute] int id, [FromBody] City obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (id != obj.CityId)
                    {
                        return CustomResult("Data is not valid! ", HttpStatusCode.BadRequest);
                    }
                    await _cityService.UpdateCityAsync(obj);

                    return CustomResult("Updated successfully.", HttpStatusCode.OK);
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
                var existsCity = await _cityService.GetCityAsync(c => c.CityId == id);

                if (existsCity == null)
                {
                    return NotFound();
                }
                await _cityService.RemoveCity(existsCity);

                return CustomResult("Delete successfully.", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }

}
