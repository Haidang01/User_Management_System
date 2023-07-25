using BT_MVC_Web.Data_Access;
using BT_MVC_Web.DTOs;
using BT_MVC_Web.Helpers;
using BT_MVC_Web.Models;
using BT_MVC_Web.Repositories;
using BT_MVC_Web.Repositories.Interface;
using BT_MVC_Web.Services;
using BT_MVC_Web.Services.Interface;
using BT_MVC_Web.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;
using System.Drawing.Printing;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BT_MVC_Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _employeeService = employeeService;
        }
        public async Task<IActionResult> Index() => View();
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(NewEmployeeDto obj)
        {
            if (obj.DateOfBirth < DateTime.Parse("01/01/1900") || obj.DateOfBirth > DateTime.Now)
            {
                ModelState.AddModelError("DateOfBirth", "Ngày không hợp lệ.");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _employeeService.AddEmployeeAsync(obj);

                    TempData["success"] = "Employee created successfully";

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                    ModelState.AddModelError(string.Empty, "An error occurred while creating the employee: " + ex.Message);
                }
            }
            return View(obj);
        }

        public async Task<IActionResult> ExportToExcel(string[] selectedIds)
        {
            if (selectedIds != null && selectedIds.Length > 0)
            {
                List<EmployeeExport> employeeToExport = await _employeeService.GetSelectedEmployee(selectedIds);

                byte[] excelBytes = ExcelHelper.ExportUsersToExcel(employeeToExport);

                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Employee-{DateTime.Now.ToString("dd/MM/yyyy")}.xlsx");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ImportFromExcel(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {

                await _employeeService.ImportFromExcel(file);

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            EmployeeVM employeeVM = new()
            {
                Employee = new Employee(),
                Citys = _unitOfWork.City.GetAll().Select(c =>
                new SelectListItem
                {
                    Text = c.CityName,
                    Value = c.CityId.ToString()
                }),
                Districts = _unitOfWork.District.GetAll().Select(d =>
                new SelectListItem
                {
                    Text = d.DistrictName,
                    Value = d.DistrictId.ToString()
                }),
                Ethnicity = _unitOfWork.Ethnicity.GetAll().Select(e =>
                new SelectListItem
                {
                    Text = e.EthnicityName,
                    Value = e.EthnicityId.ToString()
                }),
                Occupations = _unitOfWork.Occupation.GetAll().Select(o =>
                new SelectListItem
                {
                    Text = o.OccupationName,
                    Value = o.OccupationId.ToString()
                }),
                Wards = _unitOfWork.Ward.GetAll().Select(w =>
                new SelectListItem
                {
                    Text = w.WardName,
                    Value = w.WardId.ToString()
                }),
            };

            if (id == null || id == 0) return View(employeeVM);

            employeeVM.Employee = _unitOfWork.Employee.Get(u => u.Id == id);
            return View(employeeVM);
        }
        [HttpPost]
        public IActionResult EditPost(EmployeeVM employeeVM)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _employeeService.UpdateEmployee(employeeVM.Employee);

                    TempData["success"] = "Employee updated successfully";
                    
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                }

            }
            employeeVM.Citys = _unitOfWork.City.GetAll().Select(c =>
            new SelectListItem
            {
                Text = c.CityName,
                Value = c.CityId.ToString()
            });
            employeeVM.Districts = _unitOfWork.District.GetAll().Select(d =>
            new SelectListItem
            {
                Text = d.DistrictName,
                Value = d.DistrictId.ToString()
            });
            employeeVM.Ethnicity = _unitOfWork.Ethnicity.GetAll().Select(e =>
            new SelectListItem
            {
                Text = e.EthnicityName,
                Value = e.EthnicityId.ToString()
            });
            employeeVM.Occupations = _unitOfWork.Occupation.GetAll().Select(o =>
            new SelectListItem
            {
                Text = o.OccupationName,
                Value = o.OccupationId.ToString()
            });
            employeeVM.Wards = _unitOfWork.Ward.GetAll().Select(w =>
            new SelectListItem
            {
                Text = w.WardName,
                Value = w.WardId.ToString()
            });
            return View("Edit",employeeVM);
        }

        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var objList = await _employeeService.GetAllEmployeeAsync("Ethnicity,Occupation,District,Ward,City");
            return Json(new
            {
                data = objList,
            });
        }

        [HttpDelete("/employee/{id:int}")]
        public IActionResult Delete(int id)
        {
            var employeeToBeDelete = _unitOfWork.Employee.Get(u => u.Id == id);
            if (employeeToBeDelete == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Error while Deleting"
                });
            }
            _unitOfWork.Employee.Remove(employeeToBeDelete);
            _unitOfWork.Save();
            return Json(new
            {
                success = false,
                message = "Delete successful"
            });
        }
        #endregion



    }
}
