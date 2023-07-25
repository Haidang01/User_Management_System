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
using System;
using System.Drawing.Printing;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BT_MVC_Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EthnicityController : BaseController
    {
        private readonly IEthnicityService _ethnicityService;
        private readonly IMapper _mapper;

        public EthnicityController(IEthnicityService ethnicityService, IMapper mapper)
        {
            _ethnicityService = ethnicityService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetEthnicitys(int page = 1, int pageSize = 3)
        {
            try
            {
                var ethnicities = await _ethnicityService.GetAllEthnicityAsync();

                var ethnicitiesGet = _mapper.Map<List<EthnicityGetDto>>(ethnicities);

                var paginationData = PaginationHelper<EthnicityGetDto>.GetPagedData(ethnicitiesGet, page, pageSize);

                return CustomResult(paginationData, HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEthnicityById([FromRoute] int id)
        {
            try
            {
                var ethnicity = await _ethnicityService.GetEthnicityAsync(e => e.EthnicityId == id);

                if (ethnicity == null)
                {
                    return CustomResult("Ethnicity is not found!", HttpStatusCode.NotFound);
                }
                return CustomResult(ethnicity, HttpStatusCode.OK); ;
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateEthnicity([FromBody] Ethnicity obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existsEthnicity = await _ethnicityService.GetAllEthnicityAsync();

                    if (existsEthnicity.Any(e => e.EthnicityName == obj.EthnicityName))
                    {
                        return CustomResult("The Ethnicity has existed!", HttpStatusCode.BadRequest);
                    }

                    await _ethnicityService.AddEthnicityAsync(obj);

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
        public async Task<IActionResult> UpdateEthnicity([FromRoute] int id, [FromBody] Ethnicity obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (id != obj.EthnicityId)
                    {
                        return CustomResult("Data is not valid! ", HttpStatusCode.BadRequest);
                    }
                    await _ethnicityService.UpdateEthnicityAsync(obj);

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
        public async Task<IActionResult> DeleteEthnicity([FromRoute] int id)
        {
            try
            {
                var existsEthnicity = await _ethnicityService.GetEthnicityAsync(e => e.EthnicityId == id);

                if (existsEthnicity == null)
                {
                    return NotFound();
                }
                await _ethnicityService.RemoveEthnicity(existsEthnicity);

                return CustomResult("Delete successfully.", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }

}
