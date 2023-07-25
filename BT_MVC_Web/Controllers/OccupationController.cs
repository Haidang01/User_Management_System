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

namespace BT_MVC_Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OccupationController : BaseController
    {
        private readonly IOccupationService _occupationService;
        private readonly IMapper _mapper;

        public OccupationController(IOccupationService occupationService, IMapper mapper)
        {
            _occupationService = occupationService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetOccupations(int page = 1, int pageSize = 3)
        {
            try
            {
                var occupations = await _occupationService.GetAllOccupationsAsync();

                var occupationsGet = _mapper.Map<List<OccupationGetDto>>(occupations);

                var paginationData = PaginationHelper<OccupationGetDto>.GetPagedData(occupationsGet, page, pageSize);

                return CustomResult(paginationData, HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOccupationById([FromRoute] int id)
        {
            try
            {
                var occupation = await _occupationService.GetOccupationAsync(o => o.OccupationId == id);
                if (occupation == null)
                {
                    return CustomResult("Occupation is not found!", HttpStatusCode.NotFound);
                }
                return CustomResult(occupation, HttpStatusCode.OK); ;
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOccupation([FromBody] Occupation obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existsOccupation = await _occupationService.GetAllOccupationsAsync();

                    if (existsOccupation.Any(c => c.OccupationName == obj.OccupationName))
                    {
                        return CustomResult("The Occupation has existed!", HttpStatusCode.BadRequest);
                    }

                    await _occupationService.AddOccupationAsync(obj);

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
        public async Task<IActionResult> UpdateOccupation([FromRoute] int id, [FromBody] Occupation obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (id != obj.OccupationId)
                    {
                        return CustomResult("Data is not valid! ", HttpStatusCode.BadRequest);
                    }
                    await _occupationService.UpdateOccupationAsync(obj);

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
        public async Task<IActionResult> DeleteOccupation([FromRoute] int id)
        {
            try
            {
                var existsOccupation = await _occupationService.GetOccupationAsync(o => o.OccupationId == id);

                if (existsOccupation == null)
                {
                    return NotFound();
                }
                await _occupationService.RemoveOccupation(existsOccupation);

                return CustomResult("Delete successfully.", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }

}
