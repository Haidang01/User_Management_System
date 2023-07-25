using BT_MVC_Web.DTOs;
using BT_MVC_Web.Models;
using OfficeOpenXml;

namespace BT_MVC_Web.Helpers
{
    public class ExcelHelper
    {
        public static byte[] ExportUsersToExcel(List<EmployeeExport> obj)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // or LicenseContext.Commercial

            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Employee");

                // Ghi dữ liệu vào Excel
                worksheet.Cells.LoadFromCollection(obj, true);

                // Xuất file Excel
                return package.GetAsByteArray();
            }
        }

    }
}
