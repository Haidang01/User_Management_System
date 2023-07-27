using AutoMapper;
using BT_MVC_Web.Constants;
using BT_MVC_Web.DTOs;
using BT_MVC_Web.Helpers;
using BT_MVC_Web.Models;
using BT_MVC_Web.Services.Interface;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BT_MVC_Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WardController : BaseController
    {
        private readonly IDistrictService _districtService;
        private readonly IWardService _wardService;
        private readonly IMapper _mapper;

        public WardController(IDistrictService districtService, IWardService wardService, IMapper mapper)
        {
            _districtService = districtService;
            _wardService = wardService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetWards(int page = AppConstrants.PAGE_DEFAULT, int pageSize = AppConstrants.PAGE_SIZE_DEFAULT)
        {
            try
            {
                var wards = await _wardService.GetAllWardsAsync(page, pageSize, "District");

                var wardsGet = _mapper.Map<List<WardGetDto>>(wards);

                var paginationData = PaginationHelper<WardGetDto>.GetPagedData(wardsGet, page, pageSize);

                return CustomResult(paginationData, HttpStatusCode.OK); ;
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError); ;
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetWardById([FromRoute] int id)
        {
            try
            {
                var ward = await _wardService.GetWardAsync(p => p.WardId == id);
                if (ward == null)
                {
                    return CustomResult("Ward is not found!", HttpStatusCode.NotFound); ;
                }
                return CustomResult("Ward is not found!", HttpStatusCode.NotFound); ;
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError); ;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateWard([FromBody] Ward obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existsWard = await _wardService.GetWardAsync(d => d.WardName == obj.WardName);

                    if (existsWard != null)
                    {
                        return CustomResult("The ward has existed!", HttpStatusCode.BadRequest);
                    }
                    var existsdistrict = await _districtService.GetDistrictAsync(c => c.DistrictId == obj.DistrictId);

                    if (existsdistrict == null)
                    {
                        return BadRequest();
                    }
                    await _wardService.AddWardAsync(obj);

                    return CustomResult(HttpStatusCode.Created);
                }
                catch (Exception ex)
                {
                    return CustomResult(ex.Message, HttpStatusCode.InternalServerError); ;
                }
            }
            return CustomResult(ModelState, HttpStatusCode.BadRequest); ;
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateWard([FromRoute] int id, [FromBody] Ward obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (id != obj.WardId)
                    {
                        return BadRequest();
                    }
                    await _wardService.UpdateWardAsync(obj);

                    return CustomResult(obj, HttpStatusCode.OK); ;
                }
                catch (Exception ex)
                {
                    return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
                }
            }
            return CustomResult(ModelState, HttpStatusCode.BadRequest); ;
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteWard([FromRoute] int id)
        {
            try
            {
                var existsWard = await _wardService.GetWardAsync(c => c.WardId == id);

                if (existsWard == null)
                {
                    return CustomResult("The Ward does not exist!", HttpStatusCode.NotFound);
                }
                await _wardService.RemoveWard(existsWard);

                return CustomResult("Delete successfully.", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError); ;
            }
        }
    }

}
