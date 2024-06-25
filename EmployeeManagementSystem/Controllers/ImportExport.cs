using Employee_Management_System.DTO;
using Employee_Management_System.Entities;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Drawing;
using AutoMapper;
using Employee_Management_System.Interfaces;

namespace Employee_Management_System.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]

    public class ImportExport : Controller

    {
        private readonly IEmployeeBasicService _employeeBasicService;
        private readonly IEmployeeAdditionalService _employeeAdditionalService;
        
        public ImportExport(IEmployeeBasicService employeeBasicService, IEmployeeAdditionalService employeeAdditionalService)
        {
            _employeeBasicService = employeeBasicService;
            _employeeAdditionalService = employeeAdditionalService;
        }

        [HttpPost("ImportExcel")]
        public async Task<IActionResult> ImportExcel(IFormFile formFile)
        {
            var employees = new List<EmployeeBasicDTO>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var houseOrBuildingNumberString = GetStringFromCell(worksheet, row, 10);
                        int houseOrBuildingNumber;
                        if (!int.TryParse(houseOrBuildingNumberString, out houseOrBuildingNumber))
                        {
                            return BadRequest($"Invalid HouseOrBuildingNumber in row {row}");
                        }

                        var address = new Address
                        {
                            HouseNumber = houseOrBuildingNumber,
                            StreetName = GetStringFromCell(worksheet, row, 11),
                            City = GetStringFromCell(worksheet, row, 12),
                            State = GetStringFromCell(worksheet, row, 13),
                            PostalCodes = GetStringFromCell(worksheet, row, 14)
                        };

                        var employee = new EmployeeBasicDTO
                        {
                            EmployeeID = GetStringFromCell(worksheet, row, 2),
                            Salutory = GetStringFromCell(worksheet, row, 3),
                            FirstName = GetStringFromCell(worksheet, row, 4),
                            MiddleName = GetStringFromCell(worksheet, row, 5),
                            LastName = GetStringFromCell(worksheet, row, 6),
                            NickName = GetStringFromCell(worksheet, row, 7),
                            Email = GetStringFromCell(worksheet, row, 8),
                            Mobile = GetStringFromCell(worksheet, row, 9),
                            Address = address,
                            Role = GetStringFromCell(worksheet, row, 15),
                            ReportingManagerUId = GetStringFromCell(worksheet, row, 16),
                            ReportingManagerName = GetStringFromCell(worksheet, row, 17),
                        };


                        var addedEmployee = await _employeeBasicService.AddEmployeeBasicDetails(employee);
                        employees.Add(addedEmployee);
                    }
                }
            }

            return Ok(employees);
        }

        private string GetStringFromCell(ExcelWorksheet worksheet, int row, int column)
        {
            var cellValue = worksheet.Cells[row, column].Value;
            return cellValue?.ToString();
        }

        [HttpGet("ExportInExcel")]
        public async Task<IActionResult> Export()
        {

            var basicDetails = await _employeeBasicService.GetAllEmployeeBasicDetails();
            

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Employees");

                worksheet.Cells[1, 1].Value = "Sr.No";
                worksheet.Cells[1, 2].Value = "First Name";
                worksheet.Cells[1, 3].Value = "Last Name";
                worksheet.Cells[1, 4].Value = "Email";
                worksheet.Cells[1, 5].Value = "Phone No";
                worksheet.Cells[1, 6].Value = "Reporting Manager Name";

                var rowIndex = 2;
                foreach (var data in basicDetails)
                {
                    worksheet.Cells[rowIndex, 1].Value = data.EmployeeID;
                    worksheet.Cells[rowIndex, 2].Value = data.FirstName;
                    worksheet.Cells[rowIndex, 3].Value = data.LastName;
                    worksheet.Cells[rowIndex, 4].Value = data.Email;
                    worksheet.Cells[rowIndex, 5].Value = data.Mobile;
                    worksheet.Cells[rowIndex, 6].Value = data.ReportingManagerName;
                    rowIndex++;
                }

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                var fileName = "Employeeinformation.xlsx";
                return File(stream, "application/vnd.openxmlformats-document.spreadsheetml.sheet", fileName);
            }
        }
    }
}
